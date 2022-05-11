using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorVals
{
    public static Color VeniBlue { get => new Color(0f / 255f, 169f / 255f, 239f / 255f); }
    public static Color CarolOrange { get => new Color(255f / 255f, 88f / 255f, 33f / 255f); }
    public static Color GabbyPink { get => new Color(234f / 255f, 128f / 255f, 252f / 255f); }
    public static Color ValleryPurple { get => new Color(156f / 255f, 40f / 255f, 174f / 255f); }
    public static Color VerdantGreen { get => new Color(99f / 255f, 222f / 255f, 24f / 255f); }
    public static Color LunarGrey { get => new Color(176f / 255f, 190f / 255f, 198f / 255f); }
    public static Color DeepBlack { get => new Color(44f / 255f, 43f / 255f, 44f / 255f); }

    public static Color GetColorVal(TColor _val)
    {
        switch (_val)
        {
            case TColor.BLUE:
                return VeniBlue;
            case TColor.ORANGE:
                return CarolOrange;
            case TColor.PINK:
                return GabbyPink;
            case TColor.PURPLE:
                return ValleryPurple;
            case TColor.GREEN:
                return VerdantGreen;
            case TColor.GREY:
                return LunarGrey;
            case TColor.BLACK:
                return DeepBlack;
            default:
                return Color.black;
        }
    }
}

public static class Combat_UI_Commands
{
    /// <summary>
    /// Checks if the passed in color should spawn blips
    /// </summary>
    /// <param name="_tint">
    /// The color to check
    /// </param>
    /// <returns>
    /// Whether or not the color should spawn blips
    /// </returns>
    public static bool IsBlipColor(TColor _tint)
    {
        if (_tint == TColor.BLUE || _tint == TColor.ORANGE || _tint == TColor.PINK || _tint == TColor.PURPLE || _tint == TColor.GREEN || _tint == TColor.BLACK)
        {
            return true;
        }
        return false;
    }

    public static Transform GetEnergyBarPos(TColor _tint)
    {
        switch (_tint)
        {
            case TColor.BLUE:
                return GameManager.instance.combat.combatUI.player1Energy.transform;
            case TColor.ORANGE:
                return GameManager.instance.combat.combatUI.player2Energy.transform;
            case TColor.PINK:
                return GameManager.instance.combat.combatUI.player3Energy.transform;
            case TColor.PURPLE:
                return GameManager.instance.combat.combatUI.player4Energy.transform;
            default:
                return null;
        }
    }

    public static Transform GetEnergyBarPos(int _player)
    {
        switch (_player)
        {
            case 1:
                return GameManager.instance.combat.combatUI.player1Energy.transform;
            case 2:
                return GameManager.instance.combat.combatUI.player2Energy.transform;
            case 3:
                return GameManager.instance.combat.combatUI.player3Energy.transform;
            case 4:
                return GameManager.instance.combat.combatUI.player4Energy.transform;
            default:
                return null;
        }
    }

    public static Transform GetHealthBarPos(int _player)
    {
        switch (_player)
        {
            case 1:
                return GameManager.instance.combat.combatUI.player1Health.transform;
            case 2:
                return GameManager.instance.combat.combatUI.player2Health.transform;
            case 3:
                return GameManager.instance.combat.combatUI.player3Health.transform;
            case 4:
                return GameManager.instance.combat.combatUI.player4Health.transform;
            default:
                return null;
        }
    }

    public static void SendEnergy(float _val, int _player)
    {
        switch (_player)
        {
            case 1:
                GameManager.instance.combat.combatUI.player1Energy.GetComponent<Energy_UI>().RecieveEnergy(_val);
                break;
            case 2:
                GameManager.instance.combat.combatUI.player2Energy.GetComponent<Energy_UI>().RecieveEnergy(_val);
                break;
            case 3:
                GameManager.instance.combat.combatUI.player3Energy.GetComponent<Energy_UI>().RecieveEnergy(_val);
                break;
            case 4:
                GameManager.instance.combat.combatUI.player4Energy.GetComponent<Energy_UI>().RecieveEnergy(_val);
                break;
            default:
                break;
        }
    }

    public static void SendHealth(float _val, int _player)
    {
        switch (_player)
        {
            case 1:
                GameManager.instance.combat.combatUI.player1Health.GetComponent<Health_UI>().RecieveHealth(_val);
                break;
            case 2:
                GameManager.instance.combat.combatUI.player2Health.GetComponent<Health_UI>().RecieveHealth(_val);
                break;
            case 3:
                GameManager.instance.combat.combatUI.player3Health.GetComponent<Health_UI>().RecieveHealth(_val);
                break;
            case 4:
                GameManager.instance.combat.combatUI.player4Health.GetComponent<Health_UI>().RecieveHealth(_val);
                break;
            default:
                break;
        }
    }
}
