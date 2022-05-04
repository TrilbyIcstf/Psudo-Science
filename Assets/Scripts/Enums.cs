public enum GameState
{
    DUNGEON,
    MENU,
    DIALOGUE,
    COMBAT
}

public enum Color
{
    GHOST = 99,
    BLACK = 6,
    BLUE = 0,
    GREEN = 4,
    GREY = 5,
    ORANGE = 1,
    PINK = 3,
    PURPLE = 2
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
