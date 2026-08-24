using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Combat_Commands
{
    public static void BoardChanged()
    {
        GameManager.instance.combat.BoardChanged();
    }

    public static float GetBoost()
    {
        return GameManager.instance.combat.GetBoost();
    }

    public static bool MoveQueueRunning()
    {
        return GameManager.instance.combat.MoveQueueRunning();
    }

    public static bool InteractionLocked()
    {
        return MoveQueueRunning() || GameManager.instance.combat.board.MouseLock;
    }

    public static GameObject GetTargetedEnemyObject()
    {
        return GameManager.instance.combat.GetTargetedEnemyObject();
    }

    public static int GetTargetedEnemyNumber()
    {
        return GameManager.instance.combat.GetTargetedNumber();
    }

    public static Enemy_Visuals GetTargetedEnemyVisuals()
    {
        return GetTargetedEnemyObject().GetComponent<Enemy_Visuals>();
    }

    public static Enemy_Visuals GetEnemyVisuals(int enemyNum)
    {
        return GameManager.instance.combat.GetEnemy(enemyNum).GetSpriteInfo();
    }

    public static Vector2 GetBodyPart(BodyPart target, int enemyNum)
    {
        return GetEnemyVisuals(enemyNum).GetBodyPosition(target);
    }

    public static Vector2 GetTargetedBodyPart(BodyPart target)
    {
        return GetTargetedEnemyVisuals().GetBodyPosition(target);
    }

    public static Vector2 GetTargetedCenter()
    {
        return GetTargetedEnemyVisuals().GetCenter();
    }

    public static List<int> LivingEnemies()
    {
        List<int> list = GameManager.instance.combat.GetEnemies().Where(e => e.IsAlive()).Select(e => e.GetPosition()).ToList();
        return list;
    }

    public static List<int> LivingPlayers()
    {
        List<int> list = GameManager.instance.party.Players().Where(p => p.Status.IsAlive).Select(p => p.Position).ToList();
        return list;
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
