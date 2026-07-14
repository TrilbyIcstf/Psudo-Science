using System.Collections.Generic;
using UnityEngine;

public struct QueuedMove
{
    public GameObject move;

    public PC user;

    public QueuedMove(GameObject move, PC user)
    {
        this.move = move;
        this.user = user;
    }
}

public struct QueuedEnemyMove
{
    public GameObject move;

    public int user;

    public List<int> targets;

    public QueuedEnemyMove(GameObject move, int user, List<int> targets)
    {
        this.move = move;
        this.user = user;
        this.targets = targets;
    }
}

public struct MoveResult
{
    public float potency;

    public Target targetType;
    public int targetNum;

    public MoveResult(float potency, Target targetType, int targetNum)
    {
        this.potency = potency;
        this.targetType = targetType;
        this.targetNum = targetNum;
    }
}