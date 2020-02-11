using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public GameObject ragdoll;
    public float cameraSize;
    public Vector3 ragdollStartPos;
    public int ragdollStartColorNo;
    //public int ragdollCount;
   
    void Start()
    {
        ragdoll = GameObject.FindWithTag("Ragdoll");
        if(ragdoll != null)
        {
            RagdollScript ragdollScript = ragdoll.GetComponent<RagdollScript>();

            ragdoll.transform.position = ragdollStartPos;

            //uncomment this to give ragdoll a color (one of hex colors) at the start of level
            //ragdollScript.ragdollColorNo = ragdollStartColorNo;

            //uncomment this to give ragdoll a count at the start of the level
            //ragdollScript.ragdollCount = ragdollCount;
            
        }
        
        Camera.main.orthographicSize = cameraSize;
    }

}
