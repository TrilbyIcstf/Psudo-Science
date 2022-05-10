using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Burst : MonoBehaviour
{
    private ParticleSystem PS;

    public void Activate(Color _tint)
    {
        PS = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule tempMain = PS.main;
        tempMain.startColor = _tint;
        PS.Play();

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
