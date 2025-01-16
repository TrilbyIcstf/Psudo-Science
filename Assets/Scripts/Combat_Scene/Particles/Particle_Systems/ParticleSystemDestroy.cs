using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDestroy : MonoBehaviour
{
    private ParticleSystem ps;
    private bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ps != null)
        {
            if (!started && ps.isPlaying)
            {
                started = true;
            }

            if (started && !ps.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}
