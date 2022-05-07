using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Burst : MonoBehaviour
{
    private ParticleSystem PS;

    // Start is called before the first frame update
    void Start()
    {
        PS = GetComponent<ParticleSystem>();

        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
