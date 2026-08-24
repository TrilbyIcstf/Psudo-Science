using System.Collections.Generic;
using UnityEngine;

public class Minor_Feeble_Move : Player_Move
{
    [SerializeField]
    private GameObject debuffParticle;

    public override bool ApplyMove(Player_Information pi, List<MoveResult> results, Move_Information mi)
    {
        MoveResult result = results[0];
        int duration = Mathf.CeilToInt(result.Potency * Combat_Commands.GetBoost());
        GameManager.instance.combat.GetEnemy(result.TargetNum).AddStatusEffect(StatusEffect.MINORPOWERDOWN, duration, true);
        Combat_UI_Commands.UpdateStatusIcons();
        return true;
    }

    public override void EndMove(int user) { }

    public override bool IsMoveFinished()
    {
        return moveStarted && particleControllerList.Count <= 0;
    }

    public override List<MoveResult> ResultsCalc(Player_Information pi, int target, Move_Information mi)
    {
        return new List<MoveResult> { TargetCalc(pi, target, mi) };
    }

    public override void StartMove(int user, List<MoveResult> results)
    {
        GameObject tempParticleController = Instantiate(mainParticleController);
        Vector2 targetPos = Combat_UI_Commands.GetEnemyPosition(results[0].TargetNum);

        tempParticleController.GetComponent<Floating_Effect_Particle_Controller>().Setup(targetPos, this, results, debuffParticle);
        GameManager.instance.fx.AddParticleManager(tempParticleController);
        moveStarted = true;
    }

    public override MoveResult TargetCalc(Player_Information pi, int target, Move_Information mi)
    {
        return new MoveResult(mi.Potency, Target.NULL, target);
    }
}
