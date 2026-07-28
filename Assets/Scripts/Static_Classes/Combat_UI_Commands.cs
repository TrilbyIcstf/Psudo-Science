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
        return GetPlayerPosition((int)_player);
    }

    public static Transform GetPlayerPosition(int _player)
    {
        return GameManager.instance.combat.combatUI.PlayerUI[_player].PlayerObject.transform;
    }

    public static Transform GetEnergyBarPos(TColor _tint)
    {
        return GetEnergyBarPos(_tint.ToPC());
    }

    public static Transform GetEnergyBarPos(PC _player)
    {
        return GetEnergyBarPos((int)_player);
    }

    public static Transform GetEnergyBarPos(int _player)
    {
        return GameManager.instance.combat.combatUI.PlayerUI[_player].EnergyBar.transform;
    }

    public static Transform GetHealthBarPos(int _player)
    {
        return GameManager.instance.combat.combatUI.PlayerUI[_player].HealthBar.transform;
    }

    public static Transform GetHealthBarPos(PC _player)
    {
        return GetHealthBarPos((int)_player);
    }

    public static Transform GetReviveBarPos(int _player)
    {
        return GameManager.instance.combat.combatUI.PlayerUI[_player].ReviveBar.transform;
    }

    public static void SendEnergy(float _val, int _player)
    {
        GameManager.instance.combat.combatUI.PlayerUI[_player].EnergyScript.RecieveEnergy(_val);
    }

    public static void SendHealth(float _val, int _player)
    {
        GameManager.instance.combat.combatUI.PlayerUI[_player].HealthScript.RecieveHealth(_val);
    }

    public static void SendRevive(float _val, int _player)
    {
        GameManager.instance.combat.combatUI.PlayerUI[_player].ReviveScript.RecieveRevive(_val);
    }

    public static void RefreshHealthBars()
    {
        foreach (Player_Information player in GameManager.instance.party.Players())
        {
            GameManager.instance.combat.combatUI.PlayerUI[player.position].HealthScript.RefreshHealth();
        }
    }
}