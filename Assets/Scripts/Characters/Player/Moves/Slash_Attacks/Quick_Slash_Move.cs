using System.Collections.Generic;
using UnityEngine;

public class Quick_Slash_Move : Generic_Player_Attack_Move
{
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
        return new List<float>() { 0.6f };
    }

    private Dictionary<float, AnimDetails> AnimationTimes()
    {
        Dictionary<float, AnimDetails> animDict = new Dictionary<float, AnimDetails>();
        animDict[0.6f] = new AnimDetails(CombatAnimation.ColorFlash, Target.NULL, -1, null, Color.red);
        return animDict;
    }
}
