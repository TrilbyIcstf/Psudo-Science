using System.Collections.Generic;
using UnityEngine;

public class Basic_Element_Burst_Move : Generic_Enemy_Attack_Move
{
    [SerializeField]
    private Element moveElement;

    public override MoveType GetMoveType()
    {
        return MoveType.MAGICAL;
    }

    public override Element GetElement()
    {
        return moveElement;
    }

    // Particles/Animations
    public override void StartMove(int user, List<MoveResult> results)
    {
        GameObject tempParticleController = Instantiate(mainParticleController);
        tempParticleController.GetComponent<Particle_System_Controller>().Setup(this, results);
        GameManager.instance.fx.AddParticleManager(tempParticleController);

        moveStarted = true;
    }

    public override void EndMove(int user) { }

    public override bool IsMoveFinished()
    {
        return moveStarted && particleControllerList.Count <= 0;
    }
}
