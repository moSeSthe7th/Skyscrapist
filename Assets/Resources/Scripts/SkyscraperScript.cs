using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkyscraperScript : MonoBehaviour
{
    public int hexColorNo;

    Text countText;
    public int hexCount;

    Material hexMat;

    ParticleSystem hexParticleSys;

    bool isInitialPlacementDone;

    private GameHandler gameHandler;

    public bool isGemInsideThis;
    public GameObject gem;

    void Start()
    {
        hexParticleSys = GetComponentInChildren<ParticleSystem>();
        isInitialPlacementDone = false;
        gameHandler = FindObjectOfType(typeof(GameHandler)) as GameHandler;
        hexMat = GetComponent<Renderer>().material;
        hexMat.color = DataScript.hexColors[hexColorNo];
        
        AddHexToCorrespondingList();

        countText = GetComponentInChildren<Text>();
        SetCountText();
    }

    void GemCollected()
    {
        StartCoroutine(gem.GetComponent<GemScript>().GemCollectedAnimations());
    }

   
    public IEnumerator RagdollOnThis(Color ragdollColor, int ragdollCount, int ragdollColorNo)
    {
        RemoveHexFromList();
        float counter = 0;
        float duration = 0.3f;

        if (isGemInsideThis)
        {
            GemCollected();
            isGemInsideThis = false;
        }
            

        var colorOverLifetime = hexParticleSys.colorOverLifetime;
        Gradient gradient = new Gradient();
        gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(ragdollColor, 0.0f), new GradientColorKey(ragdollColor, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        colorOverLifetime.color = gradient;
        hexParticleSys.Play();

        Color initialHexColor = hexMat.color;

        
        int totalCountAtTheEnd = ragdollCount + hexCount;

        float iCount = 15;
        
        int incrementAmount = Mathf.CeilToInt(ragdollCount / iCount);
      

        for (int i = 0; i < iCount; i++)
        {
            if (hexCount <= totalCountAtTheEnd - incrementAmount)
            {
                
                hexCount += incrementAmount;
                SetCountText();
                
                
            }
            counter += Time.deltaTime / 2;

            //turn on and off the colors, no colors make the game easier
            if (DataScript.isColorsActive)
            {
                hexMat.color = Color.Lerp(initialHexColor, ragdollColor, counter / duration);
            }

            yield return new WaitForSecondsRealtime(0.01f);
        }

        hexCount = totalCountAtTheEnd;
        SetCountText();

        //turn on and off the colors, no colors make the game easier
        if (DataScript.isColorsActive)
        {
            hexColorNo = ragdollColorNo;
            hexMat.color = ragdollColor;
        }
        
        AddHexToCorrespondingList();
        DataScript.hexContainsRagdollWhenGameOver = this.gameObject;
       
        StopCoroutine(RagdollOnThis(ragdollColor, ragdollCount, ragdollColorNo));
        
    }
    

    public void SetCountText()
    {
        countText.text = hexCount.ToString();
    }

    void AddHexToCorrespondingList()
    {
        
        if(hexColorNo == 0)
        {
            DataScript.colorZeroList.Add(hexCount);
        }
        else if(hexColorNo == 1)
        {
            DataScript.colorOneList.Add(hexCount);
        }
        else if (hexColorNo == 2)
        {
            DataScript.colorTwoList.Add(hexCount);
        }
        else if (hexColorNo == 3)
        {
            DataScript.colorThreeList.Add(hexCount);
        }
        else if (hexColorNo == 4)
        {
            DataScript.colorFourList.Add(hexCount);
        }

        if (isInitialPlacementDone)
        {
            gameHandler.GameOverCheck();
        }

        isInitialPlacementDone = true;
    }

    void RemoveHexFromList()
    {
        if (hexColorNo == 0)
        {
            DataScript.colorZeroList.Remove(hexCount);
        }
        else if (hexColorNo == 1)
        {
            DataScript.colorOneList.Remove(hexCount);
        }
        else if (hexColorNo == 2)
        {
            DataScript.colorTwoList.Remove(hexCount);
        }
        else if (hexColorNo == 3)
        {
            DataScript.colorThreeList.Remove(hexCount);
        }
        else if (hexColorNo == 4)
        {
            DataScript.colorFourList.Remove(hexCount);
        }
    }

    public IEnumerator SkyScraperGameOverAnim()
    {
       
        float initialZ = transform.localScale.z;
        Vector3 hexScaler = transform.localScale;
        while(hexScaler.z < (initialZ + hexCount * 10))
        {
            
            hexScaler.z += 3f;

            if(hexScaler.z > DataScript.highestHexScale)
            {
                DataScript.highestHexScale = hexScaler.z;
                DataScript.highestPointOnAHex = transform.GetChild(0).position.y;
                
            }
            transform.localScale = hexScaler;
            if(DataScript.hexContainsRagdollWhenGameOver == this.gameObject)
            {
                GameObject ragdoll = GameObject.FindWithTag("Ragdoll");
                ragdoll.GetComponent<RagdollScript>().ragdollCountText.text = "";
                ragdoll.transform.localScale = Vector3.one * 0.9f;
                ragdoll.transform.position = transform.GetChild(0).position;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
