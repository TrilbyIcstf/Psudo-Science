using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Child_Test : Behavior_Dad
{
    public override void MakeAttack()
    {
        Debug.Log("Child attack!");
    }
}
