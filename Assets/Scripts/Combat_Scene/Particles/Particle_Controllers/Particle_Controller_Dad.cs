using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Particle_Controller_Dad : MonoBehaviour
{
    protected Move_Dad father;

    // List to keep track of the particles
    protected List<GameObject> particleList = new List<GameObject>();
    
    // Which enemies the attack is targeting
    protected List<MoveResult> targets = new List<MoveResult>();

    private bool started = false;

    public abstract IEnumerator Activate();
    public abstract void Cleanup();
    public abstract bool ControllerActive();

    public virtual void Setup(Move_Dad papa, List<MoveResult> targets)
    {
        father = papa;
        father.AddController(this);
        this.targets = targets;
        started = true;
    }

    private void LateUpdate()
    {
        if (started && !ControllerActive())
        {
            Cleanup();
        }
    }

    public virtual void SendAnimation(AnimDetails a)
    {
        GameManager.instance.combat.PlayActorAnimation(a);
    }

    public virtual void SendTempDamage(int damage, int target)
    {
        GameManager.instance.combat.GetEnemy(target).TakeDisplayDamage(damage);
    }

    public void AddParticle(GameObject newParticle)
    {
        particleList.Add(newParticle);
    }

    public bool RemoveParticle(GameObject deadParticle)
    {
        particleList.Remove(deadParticle);
        return particleList.Count <= 0;
    }
}
