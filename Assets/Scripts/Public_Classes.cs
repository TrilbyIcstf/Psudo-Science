using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorVals
{
    public static Color VeniBlue { get => new Color(0f / 255f, 169f / 255f, 239f / 255f); }
    public static Color CarolOrange { get => new Color(255f / 255f, 88f / 255f, 33f / 255f); }
    public static Color GabbyPink { get => new Color(234f / 255f, 128f / 255f, 252f / 255f); }
    public static Color ValleryPurple { get => new Color(156f / 255f, 40f / 255f, 174f / 255f); }
    public static Color VerdantGreen { get => new Color(99f / 255f, 222f / 255f, 24f / 255f); }
    public static Color LunarGrey { get => new Color(176f / 255f, 190f / 255f, 198f / 255f); }
    public static Color DeepBlack { get => new Color(44f / 255f, 43f / 255f, 44f / 255f); }

    public static Color GetColorVal(TColor _val)
    {
        switch (_val)
        {
            case TColor.BLUE:
                return VeniBlue;
            case TColor.ORANGE:
                return CarolOrange;
            case TColor.PINK:
                return GabbyPink;
            case TColor.PURPLE:
                return ValleryPurple;
            case TColor.GREEN:
                return VerdantGreen;
            case TColor.GREY:
                return LunarGrey;
            case TColor.BLACK:
                return DeepBlack;
            default:
                return Color.black;
        }
    }
}
