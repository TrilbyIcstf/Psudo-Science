using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Combat_Commands
{
    public static bool GetMoveQueueLock()
    {
        return GameManager.instance.combat.GetMoveQueueLock();
    }

    public static GameObject GetTargetedEnemy()
    {
        return GameManager.instance.combat.GetTargetedEnemy();
    }
}
