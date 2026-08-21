using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating_Effect_Particle_Controller : Particle_Controller_Dad
{
    // The particle gameobject
    private GameObject particle;

    // Variables for spawning the particles
    private const int numberToSpawn = 4;
    private const float spawnWidth = 1.0f;
    private const float minSpawnHeight = 0.1f;
    private const float maxSpawnHeight = 0.4f;
    private const float lifeSpan = 0.75f;
    private const float speed = 0.07f;
    private const float accel = 0.85f;
    private Vector2 spawnPosition;

    public override IEnumerator Activate()
    {
        int sign = Random.Range(0, 2) == 0 ? 1 : -1;
        for (float i = 0; i < numberToSpawn; i++)
        {
            float posXOffset = Mathf.Lerp(-1, 1, i / (numberToSpawn - 1)) * spawnWidth;
            float posYOffset = Random.Range(minSpawnHeight, maxSpawnHeight) * sign;
            Vector2 tempSpawnPos = spawnPosition + new Vector2(posXOffset, posYOffset);
            Vector2 tempGoal = tempSpawnPos + Vector2.up;

            GameObject tempParticle = Instantiate(particle, tempSpawnPos, Quaternion.identity);
            tempParticle.GetComponent<Particle_Float>().ParticleInitialize(tempGoal, speed, accel, lifeSpan, this);

            sign = -sign;
        }

        yield return new WaitForSeconds(0.0f);
    }

    public override bool ControllerActive()
    {
        return this.particleList.Count > 0;
    }

    public override void Cleanup()
    {
        father.RemoveController(this);
        GameManager.instance.fx.RemoveParticleManager(gameObject);
        Destroy(gameObject);
    }

    public void Setup(Vector2 spawnPosition, Player_Move papa, List<MoveResult> targets, GameObject particle)
    {
        this.spawnPosition = spawnPosition;
        this.particle = particle;
        base.Setup(papa, targets);
    }
}
