using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.UI; // REMOVE ME LATER

public class CombatManager : MonoBehaviour
{
    // Testing vars
    public Text comboCounter;
    [SerializeField]
    private Text turnCounter;

    public Board_Controller board;
    public Combat_UI combatUI;
    public Player_Energy energy = new Player_Energy();

    // The enemies in the current encounter
    [SerializeField] private List<GameObject> activeEnemies;

    // The enemy currently targeted by the player
    private int targetedEnemy = 0;
    private int hoveredEnemy = 0;

    // The currently selected moves for each character
    private Dictionary<PC, MoveName> selectedMoves = new Dictionary<PC, MoveName>();

    // Queue of moves being used in a combo
    private Queue<QueuedMove> moveQueue = new Queue<QueuedMove>();
    private bool moveQueueActive = false;
    private bool moveQueueLock = false;

    private bool deathAnimationLock = false;

    // A tracker for how many moves you've triggered in one turn.
    private int moveCombo = 0;
    private int highestMoveCombo = 0;

    private int turnCount = 0;

    public void CombatSetup(Encounter _enc)
    {
        energy = new Player_Energy();
        Transform enemyHolderPos = GameObject.FindGameObjectWithTag("EnemyHolder").transform;

        for (int i = 0; i < _enc.EncounterContents.Count; i++)
        {
            activeEnemies.Add(Instantiate(_enc.EncounterContents[i], enemyHolderPos));
            activeEnemies[i].GetComponent<Combat_Enemy>().Setup(i);
            activeEnemies[i].transform.position += _enc.EnemyPositions[i];
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

        MoveName[] test1 = { MoveName.LesserSpark, MoveName.LesserHeal };
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

    private void KillEnemy(Combat_Enemy rip)
    {
        if (rip.Die())
        {
            StartCoroutine(rip.GetSpriteInfo().PlayDeathAnimation(() => { this.deathAnimationLock = false; }));
            int nextEnemy = -1;
            for (int i = 0; i < activeEnemies.Count && nextEnemy < 0; i++)
            {
                if (activeEnemies[i].GetComponent<Combat_Enemy>().isAlive())
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
        selectedMoves[pc] = move;
        combatUI.HighlightMoveButton(pc, pos);
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

        if (moveQueue.Count > 0)
        {
            yield return new WaitForSeconds(1.0f);

            StartQueue();
        }
        
        turnCount++;
        turnCounter.text = "Turn: " + turnCount;
    }

    public void StartQueue()
    {
        moveQueueLock = true;
        moveQueueActive = true;

        StartCoroutine(RunQueue());
    }

    public void StopQueue()
    {
        moveQueueLock = false;
        moveQueueActive = false;
    }

    private IEnumerator RunQueue()
    {
        ResetCombo();
        float moveQueueDelay = 1.0f;

        while (moveQueue.Count > 0 && moveQueueActive)
        {
            QueuedMove queuedMove = moveQueue.Dequeue();
            GameObject controller = Instantiate(queuedMove.move);
            Move_Dad move = controller.GetComponent<Move_Dad>();

            Player_Information user = GameManager.instance.party.GetPlayer(queuedMove.user);

            List<MoveResult> results = move.ResultsCalc(user, targetedEnemy, move.MoveInfo);
            move.StartMove(queuedMove.user, results);
            yield return new WaitUntil(() => move.IsMoveFinished());
            move.EndMove(queuedMove.user);
            move.ApplyMove(user, results, move.MoveInfo);
            Destroy(controller);
            yield return new WaitUntil(() => !this.deathAnimationLock);
            if (moveQueue.Count > 0)
            {
                yield return new WaitForSeconds(moveQueueDelay);
            }
        }
        yield return new WaitForSeconds(0.25f);

        StopQueue();
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

    public MoveName GetSelectedMove(PC player)
    {
        return selectedMoves[player];
    }

    public Combat_Enemy GetEnemy(int target)
    {
        if (activeEnemies.Count > target)
        {
            return activeEnemies[target].GetComponent<Combat_Enemy>();
        }
        return null;
    }

    public GameObject GetTargetedEnemyObject()
    {
        if (activeEnemies.Count > targetedEnemy)
        {
            return activeEnemies[targetedEnemy];
        }
        return null;
    }

    public GameObject GetHoveredEnemyObject()
    {
        if (activeEnemies.Count > hoveredEnemy)
        {
            return activeEnemies[hoveredEnemy];
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

    public void SetBoard(Board_Controller _val)
    {
        board = _val;
    }

    public void SetCombatUI(Combat_UI _val)
    {
        combatUI = _val;
    }

    public bool GetMoveQueueLock()
    {
        return moveQueueLock;
    }

    public bool AddToMoveCombo()
    {
        moveCombo++;
        if (moveCombo > highestMoveCombo)
        {
            highestMoveCombo = moveCombo;
            comboCounter.text = "Highest Combo: " + highestMoveCombo;
            if (highestMoveCombo >= 4)
            {
                comboCounter.text += " Good Job!";
            }
            return true;
        }
        return false;
    }

    public void ResetCombo()
    {
        moveCombo = 0;
    }

    public void PlayEnemyAnimation(AnimDetails a)
    {
        if (activeEnemies.Count > a.enemy && activeEnemies[a.enemy] != null)
        {
            if (a.rotation >= 0)
            {
                activeEnemies[a.enemy].GetComponent<Enemy_Visuals>().PlayAnimationRotated(a.ea, a.rotation);
            } 
            else if (!a.color.Equals(Vector3.zero))
            {
                activeEnemies[a.enemy].GetComponent<Enemy_Visuals>().PlayAnimationColor(a.ea, a.color);
            }
            else 
            {
                activeEnemies[a.enemy].GetComponent<Enemy_Visuals>().PlayAnimation(a.ea);
            }
        }
    }
}

public class AnimDetails
{
    public EnemyAnimation ea;
    public int enemy;
    public float rotation;
    public Color color;

    AnimDetails(EnemyAnimation ea, int enemy, float rotation, Color color)
    {
        this.ea = ea;
        this.enemy = enemy;
        this.rotation = rotation;
        this.color = color;
    }

    public static AnimDetails Anim(EnemyAnimation ea, int enemy)
    {
        return new AnimDetails(ea, enemy, -99, Color.black);
    }

    public static AnimDetails Anim(EnemyAnimation ea, int enemy, float rotation)
    {
        return new AnimDetails(ea, enemy, rotation, Color.black);
    }

    public static AnimDetails Anim(EnemyAnimation ea, int enemy, Color color)
    {
        return new AnimDetails(ea, enemy, -99, color);
    }

    public static AnimDetails Anim(EnemyAnimation ea, int enemy, float rotation, Color color)
    {
        return new AnimDetails(ea, enemy, rotation, color);
    }
}