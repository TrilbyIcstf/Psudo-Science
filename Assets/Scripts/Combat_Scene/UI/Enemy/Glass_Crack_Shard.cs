using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass_Crack_Shard : MonoBehaviour
{
    private Vector3 direction;
    private float speed = 2.0f;

    private bool started = false;

    public void Instantiate(float angle)
    {
        float radAngle = Mathf.Deg2Rad * angle;
        this.direction = new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle));
        this.started = true;
    }

    public void Update()
    {
        if (this.started)
        {
            transform.position = transform.position + (this.direction * this.speed);
            this.speed = this.speed / 0.98f;
        }
    }
}
