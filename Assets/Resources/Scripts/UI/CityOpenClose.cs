using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Remove this script when decided
public class CityOpenClose : MonoBehaviour
{
   public void OpenCloseCit()
    {
        DataScript.isCityOpen = !DataScript.isCityOpen;
    }
}
