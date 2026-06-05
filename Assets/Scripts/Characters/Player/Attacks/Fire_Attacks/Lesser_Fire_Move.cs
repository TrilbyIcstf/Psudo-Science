using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesser_Fire_Move : Move_Dad
{
    private bool attackStarted = false;
    private GameObject target;
    private BodyPart targetPart = BodyPart.BODY;

    public void Setup(GameObject tar)
    {
        target = tar;
    }

    // Move effects
    public override float PotencyCalc(Player_Information pi, int target, Move_Information mi)
    {
        return (int)mi.Potency;
    }

    public override bool ApplyMove(Player_Information pi, int target, Move_Information mi, float potency)
    {
        GameManager.instance.combat.ProcessPlayerAttackDamage(target, (int)potency);
        return true;
    }

    // Particles/Animations
    public override void StartAttack(PC user)
    {
        mainParticleController = Instantiate(mainParticleController);
        mainParticleController.GetComponent<Lesser_Fire_Particle_Controller>().Setup(Combat_UI_Commands.GetPlayerPosition(user).position, (Vector2)Combat_Commands.GetTargetedBodyPart(targetPart), this, new List<int> {Combat_Commands.GetTargetedEnemyNumber()});
        GameManager.instance.fx.AddParticleManager(mainParticleController);
        attackStarted = true;
    }

    public override void EndAttack(PC user) { }

    public override bool IsMoveFinished()
    {
        return attackStarted && particleControllerList.Count <= 0;
    }
}
