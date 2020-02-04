using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public Vector3 gameOverCameraPos;
    public Vector3 gameOverCameraLookPos;

    public GameObject gameOverCity;

    GameObject ragdoll;
   
    Camera mainCam;
    GameObject[] skyscrapers;

    UIManager uIManager;

    private void Start()
    {
        uIManager = FindObjectOfType(typeof(UIManager)) as UIManager;

        gameOverCity.SetActive(false);
        ragdoll = GameObject.FindWithTag("Ragdoll");
        mainCam = Camera.main;
    }

    public void GameOverCheck()
    {
        if (isGameOver())
        {
            DataScript.inputLock = true;
            uIManager.GameOverGUI();
            StartCoroutine(StartGameOverAnimations());
        }

        else
        {
            Debug.Log("GAME CONTINUE");
        }
    }

    IEnumerator StartGameOverAnimations()
    {
        skyscrapers = GameObject.FindGameObjectsWithTag("Hex");

        foreach (GameObject skyscraper in skyscrapers)
        {
            skyscraper.GetComponentInChildren<Text>().gameObject.SetActive(false);
        }


        gameOverCity.SetActive(true);

        Debug.Log("GAME OVER");
        mainCam.orthographic = false;
        mainCam.fieldOfView = 45f;
        gameOverCameraLookPos = Vector3.zero;

        ragdoll.SetActive(false);

        for(int j = 0; j < 60; j++)
        {
            mainCam.transform.position = Vector3.MoveTowards(Camera.main.transform.position, gameOverCameraPos, 0.2f);
            mainCam.transform.LookAt(gameOverCameraLookPos);
            yield return new WaitForEndOfFrame();
        }

        foreach (GameObject skyscraper in skyscrapers)
        {
            SkyscraperScript skyscraperScript = skyscraper.GetComponent<SkyscraperScript>();
            StartCoroutine(skyscraperScript.SkyScraperGameOverAnim());
        }

        while (true)
        {
            gameOverCameraPos.y = DataScript.highestPointOnAHex + 20;
            
            mainCam.transform.position = Vector3.MoveTowards(Camera.main.transform.position, gameOverCameraPos, 0.2f);
            mainCam.transform.LookAt(gameOverCameraLookPos);
            yield return new WaitForEndOfFrame();
        }

        
    }

    public bool isGameOver()
    {

        foreach (int i in DataScript.colorZeroList)
        {
            if (DataScript.colorZeroList != null)
            {
                if (i != DataScript.colorZeroList[0])
                    return false;
            }
        }
        foreach (int i in DataScript.colorOneList)
        {
            if (DataScript.colorOneList != null)
            {
                if (i != DataScript.colorOneList[0])
                    return false;
            }
        }
        foreach (int i in DataScript.colorTwoList)
        {
            if (DataScript.colorTwoList != null)
            {
                if (i != DataScript.colorTwoList[0])
                    return false;
            }
        }
        foreach (int i in DataScript.colorThreeList)
        {
            if (DataScript.colorThreeList != null)
            {
                if (i != DataScript.colorThreeList[0])
                    return false;
            }
        }
        foreach (int i in DataScript.colorFourList)
        {
            if (DataScript.colorFourList != null)
            {
                if (i != DataScript.colorFourList[0])
                    return false;
            }
        }
        
        return true;
    }
}
