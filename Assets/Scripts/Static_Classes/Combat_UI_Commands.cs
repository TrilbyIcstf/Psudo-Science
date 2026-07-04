using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static Transform GetPlayerPosition(PC _player)
    {
        switch(_player)
        {
            case PC.VANESSA:
                return GameManager.instance.combat.combatUI.player1.transform;
            case PC.SAMANTHA:
                return GameManager.instance.combat.combatUI.player2.transform;
            case PC.GABRIELLE:
                return GameManager.instance.combat.combatUI.player3.transform;
            case PC.VALLERY:
                return GameManager.instance.combat.combatUI.player4.transform;
            default:
                return null;
        }
    }

    public static Transform GetPlayerPosition(int _player)
    {
        return GetPlayerPosition((PC)_player);
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

    public static Transform GetEnergyBarPos(PC _player)
    {
        return GetEnergyBarPos((TColor)_player);
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

    public static Transform GetHealthBarPos(PC _player)
    {
        return GetHealthBarPos((int)_player);
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

    public static void RefreshHealthBars()
    {
        foreach (Player_Information player in GameManager.instance.party.Players())
        {
            GameManager.instance.combat.combatUI.PlayerHealths[player.position].GetComponent<Health_UI>().RefreshHealth();
        }
    }
}