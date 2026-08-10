using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Burst : MonoBehaviour
{
    // The blip object that carries energy
    public GameObject normalBlip;

    // The object's particle system to release sparkles
    private ParticleSystem PS;

    public void Activate(TColor _tint, int _blips, int _pointVal, int _reviveVal)
    {
        PS = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule tempMain = PS.main;
        tempMain.startColor = Color_Vals.GetColorVal(_tint);
        PS.Play();

        if (Combat_UI_Commands.IsBlipColor(_tint))
        {
            if (_tint.IsPlayer())
            {
                bool isDead = GameManager.instance.party.GetPlayer(_tint).Status.IsDead;
                Transform barPos = isDead ? Combat_UI_Commands.GetReviveBarPos((int)_tint) : Combat_UI_Commands.GetEnergyBarPos(_tint);
                int potency = isDead ? _reviveVal : _pointVal;
                
                int remainder = potency % _blips;
                int blipPotency = Mathf.FloorToInt(potency / _blips);

                // Spawns normal blips equal to the passed in blip value
                for (int i = 0; i < _blips; i++)
                {
                    int tempPotency = i >= remainder ? blipPotency : blipPotency + 1;
                    GameObject tempBlip = Instantiate(normalBlip, transform.position, Quaternion.identity);
                    tempBlip.GetComponent<Energy_Blip>().Activate(_tint, (int)_tint, barPos, tempPotency);
                }
            } 
            else if (_tint == TColor.GREEN)
            {
                // Spawns one blip for each player, regardless of passed in value
                for (int i = 0; i <= 3; i++)
                {
                    bool isDead = GameManager.instance.party.GetPlayer(i).Status.IsDead;
                    Transform barPos = isDead ? Combat_UI_Commands.GetReviveBarPos(i) : Combat_UI_Commands.GetHealthBarPos(i);
                    int potency = isDead ? _reviveVal : _pointVal;

                    GameObject tempBlip = Instantiate(normalBlip, transform.position, Quaternion.identity);
                    tempBlip.GetComponent<Energy_Blip>().Activate(_tint, i, barPos, potency);
                }
            }
            else if (_tint == TColor.BLACK)
            {
                // Spawns one blip for each player, regardless of passed in value
                for (int i = 0; i <= 3; i++)
                {
                    bool isDead = GameManager.instance.party.GetPlayer(i).Status.IsDead;
                    Transform barPos = isDead ? Combat_UI_Commands.GetReviveBarPos(i) : Combat_UI_Commands.GetEnergyBarPos(i);
                    int potency = isDead ? _reviveVal : _pointVal;

                    GameObject tempBlip = Instantiate(normalBlip, transform.position, Quaternion.identity);
                    tempBlip.GetComponent<Energy_Blip>().Activate(_tint, i, barPos, potency);
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
