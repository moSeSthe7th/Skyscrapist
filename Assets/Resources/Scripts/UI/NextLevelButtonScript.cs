using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButtonScript : MonoBehaviour
{
   
  public void GoNextLevel()
    {
        if(DataScript.currentLevel < DataScript.maxLevel)
        {
            PlayerPrefs.SetInt("Current Level", DataScript.currentLevel + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
        else
        {
            PlayerPrefs.SetInt("Current Level", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }
}
