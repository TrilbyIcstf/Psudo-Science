public enum Color
{
    GHOST,
    BLACK,
    BLUE,
    GREEN,
    GREY,
    ORANGE,
    PINK,
    PURPLE
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
