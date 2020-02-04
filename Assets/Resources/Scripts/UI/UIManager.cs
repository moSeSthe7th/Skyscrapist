using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject levelPassedPanel;
    public Button restartButton;

    void Start()
    {
        levelPassedPanel.SetActive(false);
    }

   
    public void GameOverGUI()
    {
        levelPassedPanel.SetActive(true);
        restartButton.gameObject.SetActive(false);
    }
}
