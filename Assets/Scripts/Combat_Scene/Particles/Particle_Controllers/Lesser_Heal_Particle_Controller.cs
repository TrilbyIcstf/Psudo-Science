using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesser_Heal_Particle_Controller : Particle_Controller_Dad
{
    // The particle gameobject
    [SerializeField]
    private GameObject lesserHealParticle;

    // Variables for spawning the particles
    private const int numberToSpawn = 4;
    private const float spawnWidth = 1.0f;
    private const float spawnHeight = 0.4f;
    private const float lifeSpan = 0.75f;
    private const float speed = 0.07f;
    private const float accel = 0.85f;
    private Vector2 spawnPosition;

    public override IEnumerator Activate()
    {
        for (float i = 0; i < numberToSpawn; i++)
        {
            float posXOffset = Mathf.Lerp(-1, 1, i / (numberToSpawn - 1)) * spawnWidth;
            float posYOffset = Random.Range(-spawnHeight, spawnHeight);
            Vector2 tempSpawnPos = spawnPosition + new Vector2(posXOffset, posYOffset);
            Vector2 tempGoal = tempSpawnPos + Vector2.up;

            GameObject tempParticle = Instantiate(lesserHealParticle, tempSpawnPos, Quaternion.identity);
            tempParticle.GetComponent<Particle_Float>().ParticleInitialize(tempGoal, speed, accel, lifeSpan, this);
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

    public void Setup(Vector2 spawnPosition, Move_Dad papa, List<MoveResult> targets)
    {
        this.spawnPosition = spawnPosition;
        base.Setup(papa, targets);
    }
}
