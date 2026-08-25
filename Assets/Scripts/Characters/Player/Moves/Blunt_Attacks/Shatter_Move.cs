using System.Collections.Generic;
using UnityEngine;

public class Shatter_Move : Generic_Player_Attack_Move
{
    public override MoveResult TargetCalc(Player_Information pi, int target, Move_Information mi)
    {
        Enemy_Stats targetStats = GameManager.instance.combat.GetEnemy(target).GetStats();
        List<Element> weakness = targetStats.Weakness;
        List<Element> strength = targetStats.Strength;
        Effectiveness effectiveness = mi.Element.Evaluate(weakness, strength);

        float result = DamageCalc(mi.AdjustedPotency, pi.Power, 2.0f, targetStats.Defense, 0.35f, effectiveness);

        return new MoveResult(result, Target.ENEMY, target, effectiveness);
    }

    public override void EndMove(int user) { }

    public override bool IsMoveFinished()
    {
        return moveStarted && particleControllerList.Count <= 0;
    }

    public override void StartMove(int user, List<MoveResult> results)
    {
        GameObject tempParticleController = Instantiate(mainParticleController);
        tempParticleController.GetComponent<Effect_Overlay_Controller>().Setup(this, results, DamageTimes(), AnimationTimes());
        GameManager.instance.fx.AddParticleManager(tempParticleController);

        moveStarted = true;
    }

    private List<float> DamageTimes()
    {
        return new List<float>() { 0.3f };
    }

    private Dictionary<float, AnimDetails> AnimationTimes()
    {
        Dictionary<float, AnimDetails> animDict = new Dictionary<float, AnimDetails>();
        animDict[0.3f] = new AnimDetails(CombatAnimation.SmallShake, Target.NULL, -1, null, Color.red);
        return animDict;
    }
}
