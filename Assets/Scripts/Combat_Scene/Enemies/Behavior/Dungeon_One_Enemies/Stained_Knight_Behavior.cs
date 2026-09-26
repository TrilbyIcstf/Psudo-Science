using System.Collections.Generic;
using UnityEngine;

public class Stained_Knight_Behavior : Behavior_Dad
{
    public override (GameObject, TargetingType, int, int, float) MakeMove()
    {
        step++;

        EnemyMoveIntent currentIntent = intent;

        int speed = 0;
        switch (varient)
        {
            case 0 when step % 2 == 0:
            case 1 when step % 2 == 1:
                speed = 3;
                intent = new EnemyMoveIntent(EnemyMoveName.BasicSlash, MoveType.PHYSICAL, TargetingType.LowestHealth, 1, 125);
                break;
            case 1 when step % 2 == 0:
            case 0 when step % 2 == 1:
                speed = 6;
                intent = new EnemyMoveIntent(EnemyMoveName.SparkBurst, MoveType.MAGICAL, TargetingType.LowestHealth, 2, 85);
                break;
            default: throw new System.NotImplementedException();
        }

        GameObject moveObject = GameManager.instance.ll.enemyMoveRepository.GetValue(currentIntent.Move);

        return (moveObject, currentIntent.TargetingType, currentIntent.Targets, speed, currentIntent.Potency);
    }

    protected override int GetBaseSpeed()
    {
        switch (varient)
        {
            case 0: return 3;
            case 1: return 6;
            default: return 0;
        }
    }

    public override int GenerateBaseIntent()
    {
        int speed = 0;
        switch (varient)
        {
            case 0:
                speed = 3;
                intent = new EnemyMoveIntent(EnemyMoveName.BasicSlash, MoveType.PHYSICAL, TargetingType.LowestHealth, 1, 125);
                break;
            case 1:
                speed = 6;
                intent = new EnemyMoveIntent(EnemyMoveName.SparkBurst, MoveType.MAGICAL, TargetingType.LowestHealth, 2, 85);
                break;
            default: throw new System.NotImplementedException();
        }
        return speed;
    }
}
