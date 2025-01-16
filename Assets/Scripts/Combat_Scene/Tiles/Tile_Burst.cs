using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Burst : MonoBehaviour
{
    // The blip object that carries energy
    public GameObject normalBlip;

    // The object's particle system to release sparkles
    private ParticleSystem PS;

    public void Activate(TColor _tint, int _blips, float _totalVal)
    {
        PS = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule tempMain = PS.main;
        tempMain.startColor = Color_Vals.GetColorVal(_tint);
        PS.Play();

        if (Combat_UI_Commands.IsBlipColor(_tint))
        {
            if (_tint == TColor.BLUE || _tint == TColor.ORANGE || _tint == TColor.PINK || _tint == TColor.PURPLE)
            {
                // Spawns normal blips equal to the passed in blip value
                for (int i = 0; i < _blips; i++)
                {
                    GameObject tempBlip = Instantiate(normalBlip, transform.position, Quaternion.identity);
                    tempBlip.GetComponent<Energy_Blip>().Activate(_tint, (int)_tint + 1, Combat_UI_Commands.GetEnergyBarPos(_tint), _totalVal / (float)_blips);
                }
            } 
            else if (_tint == TColor.GREEN)
            {
                // Spawns one blip for each player, regardless of passed in value
                for (int i = 1; i <= 4; i++)
                {
                    GameObject tempBlip = Instantiate(normalBlip, transform.position, Quaternion.identity);
                    tempBlip.GetComponent<Energy_Blip>().Activate(_tint, i, Combat_UI_Commands.GetHealthBarPos(i), _totalVal);
                }
            }
            else if (_tint == TColor.BLACK)
            {
                // Spawns one blip for each player, regardless of passed in value
                for (int i = 1; i <= 4; i++)
                {
                    GameObject tempBlip = Instantiate(normalBlip, transform.position, Quaternion.identity);
                    tempBlip.GetComponent<Energy_Blip>().Activate(_tint, i, Combat_UI_Commands.GetEnergyBarPos(i), _totalVal/5.0f);
                }
            }
        }

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
