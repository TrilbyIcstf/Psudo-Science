using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public Player_Information player1;
    public Player_Information player2;
    public Player_Information player3;
    public Player_Information player4;

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
            player1.resetStatus(player1.MaxHealth / 2);
        }
        if (player2 != null)
        {
            player2.resetStatus(player2.MaxHealth / 3);
        }
        if (player3 != null)
        {
            player3.resetStatus(player3.MaxHealth / 4);
        }
        if (player4 != null)
        {
            player4.resetStatus(player4.MaxHealth / 5);
        }
    }

    public void PartyHeal(int _heal1, int _heal2, int _heal3, int _heal4)
    {
        if (player1 != null)
        {
            player1.Heal(_heal1);
        }
        if (player2 != null)
        {
            player2.Heal(_heal2);
        }
        if (player3 != null)
        {
            player3.Heal(_heal3);
        }
        if (player4 != null)
        {
            player4.Heal(_heal4);
        }
    }

    public Player_Information GetPlayer(TColor _tint)
    {
        switch(_tint)
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

    public Player_Information GetPlayer(PC _pc)
    {
        switch(_pc)
        {
            case PC.VANESSA:
                return player1;
            case PC.SAMANTHA:
                return player2;
            case PC.GABRIELLE:
                return player3;
            case PC.VALLERY:
                return player4;
            default:
                return null;
        }
    }
}
