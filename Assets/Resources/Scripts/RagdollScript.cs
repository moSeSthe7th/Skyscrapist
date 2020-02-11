using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtmostInput;
using UnityEngine.UI;

public class RagdollScript : MonoBehaviour
{
    public int ragdollColorNo;
    public int ragdollCount;
    private InputX inputX;

    private float initialYPos;

    private Material ragdollMat;
    private Text ragdollCountText;

    Animator animator;

    float tParam;
    public Transform route;
    Transform r0;
    Transform r1;
    Transform r2;
    Transform r3;
    public float moveSpeed;

    Vector3 initialScale;

    void Start()
    {
        inputX = new InputX();
        ragdollMat = GetComponentInChildren<Renderer>().material;

        //uncomment this to give ragdoll a color (one of hex colors) at the start of level
        //ragdollMat.color = DataScript.hexColors[ragdollColorNo];

        ragdollCountText = GetComponentInChildren<Text>();
        SetCountText();
        initialYPos = transform.position.y;

        animator = GetComponent<Animator>();

        r0 = route.GetChild(0);
        r1 = route.GetChild(1);
        r2 = route.GetChild(2);
        r3 = route.GetChild(3);
        tParam = 0;
        moveSpeed = 1f;

        initialScale = transform.localScale;
    }

    
    void Update()
    {
        if (inputX.GetInputs() && !DataScript.inputLock)
        {
            GeneralInput gInput = inputX.GetInput(0);

            if(gInput.phase == IPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(gInput.currentPosition);
                RaycastHit raycastHit;

                if(Physics.Raycast(ray, out raycastHit))
                {
                    if(raycastHit.transform.gameObject.tag == "Hex")
                    {
                        StartCoroutine(CarryRagdollToNewPos(raycastHit.transform.gameObject));
                       
                    }
                    
                }
            }
        }
    }

    void SetRagdollPosition()
    {
        Vector3 rPos0 = r0.position;
        Vector3 rPos1 = r1.position;
        Vector3 rPos2 = r2.position;
        Vector3 rPos3 = r3.position;

        Vector3 ragdollPosOnBezier = Mathf.Pow(1 - tParam, 3) * rPos0 +
                     3 * Mathf.Pow(1 - tParam, 2) * tParam * rPos1 +
                     3 * (1 - tParam) * Mathf.Pow(tParam, 2) * rPos2 +
                     Mathf.Pow(tParam, 3) * rPos3;


       
        
        transform.position = ragdollPosOnBezier;
    }

    void SetJumpBeziers(Vector3 from, Vector3 to)
    {
        Vector3 middlePoint = new Vector3();
        middlePoint.x = (from.x + to.x) / 2;
        middlePoint.y = 15f;
        middlePoint.z = (from.z + to.z) / 2;

        from.y = initialYPos;
        to.y = initialYPos;

        to.z = to.z - 0.3f;

        r0.position = from;
        r1.position = middlePoint;
        r2.position = middlePoint;
        r3.position = to;
    }

    IEnumerator CarryRagdollToNewPos(GameObject hex)
    {
        SkyscraperScript skyscraperScript = hex.GetComponent<SkyscraperScript>();

        DataScript.inputLock = true;

        SetJumpBeziers(transform.position, hex.transform.position);
        tParam = 0;

        //Ragdoll look to target rotation
        Quaternion targetRotation = Quaternion.LookRotation(r3.position - transform.position);
        for (int k = 0; k < 10; k++)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        //-----------------------------------
        
        //Ragdoll move to target rotation
        animator.SetBool("isJumping", true);
        animator.SetBool("isOnAir", true);
        for (int j = 0; j < 20; j++)
        {
            tParam += 0.05f;
            SetRagdollPosition();

            if (tParam < 0.6f)
            {
                if(tParam > 0.3f)
                {
                    animator.SetBool("isJumping", false);
                    animator.SetBool("isOnAir", false);
                }
                transform.localScale += Vector3.one * 0.02f;
            }
            else
            {
                transform.localScale -= Vector3.one * 0.03f;
            }
            yield return new WaitForEndOfFrame();
        }
        
        //-----------------------------------


        Color initialRagdollColor = ragdollMat.color;
        Color initialHexColor = hex.GetComponent<Renderer>().material.color;
        int iCount = 15;
        float counter = 0;
        float duration = 0.3f;
        int hexCount = skyscraperScript.hexCount;
        int diff = hexCount - ragdollCount;
        int incrementAmount = Mathf.CeilToInt(diff / iCount);

        //Set hex count and color in its script
        StartCoroutine(skyscraperScript.RagdollOnThis(initialRagdollColor,ragdollCount,ragdollColorNo));

        //Set ragdoll count and color
        for (int i = 0; i < iCount; i++)
        {

            if(Mathf.Abs(ragdollCount - hexCount) > incrementAmount)
            {
                ragdollCount += incrementAmount;
                SetCountText();
            }

            counter += Time.deltaTime / 2;

            //turn on and off the colors, no colors make the game easier
            if (DataScript.isColorsActive)
            {
                ragdollMat.color = Color.Lerp(initialRagdollColor, initialHexColor, counter / duration);
            }
            

            yield return new WaitForEndOfFrame();
        }
        //--------------------------------

        //making things clear before finish
        transform.localScale = initialScale;
       /* Vector3 newPos = transform.position;
        newPos = new Vector3(hex.transform.position.x, initialYPos, hex.transform.position.z);
        transform.position = newPos;*/
        ragdollCount = hexCount;
        SetCountText();

        //turn on and off the colors, no colors make the game easier
        if (DataScript.isColorsActive)
        {
            ragdollMat.color = initialHexColor;
            ragdollColorNo = skyscraperScript.hexColorNo;
        }

        
        DataScript.inputLock = false;
        StopCoroutine(CarryRagdollToNewPos(hex));
    }

    void SetCountText()
    {
        ragdollCountText.text = ragdollCount.ToString();
    }
}
