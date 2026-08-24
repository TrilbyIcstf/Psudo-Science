using System.Collections.Generic;
using UnityEngine;

public class Warm_Up_Move : Player_Move
{
    [SerializeField]
    private GameObject buffParticle;

    public override bool ApplyMove(Player_Information pi, List<MoveResult> results, Move_Information mi)
    {
        MoveResult result = results[0];
        int duration = Mathf.CeilToInt(result.potency * Combat_Commands.GetBoost());
        GameManager.instance.party.ApplyStatus(result.targetNum, StatusEffect.WARMUP, duration, true);
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
        Vector2 targetPos = Combat_UI_Commands.GetPlayerPosition(results[0].targetNum).position;

        tempParticleController.GetComponent<Floating_Effect_Particle_Controller>().Setup(targetPos, this, results, buffParticle);
        GameManager.instance.fx.AddParticleManager(tempParticleController);
        moveStarted = true;
    }

    public override MoveResult TargetCalc(Player_Information pi, int target, Move_Information mi)
    {
        return new MoveResult(mi.Potency, Target.NULL, pi.Position);
    }
}
