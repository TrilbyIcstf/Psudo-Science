using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesser_Fire_Particle_Controller : Particle_Controller_Dad
{
    // The particle gameobject
    public GameObject lesserFireParticle;

    // Variables for spawning the particles
    private int numberToSpawn = 4;
    private float spawnDelay = 0.1f;
    private float minAngle = 25;
    private float maxAngle = 60;
    private Vector2 spawnPosition;
    private Vector2 goalPosition;
    private int enemyNum;

    public override IEnumerator Activate()
    {
        float initialAngle = ((Mathf.Rad2Deg * Mathf.Atan2(goalPosition.y - spawnPosition.y, goalPosition.x - spawnPosition.x)) + 360) % 360;
        Vector2[] directions = MakeSpawnAngleArray(initialAngle);
        Particle_Lesser_Spark particleScript = lesserFireParticle.GetComponent<Particle_Lesser_Spark>();

        for (int i = 0; i < numberToSpawn; i++)
        {
            GameObject tempParticle = Instantiate(lesserFireParticle, spawnPosition, Quaternion.identity);
            tempParticle.GetComponent<Particle_Lesser_Spark>().ParticleInitialize(goalPosition, targets[0], 0.2f, 1.001f, directions[i], 0.05f, 0.7f, this);
            yield return new WaitForSeconds(spawnDelay);
        }

        yield return new WaitUntil(() => particleList.Count <= 0);

        father.RemoveController(this);
        GameManager.instance.fx.RemoveParticleManager(gameObject);
        Destroy(gameObject);
    }

    public void Setup(Vector2 sp, Vector2 gp, Move_Dad papa, List<int> targets)
    {
        base.Setup(papa, targets);
        spawnPosition = sp;
        goalPosition = gp;
    }

    private Vector2[] MakeSpawnAngleArray(float initialAngle)
    {
        Vector2[] angleArray = new Vector2[numberToSpawn];
        int angleDirection = 1;
        float angleAdjust = Vector2.Distance(spawnPosition, goalPosition) <= 7.5f ? 2 : 1;

        for (int i = 0; i < numberToSpawn; i++)
        {
            float newAngle = initialAngle + ((Random.Range(minAngle, maxAngle) / angleAdjust) * angleDirection);
            angleArray[i] = new Vector2(Mathf.Cos(Mathf.Deg2Rad * newAngle), Mathf.Sin(Mathf.Deg2Rad * newAngle));
            angleDirection *= -1;
        }

        return angleArray;
    }

    public override bool ControllerActive()
    {
        return particleList.Count > 0;
    }
}
