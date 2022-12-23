using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesser_Fire_Controller : Particle_Controller_Dad
{
    // The particle gameobject
    public GameObject lesserFireParticle;

    // Variables for spawning the particles
    private int numberToSpawn = 8;
    private float spawnDelay = 0.05f;
    private float minAngle = 25;
    private float maxAngle = 60;

    public IEnumerator Activate(Vector2 spawnPosition, Vector2 goalPosition)
    {
        yield return new WaitUntil(() => !GameManager.instance.fx.CheckParticleLock());

        float initialAngle = ((Mathf.Rad2Deg * Mathf.Atan2(goalPosition.y - spawnPosition.y, goalPosition.x - spawnPosition.x)) + 360) % 360;
        Vector2[] directions = MakeSpawnAngleArray(initialAngle);
        Particle_Lesser_Spark particleScript = lesserFireParticle.GetComponent<Particle_Lesser_Spark>();

        for (int i = 0; i < numberToSpawn; i++)
        {
            GameObject tempParticle = Instantiate(lesserFireParticle, spawnPosition, Quaternion.identity);
            tempParticle.GetComponent<Particle_Lesser_Spark>().ParticleInitialize(goalPosition, 0.15f, 1.001f, directions[i], 0.05f, 0.7f);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private Vector2[] MakeSpawnAngleArray(float initialAngle)
    {
        Vector2[] angleArray = new Vector2[numberToSpawn];
        int angleDirection = 1;

        for (int i = 0; i < numberToSpawn; i++)
        {
            float newAngle = initialAngle + (Random.Range(minAngle, maxAngle) * angleDirection);
            angleArray[i] = new Vector2(Mathf.Cos(Mathf.Deg2Rad * newAngle), Mathf.Sin(Mathf.Deg2Rad * newAngle));
            angleDirection *= -1;
        }

        return angleArray;
    }
}
