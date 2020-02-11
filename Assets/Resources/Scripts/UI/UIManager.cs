using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject levelPassedPanel;
    public Button restartButton;

    public Text gemCounterText;

    void Start()
    {
        levelPassedPanel.SetActive(false);
        SetGemCounterText();
    }

   
    public void GameOverGUI()
    {
        levelPassedPanel.SetActive(true);
        restartButton.gameObject.SetActive(false);
    }

    public void SetGemCounterText()
    {
        gemCounterText.text = DataScript.gemCount.ToString();
    }
}
