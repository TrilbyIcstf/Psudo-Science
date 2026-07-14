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
    private float blueCap = 20;
    private float orangeCap = 20;
    private float pinkCap = 20;
    private float purpleCap = 20;

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
    public bool GainEnergy(float _val, TColor _eColor)
    {
        AddColor(_val, _eColor);

        bool triggered = false;
        while (CheckCap(_eColor))
        {
            triggered = true;

            QueueMove(_eColor);
        }

        return triggered;
    }

    public void QueueMove(TColor _eColor)
    {
        MoveName selectedMove = GameManager.instance.combat.GetSelectedMove(_eColor.ToPC());
        GameObject moveObject = GameManager.instance.ll.moveRepository.GetValue(selectedMove);
        Move_Information moveInfo = GameManager.instance.ll.moveRepository.GetInformation(selectedMove);
        QueuedMove genAttack = new QueuedMove(moveObject, _eColor.ToPC());
        GameManager.instance.combat.AddMoveToQueue(genAttack);
        GameManager.instance.combat.AddToMoveCombo();

        ColorCapExpend(_eColor);
        SetColorCap(_eColor, moveInfo.ManaCost);
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
    public float AddColor(float _val, TColor _eColor)
    {
        switch (_eColor)
        {
            case TColor.BLUE:
                bluePoints += _val;
                return bluePoints;
            case TColor.ORANGE:
                orangePoints += _val;
                return orangePoints;
            case TColor.PINK:
                pinkPoints += _val;
                return pinkPoints;
            case TColor.PURPLE:
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
    public float ColorCapExpend(TColor _eColor)
    {
        switch (_eColor)
        {
            case TColor.BLUE:
                bluePoints -= BlueCap;
                return bluePoints;
            case TColor.ORANGE:
                orangePoints -= OrangeCap;
                return orangePoints;
            case TColor.PINK:
                pinkPoints -= PinkCap;
                return pinkPoints;
            case TColor.PURPLE:
                purplePoints -= PurpleCap;
                return purplePoints;
            default:
                return -8675309;
        }
    }

    /// <summary>
    /// Sets the energy cap for a specific color.
    /// </summary>
    /// <param name="_eColor">
    /// The color to set.
    /// </param>
    /// <param name="cap">
    /// The new cap.
    /// </param>
    public void SetColorCap(TColor _eColor, float cap)
    {
        switch (_eColor)
        {
            case TColor.BLUE:
                BlueCap = cap;
                return;
            case TColor.ORANGE:
                OrangeCap = cap;
                return;
            case TColor.PINK:
                PinkCap = cap;
                return;
            case TColor.PURPLE:
                PurpleCap = cap;
                return;
            default:
                return;
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
    public bool CheckCap(TColor _eColor)
    {
        switch (_eColor)
        {
            case TColor.BLUE:
                return bluePoints >= BlueCap;
            case TColor.ORANGE:
                return orangePoints >= OrangeCap;
            case TColor.PINK:
                return pinkPoints >= PinkCap;
            case TColor.PURPLE:
                return purplePoints >= PurpleCap;
            default:
                return false;
        }
    }

    public float ExpoPowerUp(float _val, TColor _pColor)
    {
        switch (_pColor)
        {
            case TColor.BLUE:
                bluePower *= _val;
                return bluePower;
            case TColor.ORANGE:
                orangePower *= _val;
                return orangePower;
            case TColor.PINK:
                pinkPower *= _val;
                return pinkPower;
            case TColor.PURPLE:
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
    public float BlueCap { get => blueCap; set => blueCap = value; }
    public float OrangeCap { get => orangeCap; set => orangeCap = value; }
    public float PinkCap { get => pinkCap; set => pinkCap = value; }
    public float PurpleCap { get => purpleCap; set => purpleCap = value; }

    /// <summary>
    /// Returns the specified color's cap.
    /// </summary>
    /// <param name="_eColor">
    /// The color to check.
    /// </param>
    /// <returns>
    /// The passed in color's cap.
    /// </returns>
    public float GetCap(TColor _eColor)
    {
        switch (_eColor)
        {
            case TColor.BLUE:
                return BlueCap;
            case TColor.ORANGE:
                return OrangeCap;
            case TColor.PINK:
                return PinkCap;
            case TColor.PURPLE:
                return PurpleCap;
            default:
                return -1;
        }
    }

    /// <summary>
    /// Returns the specified color's point value.
    /// </summary>
    /// <param name="_eColor">
    /// The color to get.
    /// </param>
    /// <returns>
    /// The current point value of the passed in color.
    /// </returns>
    public float GetColor(TColor _eColor)
    {
        switch (_eColor)
        {
            case TColor.BLUE:
                return BluePoints;
            case TColor.ORANGE:
                return OrangePoints;
            case TColor.PINK:
                return PinkPoints;
            case TColor.PURPLE:
                return PurplePoints;
            default:
                return -1;
        }
    }

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
