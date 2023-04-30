using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    // List of all active particle managers in the scene
    public List<GameObject> activeParticleManagers = new List<GameObject>();

    /// <summary>
    /// Checks if there are any active particle effects in the current screen.
    /// </summary>
    /// <returns>
    /// True if any particles are currently active, false otherwise.
    /// </returns>
    public bool CheckParticleLock()
    {
        return GameObject.FindGameObjectsWithTag("ParticleEffect").Length > 0 || GameObject.FindGameObjectsWithTag("Blip").Length > 0;
    }

    /// <summary>
    /// Checks if there is a particle manager active in the current screen.
    /// </summary>
    /// <returns>
    /// True if any particle managers are currently active, false otherwise.
    /// </returns>
    public bool CheckManagerLock()
    {
        return activeParticleManagers.Count > 0;
    }

    /// <summary>
    /// A combination of all the lock checks.
    /// </summary>
    /// <returns>
    /// True if any lock is true, false otherwise.
    /// </returns>
    public bool CheckAllFXLock()
    {
        return (CheckParticleLock() || CheckManagerLock());
    }

    /// <summary>
    /// Adds a new particle manager to the list.
    /// </summary>
    /// <param name="pm">
    /// The particle manager to add to the list.
    /// </param>
    public void AddParticleManager(GameObject pm)
    {
        this.activeParticleManagers.Add(pm);
        Particle_Controller_Dad tempController = pm.GetComponent<Particle_Controller_Dad>();
        StartCoroutine(tempController.Activate());
    }

    public IEnumerator AddParticleManagerAndWaitToFinish(GameObject pm)
    {
        this.activeParticleManagers.Add(pm);
        Particle_Controller_Dad tempController = pm.GetComponent<Particle_Controller_Dad>();
        StartCoroutine(tempController.Activate());
        yield return new WaitUntil(() => !tempController.ControllerActive());
    }

    /// <summary>
    /// Removes the passed in particle manager from the active list.
    /// </summary>
    /// <param name="pm">
    /// The particle manager to remove.
    /// </param>
    public void RemoveParticleManager(GameObject pm)
    {
        this.activeParticleManagers.Remove(pm);
    }
}
