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