using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Dad : MonoBehaviour
{
    [SerializeField]
    protected int baseSpeed = 0;
    public int BaseSpeed { get => baseSpeed; }

    public virtual void MakeAttack()
    {
        Debug.Log("Behavior Dad Attack");
    }
}
