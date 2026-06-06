using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Particle_Controller_Dad : MonoBehaviour
{
    protected Move_Dad father;

    // List to keep track of the particles
    protected List<GameObject> particleList = new List<GameObject>();
    
    // Which enemies the attack is targeting
    protected List<int> targets = new List<int>();

    public abstract IEnumerator Activate();

    public virtual void Setup(Move_Dad papa, List<int> targets)
    {
        father = papa;
        father.AddController(this);
        this.targets = targets;
    }

    public virtual void SendAnimation(AnimDetails a)
    {
        GameManager.instance.combat.PlayEnemyAnimation(a);
    }

    public virtual void SendTempDamage(int damage, int target)
    {
        GameManager.instance.combat.GetEnemy(target).TakeDisplayDamage(damage);
    }

    public abstract bool ControllerActive();

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
