using System.Collections.Generic;
using UnityEngine;

public class Stained_Knight_Behavior : Behavior_Dad
{
    public override (GameObject, TargetingType, int, int, float) MakeMove()
    {
        GameObject slashMove = GameManager.instance.ll.enemyMoveRepository.GetValue(EnemyMoveName.BasicSlash);

        switch (varient)
        {
            case 0:
                {
                    return (slashMove, TargetingType.LowestHealth, 1, 3, 125);
                }
            case 1:
                {
                    return (slashMove, TargetingType.LowestHealth, 2, 6, 85);
                }
            default: throw new System.NotImplementedException();
        }
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
}
