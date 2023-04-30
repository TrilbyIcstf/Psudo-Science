using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Particle_Controller_Dad : MonoBehaviour
{
    protected Move_Dad father;

    // List to keep track of the particles
    protected List<GameObject> particleList = new List<GameObject>();

    public abstract IEnumerator Activate();

    public virtual void Setup(Move_Dad papa)
    {
        father = papa;
        father.AddController(this);
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
