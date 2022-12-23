using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    /// <summary>
    /// Checks if there are any active particle effects in the current screen.
    /// </summary>
    /// <returns>
    /// True if any particles are currently active, false otherwise.
    /// </returns>
    public bool CheckParticleLock()
    {
        return GameObject.FindGameObjectsWithTag("ParticleEffect").Length > 0;
    }

    /// <summary>
    /// A combination of all the lock checks.
    /// </summary>
    /// <returns>
    /// True if any lock is true, false otherwise.
    /// </returns>
    public bool CheckAllFXLock()
    {
        return (CheckParticleLock());
    }
}
