using System.Collections.Generic;

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

public static class TColorExtensions
{
    public static bool CanRevive(this TColor color)
    {
        switch (color)
        {
            case TColor.BLACK:
            case TColor.BLUE:
            case TColor.GREEN:
            case TColor.ORANGE:
            case TColor.PINK:
            case TColor.PURPLE:
                return true;
            default:
                return false;
        }
    }

    public static bool ForceInt(this TColor color)
    {
        switch (color)
        {
            case TColor.BLACK:
            case TColor.BLUE:
            case TColor.GREEN:
            case TColor.ORANGE:
            case TColor.PINK:
            case TColor.PURPLE:
                return true;
            default:
                return false;
        }
    }

    public static bool IsPlayer(this TColor color)
    {
        switch (color)
        {
            case TColor.BLUE:
            case TColor.ORANGE:
            case TColor.PINK:
            case TColor.PURPLE:
                return true;
            default:
                return false;
        }
    }

    public static TColor FromPos(int pos)
    {
        return (TColor)pos;
    }
} 

public enum Element
{
    VOID,
    BLUNT,
    SLASH,
    PIERCE,
    WATER,
    EARTH,
    FIRE,
    AIR,
    LIGHT,
    DARK,
}

public enum StatusEffect
{
    POISONED = 1,
    BURNED = 2,
    CONFUSED = 3,
    SLOW = 4,
    MINORPOWERUP = 1001,
    MIDPOWERUP = 1002,
    MAJORPOWERUP = 1003,
    WARMUP = 9901,
}

public static class StatusEffectExtensions
{
    public static bool IsBuff(this StatusEffect se)
    {
        switch(se)
        {
            case StatusEffect.MINORPOWERUP:
            case StatusEffect.MIDPOWERUP:
            case StatusEffect.MAJORPOWERUP:
            case StatusEffect.WARMUP:
                return true;
            default:
                return false;
        }
    }

    public static bool IsDebuff(this StatusEffect se)
    {
        return !se.IsBuff();
    }

    public static (Dictionary<StatusEffect, int>, Dictionary<StatusEffect, int>) SplitStatusEffects(this Dictionary<StatusEffect, int> combinedList)
    {
        Dictionary<StatusEffect, int> buffs = new Dictionary<StatusEffect, int>();
        Dictionary<StatusEffect, int> debuffs = new Dictionary<StatusEffect, int>();

        foreach (var se in combinedList)
        {
            if (se.Key.IsBuff())
            {
                buffs.Add(se.Key, se.Value);
            } else
            {
                debuffs.Add(se.Key, se.Value);
            }
        }

        return (buffs, debuffs);
    }
}

public enum MoveType
{
    NULL,
    PHYSICAL,
    MAGICAL,
    HEALING,
    STATUS,
    BOARD
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

public enum BodyPart
{
    CENTER,
    HEAD,
    BODY,
    LEGS,
    HANDS
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
    SAMANTHA = 1,
    GABRIELLE = 2,
    VALLERY = 3,
    NULL = 99
}

static class PCExtensions
{
    public static PC ToPC(this TColor color)
    {
        switch(color)
        {
            case TColor.BLUE:
                return PC.VANESSA;
            case TColor.ORANGE:
                return PC.SAMANTHA;
            case TColor.PINK:
                return PC.GABRIELLE;
            case TColor.PURPLE:
                return PC.VALLERY;
            default:
                return PC.NULL;
        }
    }
}

public enum Target
{
    PC,
    ENEMY,
    NULL
}

public enum TargetingType
{
    LowestHealth,
    HighestHealth,
    Random,
    Custom
}

public enum MoveName
{
    // Slash attacks
    QuickSlash = 1001,

    // Blunt attacks
    GlancingBlow = 1101,
    Shatter = 1102,

    // Pierce attacks

    // Fire spells
    LesserSpark = 1301,
    LesserSparkfield = 1302,

    // Frost spells
    LesserFrost = 1401,
    LesserFrostField = 1402,

    // Air spells
    LesserAreo = 1501,
    ConcentratedAreo = 1502,

    // Earth spells
    LesserTremor = 1601,
    ConcentratedTremor = 1602,

    // Healing spells
    LesserHeal = 1701,
    LesserHealfield = 1702,

    // Buffs
    WarmUp = 1801,
    ValientStrength = 1802,

    // Debuffs
    Slow = 1901,

    // Board spells
    ShareEnergy = 2001,

    // Misc
}

public enum EnemyMoveName
{
    // Slash attacks
    BasicSlash = 1001,

    // Blunt attacks

    // Pierce attacks

    // Fire spells

    // Frost spells

    // Air spells

    // Earth spells

    // Healing spells

    // Buffs

    // Debuffs

    // Board spells

    // Misc
}

public enum Bestiary
{
    BookRat = 1001,
    KnickedSkeleton = 1002,
    StainedKnight = 1003,
}

public enum CombatAnimation
{
    SmallRecoil,
    ColorFlash,
    SmallShake,
}

public static class EnumMapping
{
    public static string ToAnimString(this CombatAnimation ea)
    {
        switch(ea)
        {
            case CombatAnimation.SmallRecoil:
                return "SmallRecoil";
            case CombatAnimation.ColorFlash:
                return "ColorFlash";
            case CombatAnimation.SmallShake:
                return "SmallShake";
            default:
                return "";
        }
    }
}

public enum Direction
{
    LEFT,
    RIGHT
}

public static class DirectionExtension
{
    public static float NumericRepresentation(this Direction dir)
    {
        return dir == Direction.LEFT ? -1 : 1;
    }
}