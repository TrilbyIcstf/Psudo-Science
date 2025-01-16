using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass_Crack : MonoBehaviour
{
    public GameObject glassShard;

    private const int NUMBER_OF_SHARDS = 6;

    // Start is called before the first frame update
    void Start()
    {
        float angleIncrement = 360 / NUMBER_OF_SHARDS;
        for (int i = 0; i < NUMBER_OF_SHARDS; i++)
        {
            float randomAngle = Random.Range(angleIncrement * i, angleIncrement * (i + 1));
            GameObject newShard = Instantiate(glassShard, this.transform);
            newShard.GetComponent<Glass_Crack_Shard>().Instantiate(randomAngle);
        }
    }
}
