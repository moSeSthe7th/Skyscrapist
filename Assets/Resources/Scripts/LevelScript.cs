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
            ragdollScript.ragdollColorNo = ragdollStartColorNo;
            //ragdollScript.ragdollCount = ragdollCount;
            
        }
        
        Camera.main.orthographicSize = cameraSize;
    }

}
