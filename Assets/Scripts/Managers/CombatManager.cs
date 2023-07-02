using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // REMOVE ME LATER

public class CombatManager : MonoBehaviour
{
    // Testing vars
    public GameObject testMove;
    public Text comboCounter;

    public Board_Controller board;
    public Combat_UI combatUI;
    public Player_Energy energy;

    // The enemies in the current encounter
    [SerializeField] private List<GameObject> activeEnemies;

    // The enemy currently targeted by the player
    private int targetedEnemy = 0;
    private int hoveredEnemy = 0;

    private Queue<QueuedMove> moveQueue = new Queue<QueuedMove>();
    private bool moveQueueActive = false;
    private bool moveQueueLock = false;

    // The delay between different moves in the move queue
    private float moveQueueDelay = 1.5f;

    // A tracker for how many moves you've triggered in one turn.
    private int moveCombo = 0;
    private int highestMoveCombo = 0;

    public void CombatSetup(Encounter _enc)
    {
        energy = new Player_Energy();
        energy.TestMove = testMove; // REMOVE ME
        Transform enemyHolderPos = GameObject.FindGameObjectWithTag("EnemyHolder").transform;

        for (int i = 0; i < _enc.EncounterContents.Count; i++)
        {
            activeEnemies.Add(Instantiate(_enc.EncounterContents[i], enemyHolderPos));
            activeEnemies[i].GetComponent<Combat_Enemy>().Setup(i);
            activeEnemies[i].transform.position += _enc.EnemyPositions[i];
        }

        if (activeEnemies.Count > 0)
        {
            TargetEnemy(0);
        }
    }

    public void CombatCleanup()
    {
        energy = null;
        combatUI = null;
        board = null;
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
        moveQueueLock = true;
        yield return new WaitUntil(() => !GameManager.instance.fx.CheckAllFXLock());
        yield return new WaitForSeconds(1.0f);

        StartQueue();
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

        while(moveQueue.Count > 0 && moveQueueActive)
        {
            QueuedMove nextMove = moveQueue.Dequeue();
            GameObject nextMoveObject = Instantiate(nextMove.move);
            Move_Dad nextScript = nextMoveObject.GetComponent<Move_Dad>();
            nextScript.StartAttack(nextMove.user);
            yield return new WaitUntil(() => nextScript.MoveFinished());
            nextScript.Destroy();
            if (moveQueue.Count > 0)
            {
                yield return new WaitForSeconds(moveQueueDelay);
            }
        }
        yield return new WaitForSeconds(1.0f);
        moveQueueLock = false;
        Debug.Log("Queue done");
    }

    public bool TargetEnemy(int enemy)
    {
        targetedEnemy = enemy;
        combatUI.TargetCrosshair(GetTargetedEnemy().GetComponent<Enemy_Visuals>().GetCenter());
        combatUI.SetCrosshairEnabled(true);
        UnhoverEnemy(enemy);
        return true;
    }

    public bool HoverEnemy(int enemy)
    {
        if (enemy == targetedEnemy)
        {
            return false;
        }
        hoveredEnemy = enemy;
        combatUI.HoverCrosshair(GetHoveredEnemy().GetComponent<Enemy_Visuals>().GetCenter());
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

    public GameObject GetTargetedEnemy()
    {
        if (activeEnemies.Count >= targetedEnemy)
        {
            return activeEnemies[targetedEnemy];
        }
        return null;
    }

    public GameObject GetHoveredEnemy()
    {
        if (activeEnemies.Count >= hoveredEnemy)
        {
            return activeEnemies[hoveredEnemy];
        }
        return null;
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
}
