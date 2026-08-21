using System.Collections.Generic;
using UnityEngine;

public class Lesser_Heal_Move : Player_Move
{
    [SerializeField]
    private GameObject healParticle;

    // Move Effects
    public override bool ApplyMove(Player_Information pi, List<MoveResult> results, Move_Information mi)
    {
        MoveResult result = results[0];
        GameManager.instance.party.SingleHeal(result.targetNum, (int)result.potency);
        Combat_UI_Commands.RefreshHealthBars();
        return true;
    }

    public override List<MoveResult> ResultsCalc(Player_Information pi, int target, Move_Information mi)
    {
        List<MoveResult> results = new List<MoveResult>();
        results.Add(TargetCalc(pi, GameManager.instance.party.MostDamaged(), mi));
        return results;
    }

    public override MoveResult TargetCalc(Player_Information pi, int target, Move_Information mi)
    {
        float result = mi.AdjustedPotency * ((pi.Intelligence + pi.Resistance) / 2);
        result = result * Combat_Commands.GetBoost();
        return new MoveResult(result, Target.PC, target);
    }

    // Particles/Animations
    public override void StartMove(int user, List<MoveResult> results)
    {
        GameObject tempParticleController = Instantiate(mainParticleController);
        Vector2 targetPos = Combat_UI_Commands.GetPlayerPosition(results[0].targetNum).position;

        tempParticleController.GetComponent<Floating_Effect_Particle_Controller>().Setup(targetPos, this, results, healParticle);
        GameManager.instance.fx.AddParticleManager(tempParticleController);
        moveStarted = true;
    }

    public override void EndMove(int user) { }

    public override bool IsMoveFinished()
    {
        return moveStarted && particleControllerList.Count <= 0;
    }
}
