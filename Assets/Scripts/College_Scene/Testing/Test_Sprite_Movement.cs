using UnityEngine;

public class Test_Sprite_Movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = gameObject.transform.position;
        pos.x += 0.05f;
        gameObject.transform.position = pos;
    }
}
