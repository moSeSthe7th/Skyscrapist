using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemRotator : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(RotateTheGem());
    }

    IEnumerator RotateTheGem()
    {
        while (true)
        {
            transform.Rotate(0, 0.5f, 0);
            yield return new WaitForEndOfFrame();
        }
    }
}
