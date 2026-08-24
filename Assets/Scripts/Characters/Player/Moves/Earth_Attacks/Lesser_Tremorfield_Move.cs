using System.Collections.Generic;
using UnityEngine;

public class Lesser_Tremorfield_Move : Generic_Player_Attack_Move
{
    private BodyPart targetPart = BodyPart.LEGS;

    public override List<MoveResult> ResultsCalc(Player_Information pi, int target, Move_Information mi)
    {
        List<int> targets = Combat_Commands.LivingEnemies();
        List<MoveResult> results = new List<MoveResult>();
        foreach (int t in targets)
        {
            results.Add(TargetCalc(pi, t, mi));
        }
        return results;
    }

    // Particles/Animations
    public override void StartMove(int user, List<MoveResult> results)
    {
        foreach (MoveResult result in results)
        {
            GameObject tempParticleController = Instantiate(mainParticleController);
            tempParticleController.GetComponent<Bullet_Spray_Particle_Controller>().Setup(Combat_UI_Commands.GetPlayerPosition(user).position, (Vector2)Combat_Commands.GetBodyPart(targetPart, result.targetNum), this, new List<MoveResult>() { result }, 3, result.potency);
            GameManager.instance.fx.AddParticleManager(tempParticleController);
        }
        moveStarted = true;
    }

    public override void EndMove(int user) { }

    public override bool IsMoveFinished()
    {
        return moveStarted && particleControllerList.Count <= 0;
    }
}
