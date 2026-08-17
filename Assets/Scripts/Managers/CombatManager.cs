using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private const float MOVEQUEUEDEFAULTDELAY = 1.0f;

    public Board_Controller board;
    public Combat_UI combatUI;
    public Player_Energy energy = new Player_Energy();

    private Coroutine queueRunner;

    private float boost = 1.0f;

    // The enemies in the current encounter
    [SerializeField]
    private List<ActiveEnemy> activeEnemies = new List<ActiveEnemy>();

    // The enemy currently targeted by the player
    private int targetedEnemy = 0;
    private int hoveredEnemy = 0;

    // The currently selected moves for each character
    private Dictionary<PC, MoveName> selectedMoves = new Dictionary<PC, MoveName>();

    // Queue of moves being used in a combo
    private Queue<QueuedMove> moveQueue = new Queue<QueuedMove>();

    // Queue of moves for the enemy
    private Queue<QueuedEnemyMove> enemyMoveQueue = new Queue<QueuedEnemyMove>();

    private bool moveQueueActive = false;

    private float queueDelay = MOVEQUEUEDEFAULTDELAY;

    private bool boardChanged = false;

    // Lock for enemy death animation
    private bool deathAnimationLock = false;

    // A tracker for how many moves you've triggered in one turn.
    private int moveCombo = 0;
    private int highestMoveCombo = 0;

    private int turnCount = 0;

    public void CombatSetup(Encounter _enc)
    {
        energy = new Player_Energy();
        Transform enemyHolderPos = GameObject.FindGameObjectWithTag("EnemyHolder").transform;

        for (int i = 0; i < _enc.EncounterEnemies.Count; i++)
        {
            Bestiary enemyType = _enc.EncounterEnemies[i];
            int enemyVarient = _enc.EnemyVarients[i];
            GameObject enemyObject = GameManager.instance.ll.enemyRepository.GetValue(enemyType);
            activeEnemies.Add(new ActiveEnemy(Instantiate(enemyObject, enemyHolderPos)));
            activeEnemies[i].enemyObject.transform.position += _enc.EnemyPositions[i];
            activeEnemies[i].EnemySetup(i, enemyVarient);
        }

        if (activeEnemies.Count > 0)
        {
            TargetEnemy(0, true);
        }

        SetupMoves();
    }

    private void SetupMoves()
    {
        // TODO: In future, have this check how many players are in the battle.

        MoveName[] test1 = { MoveName.LesserSpark, MoveName.LesserHeal, MoveName.ShareEnergy };
        MoveName[][] test2 = { test1, test1, test1, test1 };
        combatUI.SetupMoveButtons(test2);

        selectedMoves[PC.VANESSA] = MoveName.LesserSpark;
        selectedMoves[PC.SAMANTHA] = MoveName.LesserSpark;
        selectedMoves[PC.GABRIELLE] = MoveName.LesserSpark;
        selectedMoves[PC.VALLERY] = MoveName.LesserSpark;
    }

    public void CombatVictory()
    {

    }

    public void CombatCleanup()
    {
        energy = null;
        combatUI = null;
        board = null;
    }

    public void ProcessPlayerAttackDamage(int target, int potency)
    {
        if (activeEnemies[target] != null)
        {
            Combat_Enemy enemy = GetEnemy(target);
            Enemy_Stats targetStats = enemy.GetStats();
            if (targetStats.CurrentHealth > 0)
            {
                enemy.TakeDamage(potency);
                if (enemy.ShouldDie())
                {
                    this.deathAnimationLock = true;
                    KillEnemy(enemy);
                }
            }
        }
    }

    public void ProcessEnemyAttackDamage(int target, int potency)
    {
        Player_Information player = GameManager.instance.party.GetPlayer(target);
        if (player.CurrentHealth > 0)
        {
            GameManager.instance.party.GetPlayer(target).Damage(potency);
            if (player.ShouldDie())
            {
                player.ThenDie();
                combatUI.PlayerUI[target].KILL();
            }
        }
    }

    private void KillEnemy(Combat_Enemy rip)
    {
        if (rip.Die())
        {
            StartCoroutine(rip.GetSpriteInfo().PlayDeathAnimation(() => { this.deathAnimationLock = false; }));
            int nextEnemy = -1;
            for (int i = 0; i < activeEnemies.Count && nextEnemy < 0; i++)
            {
                if (activeEnemies[i].enemyScript.IsAlive())
                {
                    nextEnemy = i;
                }
            }

            if (nextEnemy >= 0)
            {
                TargetEnemy(nextEnemy, true);
            }
            else
            {
                CombatVictory();
            }
        }
    }

    public void SelectMove(PC pc, MoveName move, int pos)
    {
        if (!Combat_Commands.InteractionLocked())
        {
            selectedMoves[pc] = move;
            combatUI.HighlightMoveButton(pc, pos);
        }
    }

    public bool AddMoveToQueue(QueuedMove move)
    {
        try
        {
            moveQueue.Enqueue(move);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerator WaitToStartQueue()
    {
        yield return new WaitUntil(() => !GameManager.instance.fx.CheckAllFXLock());
        
        StartQueue();
    }

    public void StartQueue()
    {
        moveQueueActive = true;
        board.SetMouseLock(true);

        queueRunner = StartCoroutine(RunQueue());
    }

    public IEnumerator StopQueue()
    {
        moveQueueActive = false;
        board.SetMouseLock(false);
        queueRunner = null;

        if (CheckForRevives())
        {
            yield return new WaitForSeconds(1.0f);
        }

        EndTurn();
    }

    private void EndTurn()
    {
        IncrementEnemyTurn();
        CountDownStatus();
        turnCount++;
    }

    private IEnumerator RunQueue()
    {
        ResetCombo();
        
        if (moveQueue.Count > 0)
        {
            yield return new WaitForSeconds(1.0f);

            while (moveQueue.Count > 0 && moveQueueActive)
            {
                yield return StartCoroutine(NextQueue());
                yield return new WaitUntil(() => !this.deathAnimationLock);
                if (boardChanged)
                {
                    yield return new WaitForSeconds(queueDelay);
                    queueRunner = null;
                    boardChanged = false;
                    board.ResolveChains();
                    yield break;
                } else if (moveQueue.Count > 0)
                {
                    yield return new WaitForSeconds(queueDelay);
                }
            }
            yield return new WaitForSeconds(0.5f);

            ResetBoost();
        }

        StartCoroutine(StopQueue());
    }

    private IEnumerator NextQueue()
    {
        QueuedMove queuedMove = moveQueue.Dequeue();
        GameObject controller = Instantiate(queuedMove.move);
        Player_Move move = controller.GetComponent<Player_Move>();

        Player_Information user = GameManager.instance.party.GetPlayer(queuedMove.user);

        List<MoveResult> results = move.ResultsCalc(user, targetedEnemy, move.MoveInfo);
        move.StartMove((int)queuedMove.user, results);
        yield return new WaitUntil(() => move.IsMoveFinished());
        move.EndMove((int)queuedMove.user);
        move.ApplyMove(user, results, move.MoveInfo);

        queueDelay = move.DelayOverride ?? MOVEQUEUEDEFAULTDELAY;
        Destroy(controller);
    }

    private void StartEnemyQueue()
    {
        moveQueueActive = true;
        board.SetMouseLock(true);

        StartCoroutine(RunEnemyQueue());
    }

    private void StopEnemyQueue()
    {
        moveQueueActive = false;
        board.SetMouseLock(false);
    }

    private IEnumerator RunEnemyQueue()
    {
        // TEST CODE
        if (enemyMoveQueue.Count > 0)
        {
            yield return new WaitForSeconds(1.0f);

            while (enemyMoveQueue.Count > 0 && moveQueueActive)
            {
                QueuedEnemyMove queuedMove = enemyMoveQueue.Dequeue();
                GameObject controller = Instantiate(queuedMove.move);
                Enemy_Move move = controller.GetComponent<Enemy_Move>();

                int user = queuedMove.user;
                Enemy_Stats enemy = activeEnemies[user].enemyScript.GetStats();
                Behavior_Dad behavior = activeEnemies[user].enemyBehavior;

                List<int> targets = DecideEnemyTargets(queuedMove, behavior);
                List<MoveResult> results = move.ResultsCalc(enemy, targets, queuedMove.potency);
                move.StartMove(user, results);
                yield return new WaitUntil(() => move.IsMoveFinished());
                move.EndMove(user);
                move.ApplyMove(enemy, results);
                Destroy(controller);
                yield return new WaitUntil(() => !this.deathAnimationLock);

                activeEnemies[user].enemyVisuals.SetTurnNumber(activeEnemies[user].speed);

                if (enemyMoveQueue.Count > 0)
                {
                    yield return new WaitForSeconds(MOVEQUEUEDEFAULTDELAY);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }

        StopEnemyQueue();
    }

    private List<int> DecideEnemyTargets(QueuedEnemyMove move, Behavior_Dad behavior)
    {
        int livingPlayers = GameManager.instance.party.LivingPlayers();
        int num = move.targets <= livingPlayers ? move.targets : livingPlayers;

        List<int> targets = new List<int>();

        switch (move.targetingType)
        {
            case TargetingType.LowestHealth:
                for (int i = 0; i < num; i++)
                {
                    int target = GameManager.instance.party.LowestHealth(true, targets);
                    targets.Add(target);
                }
                break;
            case TargetingType.HighestHealth:
                for (int i = 0; i < num; i++)
                {
                    int target = GameManager.instance.party.HighestHealth(true, targets);
                    targets.Add(target);
                }
                break;
            case TargetingType.Random:
                for (int i = 0; i < num; i++)
                {
                    int target;
                    do
                    {
                        target = Random.Range(0, 4);
                    } while (targets.Contains(target));
                    targets.Add(target);
                }
                break;
            case TargetingType.Custom:
                targets = behavior.CustomTargeting();
                break;
        }

        return targets;
    }

    public void IncrementEnemyTurn()
    {
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (activeEnemies[i].enemyScript.IsAlive())
            {
                activeEnemies[i].speed -= 1;
                activeEnemies[i].enemyVisuals.SetTurnNumber(activeEnemies[i].speed);

                if (activeEnemies[i].speed <= 0)
                {
                    (GameObject moveObject, TargetingType targetType, int targets, int cooldown, float potency) = activeEnemies[i].enemyBehavior.MakeMove();
                    activeEnemies[i].speed = cooldown;
                    QueuedEnemyMove queuedMove = new QueuedEnemyMove(moveObject, i, targetType, targets, potency);
                    enemyMoveQueue.Enqueue(queuedMove);
                }
            }
        }

        if (enemyMoveQueue.Count > 0)
        {
            StartEnemyQueue();
        }
    }

    private void CountDownStatus()
    {
        foreach(Player_Information pi in GameManager.instance.party.Players())
        {
            if (pi.Status.IsAlive)
            {
                pi.Status.CountDownStatus();
            }
        }

        foreach(Combat_Enemy ce in GameManager.instance.combat.GetEnemies())
        {
            if (ce.IsAlive())
            {
                ce.GetStats().CountDownStatus();
            }
        }

        Combat_UI_Commands.UpdateStatusIcons();
    }

    private bool CheckForRevives()
    {
        bool revive = false;
        foreach (Player_Information player in GameManager.instance.party.Players())
        {
            if (player.ShouldLive())
            {
                player.LifeIsLifegem();
                revive = true;
            }
        }

        return revive;
    }

    public bool TargetEnemy(int enemy, bool stealth = false)
    {
        if (GetTargetedEnemyObject() != null)
        {
            Enemy_Visuals oldVisuals = GetTargetedEnemyObject().GetComponent<Enemy_Visuals>();
            oldVisuals.SetHealthBarEnabled(false);
        }

        targetedEnemy = enemy;
        Enemy_Visuals enemyVisuals = GetTargetedEnemyObject().GetComponent<Enemy_Visuals>();
        combatUI.TargetCrosshair(enemyVisuals.GetCenter());
        combatUI.SetCrosshairEnabled(true);
        if (!stealth)
        {
            enemyVisuals.SetHealthBarTimer(0.75f);
            UnhoverEnemy(enemy);
        }
        return true;
    }

    public bool HoverEnemy(int enemy)
    {
        if (enemy == targetedEnemy)
        {
            return false;
        }
        hoveredEnemy = enemy;
        Enemy_Visuals enemyVisuals = GetHoveredEnemyObject().GetComponent<Enemy_Visuals>();
        combatUI.HoverCrosshair(enemyVisuals.GetCenter());
        combatUI.SetHoverEnabled(true);
        return true;
    }

    public bool UnhoverEnemy(int enemy)
    {
        if (hoveredEnemy == enemy)
        {
            combatUI.SetHoverEnabled(false);
            return true;
        }
        return false;
    }

    public void AddBoost(float val)
    {
        boost += val;
        combatUI.SetBoostAmount(boost);
    }

    public void ResetBoost()
    {
        boost = 1.0f;
        combatUI.SetBoostAmount(boost);
    }

    public MoveName GetSelectedMove(PC player)
    {
        return selectedMoves[player];
    }

    public Combat_Enemy GetEnemy(int target)
    {
        if (activeEnemies.Count > target)
        {
            return activeEnemies[target].enemyScript;
        }
        return null;
    }

    public List<Combat_Enemy> GetEnemies()
    {
        return activeEnemies.Select(e => e.enemyScript).ToList();
    }

    public GameObject GetTargetedEnemyObject()
    {
        if (activeEnemies.Count > targetedEnemy)
        {
            return activeEnemies[targetedEnemy].enemyObject;
        }
        return null;
    }

    public GameObject GetHoveredEnemyObject()
    {
        if (activeEnemies.Count > hoveredEnemy)
        {
            return activeEnemies[hoveredEnemy].enemyObject;
        }
        return null;
    }

    public int GetTargetedNumber()
    {
        return targetedEnemy;
    }

    public int GetHoveredNumber()
    {
        return hoveredEnemy;
    }

    public float GetBoost()
    {
        return boost;
    }

    public void SetBoard(Board_Controller _val)
    {
        board = _val;
    }

    public void SetCombatUI(Combat_UI _val)
    {
        combatUI = _val;
    }

    public bool MoveQueueRunning()
    {
        return moveQueueActive;
    }

    public bool AddToMoveCombo()
    {
        moveCombo++;
        if (moveCombo > highestMoveCombo)
        {
            highestMoveCombo = moveCombo;
            return true;
        }
        return false;
    }

    public void ResetCombo()
    {
        moveCombo = 0;
    }

    public void BoardChanged()
    {
        boardChanged = true;
    }

    public void PlayActorAnimation(AnimDetails a)
    {
        if (a.targetType == Target.ENEMY)
        {
            if (activeEnemies.Count > a.target && activeEnemies[a.target] != null)
            {
                if (a.rotation != null)
                {
                    activeEnemies[a.target].enemyVisuals.PlayAnimationRotated(a.anim, (float)a.rotation);
                }
                else if (a.color != null)
                {
                    activeEnemies[a.target].enemyVisuals.PlayAnimationColor(a.anim, (Color)a.color);
                }
                else
                {
                    activeEnemies[a.target].enemyVisuals.PlayAnimation(a.anim);
                }
            }
        } else
        {
            int target = a.target;
            combatUI.PlayerUI[target].Anim.PlayAnimation(a);
        }
    }

    private class ActiveEnemy
    {
        public GameObject enemyObject;
        public Combat_Enemy enemyScript;
        public Behavior_Dad enemyBehavior;
        public Enemy_Visuals enemyVisuals;
        public int speed;

        public ActiveEnemy(GameObject obj)
        {
            enemyObject = obj;
            enemyScript = obj.GetComponent<Combat_Enemy>();
            enemyBehavior = obj.GetComponent<Behavior_Dad>();
            enemyVisuals = obj.GetComponent<Enemy_Visuals>();
        }

        public void EnemySetup(int position, int varient)
        {
            enemyScript.Setup(position, varient);

            speed = enemyBehavior.BaseSpeed;
        }
    }
}