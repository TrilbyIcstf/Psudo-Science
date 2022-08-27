using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Startup_Spoof : MonoBehaviour
{
    public Encounter testEnemies;
    public GameObject testParticle;

    private void Start()
    {
        GameManager.instance.combat.CombatSetup(testEnemies);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A down");
            GameObject tempParticle = Instantiate(testParticle, new Vector3(-3, -3, 0), Quaternion.identity);
            tempParticle.GetComponent<Particle_Lesser_Spark>().ParticleInitialize(new Vector2(3, 3), 0.15f, 0, new Vector2(Random.Range(-1,1), Random.Range(-1,1)));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector2(3, 3), 0.5f);
    }
}
