using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar_Test : MonoBehaviour
{
    private Slider slide;

    // Start is called before the first frame update
    void Start()
    {
        slide = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slide.value = Mathf.Clamp((float)GameManager.instance.combat.energy.BluePoints / GameManager.instance.combat.energy.BlueCap, 0, 1);
    }
}
