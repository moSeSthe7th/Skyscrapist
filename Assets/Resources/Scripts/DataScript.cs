using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataScript
{
    public static Color[] hexColors;

    public static bool inputLock;

    public static int currentLevel;
    public static int maxLevel;

    //turn on and off the colors, no colors make the game easier
    public static bool isColorsActive;

    //holds color codes
    public static List<int> colorZeroList;
    public static List<int> colorOneList;
    public static List<int> colorTwoList;
    public static List<int> colorThreeList;
    public static List<int> colorFourList;

    public static float highestPointOnAHex;
    public static float highestHexScale;

    //Remove when decided
    public static bool isCityOpen;
    
}
