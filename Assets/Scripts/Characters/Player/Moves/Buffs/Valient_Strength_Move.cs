using System.Collections.Generic;
using UnityEngine;

public class Valient_Strength_Move : Player_Move
{
    [SerializeField]
    private GameObject buffParticle;

    public override bool ApplyMove(Player_Information pi, List<MoveResult> results, Move_Information mi)
    {
        foreach (MoveResult result in results)
        {
            int duration = Mathf.CeilToInt(result.potency);
            GameManager.instance.party.ApplyStatus(result.targetNum, StatusEffect.MINORPOWERUP, duration, true);
            Combat_UI_Commands.UpdateStatusIcons();
        }
        return true;
    }
    public override void EndMove(int user) { }

    public override bool IsMoveFinished()
    {
        return moveStarted && particleControllerList.Count <= 0;
    }

    public override List<MoveResult> ResultsCalc(Player_Information pi, int target, Move_Information mi)
    {
        List<MoveResult> results = new List<MoveResult>();
        List<int> party = Combat_Commands.LivingPlayers();
        foreach (int p in party)
        {
            results.Add(TargetCalc(pi, p, mi));
        }
        return results;
    }

    public override void StartMove(int user, List<MoveResult> results)
    {
        foreach (MoveResult result in results)
        {
            GameObject tempParticleController = Instantiate(mainParticleController);
            Vector2 targetPos = Combat_UI_Commands.GetPlayerPosition(result.targetNum).position;

            tempParticleController.GetComponent<Floating_Effect_Particle_Controller>().Setup(targetPos, this, results, buffParticle);
            GameManager.instance.fx.AddParticleManager(tempParticleController);
        }
        moveStarted = true;
    }

    public override MoveResult TargetCalc(Player_Information pi, int target, Move_Information mi)
    {
        int duration = Mathf.CeilToInt(mi.Potency * Combat_Commands.GetBoost());
        return new MoveResult(duration, Target.NULL, target);
    }
}
