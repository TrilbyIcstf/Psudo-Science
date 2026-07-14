using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] private Player_Information player1;
    [SerializeField] private Player_Information player2;
    [SerializeField] private Player_Information player3;
    [SerializeField] private Player_Information player4;

    private List<Player_Information> players => new List<Player_Information> { player1, player2, player3, player4 };

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

    public void SingleHeal(int pos, int potency)
    {
        players[pos].Heal(potency);
    }

    public void PartyHeal(int heal)
    {
        PartyHeal(heal, heal, heal, heal);
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
    public List<Player_Information> Players()
    {
        return players;
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

    public Player_Information GetPlayer(int _posit)
    {
        return players[_posit];
    }

    public int MostDamaged()
    {
        return players.Aggregate((highest, next) => next.CurrentDamage > highest.CurrentDamage ? next : highest).position;
    }
}
