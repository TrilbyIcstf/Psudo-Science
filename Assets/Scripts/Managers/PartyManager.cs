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
            player1.ResetStatus(player1.MaxHealth);
        }
        if (player2 != null)
        {
            player2.ResetStatus(player2.MaxHealth);
        }
        if (player3 != null)
        {
            player3.ResetStatus(player3.MaxHealth);
        }
        if (player4 != null)
        {
            player4.ResetStatus(player4.MaxHealth);
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

    public int LivingPlayers()
    {
        return players.Count(p => !p.ShouldDie());
    }

    public int MostDamaged(bool alive = true, List<int> ignore = null)
    {
        ignore ??= new List<int>();

        Player_Information highest = null;

        foreach (Player_Information player in players)
        {
            if (ignore.Contains(player.Position))
            {
                continue;
            }

            if (alive && player.ShouldDie())
            {
                continue;
            }

            if (MoreDamaged(player, highest))
            {
                highest = player;
            }
        }

        return highest?.position ?? -1;
    }

    private bool MoreDamaged(Player_Information next, Player_Information highest)
    {
        int highestDam = highest?.CurrentDamage ?? -1;

        if (next.CurrentDamage == highestDam)
        {
            return Random.Range(0, 2) == 0 ? true : false;
        }

        return next.CurrentDamage > highestDam;
    }

    public int LowestHealth(bool alive = true, List<int> ignore = null)
    {
        ignore ??= new List<int>();

        Player_Information lowest = null;

        foreach (Player_Information player in players)
        {
            if (ignore.Contains(player.Position))
            {
                continue;
            }

            if (alive && player.ShouldDie())
            {
                continue;
            }

            if (LowerHealth(player, lowest))
            {
                lowest = player;
            }
        }

        return lowest?.position ?? -1;
    }

    private bool LowerHealth(Player_Information next, Player_Information lowest)
    {
        int lowestHealth = lowest?.CurrentHealth ?? int.MaxValue;

        if (next.CurrentHealth == lowestHealth)
        {
            return Random.Range(0, 2) == 0 ? true : false;
        }

        return next.CurrentHealth < lowestHealth;
    }

    public int HighestHealth(bool alive = true, List<int> ignore = null)
    {
        ignore ??= new List<int>();

        Player_Information highest = null;

        foreach (Player_Information player in players)
        {
            if (ignore.Contains(player.Position))
            {
                continue;
            }

            if (alive && player.ShouldDie())
            {
                continue;
            }

            if (HigherHealth(player, highest))
            {
                highest = player;
            }
        }

        return highest?.position ?? -1;
    }

    private bool HigherHealth(Player_Information next, Player_Information highest)
    {
        int highestHealth = highest?.CurrentHealth ?? -1;

        if (next.CurrentHealth == highestHealth)
        {
            return Random.Range(0, 2) == 0 ? true : false;
        }

        return next.CurrentHealth > highestHealth;
    }
}
