using System.Collections.Generic;
using UnityEngine;

public abstract class Move_Dad : MonoBehaviour
{
    protected List<Particle_Controller_Dad> particleControllerList = new List<Particle_Controller_Dad>();

    [SerializeField]
    protected GameObject mainParticleController;

    protected bool moveStarted = false;

    public virtual float? DelayOverride { get; } = null;

    // Section for handling animations and particles
    public abstract void StartMove(int user, List<MoveResult> results);
    public abstract void EndMove(int user);
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
