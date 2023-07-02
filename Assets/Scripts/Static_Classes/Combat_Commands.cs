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

    public static Vector2 GetTargetedBodyPart(BodyPart target)
    {
        return GetTargetedEnemy().GetComponent<Enemy_Visuals>().GetBodyPosition(target);
    }

    public static Vector2 GetTargetedCenter()
    {
        return GetTargetedEnemy().GetComponent<Enemy_Visuals>().GetCenter();
    }
}
