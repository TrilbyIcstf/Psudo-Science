public enum GameState
{
    DUNGEON,
    MENU,
    DIALOGUE,
    COMBAT
}

public enum TColor
{
    GHOST = 99,
    BLACK = 6,
    BLUE = 0,
    GREEN = 4,
    GREY = 5,
    ORANGE = 1,
    PINK = 2,
    PURPLE = 3
}

public enum Element
{
    NULL,
    WATER,
    EARTH,
    FIRE,
    AIR
}

public enum MoveType
{
    NULL,
    PHYSICAL,
    MAGICAL,
    HEALING,
    STATUS
}

public enum PlayerClass
{
    HERO,
    CLERIC,
    WARRIOR,
    MAGE
}

public enum EquipType
{
    WEAPON,
    HELMET,
    ARMOR,
    PANT,
    ACC
}

/// <summary>
/// Enum for effect timing on equipment and status effects
/// </summary>
public enum CombatTiming
{
    COMBATSTART,
    COMBATEND,
    TURNSTART,
    TURNEND,
    PLAYERATTACK,
    ENEMYATTACK,
    PLAYERHIT,
    PLAYERENERGY
}

public enum PC
{
    VANESSA = 0,
    CAROLINE = 1,
    GABRIELLE = 2,
    VALLERY = 3,
    NULL = 99
}

static class PCExtensions
{
    public static PC FromColor(TColor color)
    {
        switch(color)
        {
            case TColor.BLUE:
                return PC.VANESSA;
            case TColor.ORANGE:
                return PC.CAROLINE;
            case TColor.PINK:
                return PC.GABRIELLE;
            case TColor.PURPLE:
                return PC.VALLERY;
            default:
                return PC.NULL;
        }
    }
}

public enum Bestiary
{
    BookRat,
    KnickedSkeleton,
    StainedKnight
}