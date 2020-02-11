using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    private GameObject gemUI;

    UIManager uIManager;

    private void Start()
    {
        uIManager = FindObjectOfType(typeof(UIManager)) as UIManager;

        gemUI = GameObject.FindWithTag("GemUI");

        gameObject.GetComponentInParent<SkyscraperScript>().gem = gameObject;
        gameObject.GetComponentInParent<SkyscraperScript>().isGemInsideThis = true;

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

    public IEnumerator GemCollectedAnimations()
    {
        for(int i = 0; i < 40; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, gemUI.transform.position, 0.1f);
            yield return new WaitForEndOfFrame();
        }

        DataScript.gemCount++;
        PlayerPrefs.SetInt("Gem Count", DataScript.gemCount);
        uIManager.SetGemCounterText();

        gameObject.SetActive(false);
    }
}
