using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Energy
{
    // The points towards each color's next move
    private float bluePoints = 0;
    private float orangePoints = 0;
    private float pinkPoints = 0;
    private float purplePoints = 0;

    // The point cap needed to execute the next move
    private float blueCap = 50;
    private float orangeCap = 50;
    private float pinkCap = 50;
    private float purpleCap = 50;

    // The power multiplier for each color's next move
    private float bluePower = 1;
    private float orangePower = 1;
    private float pinkPower = 1;
    private float purplePower = 1;

    /// <summary>
    /// Add points to designated color, then execute attack if cap has been reached.
    /// </summary>
    /// <param name="_val">
    /// Amount being added to the color.
    /// </param>
    /// <param name="_eColor">
    /// The color to add to.
    /// </param>
    /// <returns>
    /// Whether the attack was executed or not.
    /// </returns>
    public bool GainEnergy(float _val, Color _eColor)
    {
        AddColor(_val, _eColor);

        bool triggered = false;
        while (CheckCap(_eColor))
        {
            triggered = true;

            // SEND ATTACK TO QUEUE

            ColorCapExpend(_eColor);
        }

        return triggered;
    }

    /// <summary>
    /// Adds amount to specified color.
    /// </summary>
    /// <param name="_val">
    /// Amount to add.
    /// </param>
    /// <param name="_eColor">
    /// Color to add it to.
    /// </param>
    /// <returns>
    /// Amount after addition.
    /// </returns>
    public float AddColor(float _val, Color _eColor)
    {
        switch (_eColor)
        {
            case Color.BLUE:
                bluePoints += _val;
                return bluePoints;
            case Color.ORANGE:
                orangePoints += _val;
                return orangePoints;
            case Color.PINK:
                pinkPoints += _val;
                return pinkPoints;
            case Color.PURPLE:
                purplePoints += _val;
                return purplePoints;
            default:
                return -8675309;
        }
    }

    /// <summary>
    /// Reduces the specified color points by its cap.
    /// </summary>
    /// <param name="_eColor">
    /// The color to reduce.
    /// </param>
    /// <returns>
    /// The amount remaining.
    /// </returns>
    public float ColorCapExpend(Color _eColor)
    {
        switch (_eColor)
        {
            case Color.BLUE:
                bluePoints -= blueCap;
                return bluePoints;
            case Color.ORANGE:
                orangePoints -= orangeCap;
                return orangePoints;
            case Color.PINK:
                pinkPoints -= pinkCap;
                return pinkPoints;
            case Color.PURPLE:
                purplePoints -= purpleCap;
                return purplePoints;
            default:
                return -8675309;
        }
    }

    /// <summary>
    /// Checks if the specified color has reached its required amount to execute an attack.
    /// </summary>
    /// <param name="_eColor">
    /// The color to check.
    /// </param>
    /// <returns>
    /// Whether the cap has been reached.
    /// </returns>
    public bool CheckCap(Color _eColor)
    {
        switch (_eColor)
        {
            case Color.BLUE:
                return bluePoints >= blueCap;
            case Color.ORANGE:
                return orangePoints >= orangeCap;
            case Color.PINK:
                return pinkPoints >= pinkCap;
            case Color.PURPLE:
                return purplePoints >= purpleCap;
            default:
                return false;
        }
    }

    public float ExpoPowerUp(float _val, Color _pColor)
    {
        switch (_pColor)
        {
            case Color.BLUE:
                bluePower *= _val;
                return bluePower;
            case Color.ORANGE:
                orangePower *= _val;
                return orangePower;
            case Color.PINK:
                pinkPower *= _val;
                return pinkPower;
            case Color.PURPLE:
                purplePower *= _val;
                return purplePower;
            default:
                return -1;
        }
    }

    // Get/Set
    public float BluePoints { get => bluePoints; set => bluePoints = value; }
    public float OrangePoints { get => orangePoints; set => orangePoints = value; }
    public float PinkPoints { get => pinkPoints; set => pinkPoints = value; }
    public float PurplePoints { get => purplePoints; set => purplePoints = value; }
    public float BluePower { get => bluePower; set => bluePower = value; }
    public float OrangePower { get => orangePower; set => orangePower = value; }
    public float PinkPower { get => pinkPower; set => pinkPower = value; }
    public float PurplePower { get => purplePower; set => purplePower = value; }

    // DEBUG FUNCTIONS
    private void DebugColorPointMessage()
    {
        Debug.Log("Blue points: " + bluePoints);
        Debug.Log("Orange points: " + orangePoints);
        Debug.Log("Pink points: " + pinkPoints);
        Debug.Log("Purple points: " + purplePoints);
        Debug.Log("");
        Debug.Log("Blue power: " + bluePower);
        Debug.Log("Orange power: " + orangePower);
        Debug.Log("Pink power: " + pinkPower);
        Debug.Log("Purple power: " + purplePower);
    }
}
