using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move_Dad : MonoBehaviour
{
    public Move_Information moveInfo;

    protected List<Particle_Controller_Dad> particleControllerList = new List<Particle_Controller_Dad>();

    public GameObject mainParticleController;

    // Section for handling move effects
    public abstract float PotencyCalc(Player_Information pi, int target, Move_Information mi);
    public abstract bool ApplyMove(Player_Information pi, int target, Move_Information mi, float potency);

    // Section for handling animations and particles
    public abstract void StartAttack(PC user);
    public abstract void EndAttack(PC user);
    public abstract bool IsMoveFinished();

    public void AddController(Particle_Controller_Dad newController)
    {
        particleControllerList.Add(newController);
    }

    public bool RemoveController(Particle_Controller_Dad deadController)
    {
        particleControllerList.Remove(deadController);
        return particleControllerList.Count <= 0;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
