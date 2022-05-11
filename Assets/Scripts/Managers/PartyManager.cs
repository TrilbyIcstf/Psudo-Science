using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public Player_Information player1;
    public Player_Information player2;
    public Player_Information player3;
    public Player_Information player4;

    private Player_Status playerStatus1;
    private Player_Status playerStatus2;
    private Player_Status playerStatus3;
    private Player_Status playerStatus4;

    void Awake()
    {
        ResetStatus();
    }

    /// <summary>
    /// Resets all status variables to their defaults.
    /// </summary>
    public void ResetStatus()
    {
        if (player1 != null)
        {
            playerStatus1 = new Player_Status(player1.MaxHealth/2);
        }
        if (player2 != null)
        {
            playerStatus2 = new Player_Status(player2.MaxHealth/3);
        }
        if (player3 != null)
        {
            playerStatus3 = new Player_Status(player3.MaxHealth/4);
        }
        if (player4 != null)
        {
            playerStatus4 = new Player_Status(player4.MaxHealth/5);
        }
    }

    public void PartyHeal(int _heal1, int _heal2, int _heal3, int _heal4)
    {
        if (player1 != null)
        {
            playerStatus1.CurrentHealth = (int)Mathf.Min(playerStatus1.CurrentHealth + _heal1, (player1.MaxHealth * playerStatus1.HealthMult) + playerStatus1.HealthBuff);
        }
        if (player2 != null)
        {
            playerStatus2.CurrentHealth = (int)Mathf.Min(playerStatus2.CurrentHealth + _heal2, (player2.MaxHealth * playerStatus2.HealthMult) + playerStatus2.HealthBuff);
        }
        if (player3 != null)
        {
            playerStatus3.CurrentHealth = (int)Mathf.Min(playerStatus3.CurrentHealth + _heal3, (player3.MaxHealth * playerStatus3.HealthMult) + playerStatus3.HealthBuff);
        }
        if (player4 != null)
        {
            playerStatus4.CurrentHealth = (int)Mathf.Min(playerStatus4.CurrentHealth + _heal4, (player4.MaxHealth * playerStatus4.HealthMult) + playerStatus4.HealthBuff);
        }
    }

    // Get/Set
    public Player_Status PlayerStatus1 { get => playerStatus1; set => playerStatus1 = value; }
    public Player_Status PlayerStatus2 { get => playerStatus2; set => playerStatus2 = value; }
    public Player_Status PlayerStatus3 { get => playerStatus3; set => playerStatus3 = value; }
    public Player_Status PlayerStatus4 { get => playerStatus4; set => playerStatus4 = value; }

    public Player_Status GetPlayer(TColor _tint)
    {
        switch (_tint)
        {
            case TColor.BLUE:
                return playerStatus1;
            case TColor.ORANGE:
                return playerStatus2;
            case TColor.PINK:
                return playerStatus3;
            case TColor.PURPLE:
                return playerStatus4;
            default:
                return null;
        }
    }

    public Player_Information GetPlayerInfo(TColor _tint)
    {
        switch (_tint)
        {
            case TColor.BLUE:
                return player1;
            case TColor.ORANGE:
                return player2;
            case TColor.PINK:
                return player3;
            case TColor.PURPLE:
                return player4;
            default:
                return null;
        }
    }
}
