using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationScript : MonoBehaviour
{
    
    void Awake()
    {
        DataScript.hexColors = new Color[5] {new Color(0.295248f, 0.2823068f, 0.9811321f), new Color(0f, 0.9245283f, 0.8622006f),
            new Color(1, 0.8548142f, 0), new Color(1f, 0.2122642f, 0.3937522f),new Color(0.8113208f, 0, 0.7782146f) };

        
        DataScript.maxLevel = 6;
        DataScript.currentLevel = PlayerPrefs.GetInt("Current Level", 1);

        //lock/unlock the input
        DataScript.inputLock = false;

        //turn on and off the colors, no colors make the game easier
        DataScript.isColorsActive = false;

        DataScript.gemCount = PlayerPrefs.GetInt("Gem Count", 0);

        DataScript.colorZeroList = new List<int>();
        DataScript.colorOneList = new List<int>();
        DataScript.colorTwoList = new List<int>();
        DataScript.colorThreeList = new List<int>();
        DataScript.colorFourList = new List<int>();

        DataScript.highestPointOnAHex = 0;
        DataScript.highestHexScale = 0;

        //Remove when decided
        DataScript.isCityOpen = true;

        GameObject primaryMap = Instantiate(Resources.Load<GameObject>("Levels/" + DataScript.currentLevel.ToString()), Vector3.zero, Quaternion.identity);
    }

   
}
