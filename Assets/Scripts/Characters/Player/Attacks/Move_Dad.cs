using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move_Dad : MonoBehaviour
{
    public Move_Information moveInfo;

    protected List<Particle_Controller_Dad> particleControllerList = new List<Particle_Controller_Dad>();

    public GameObject mainParticleController;

    public abstract void StartAttack(PC user);

    public abstract void EndAttack(PC user);
    public abstract bool MoveFinished();

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
