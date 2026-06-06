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
        return GameManager.instance.combat.GetTargetedEnemyObject();
    }

    public static int GetTargetedEnemyNumber()
    {
        return GameManager.instance.combat.GetTargetedNumber();
    }

    public static Enemy_Visuals GetTargetedEnemyVisuals()
    {
        return GetTargetedEnemy().GetComponent<Enemy_Visuals>();
    }

    public static Vector2 GetTargetedBodyPart(BodyPart target)
    {
        return GetTargetedEnemyVisuals().GetBodyPosition(target);
    }

    public static Vector2 GetTargetedCenter()
    {
        return GetTargetedEnemyVisuals().GetCenter();
    }

    public static string GetPath(this Transform current)
    {
        if (current.parent == null)
        {
            return "/" + current.name;
        }
        return current.parent.GetPath() + "/" + current.name;
    }
}
