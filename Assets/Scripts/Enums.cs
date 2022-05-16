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

public enum AttackType
{
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

public enum Bestiary
{
    BookRat,
    KnickedSkeleton,
    StainedKnight
}