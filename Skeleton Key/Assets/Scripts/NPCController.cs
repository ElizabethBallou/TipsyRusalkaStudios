using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public InkManager _inkManager;
    private List<GameObject> NPCList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //_inkManager = GetComponent<InkManager>();
        GameObject[] NPCfiller = GameObject.FindGameObjectsWithTag("NPC");

        foreach (GameObject NPC in NPCfiller)
        {
            NPCList.Add(NPC);
            Debug.Log("Loaded NPC: " + NPC.name);
        }
    }
    
    
}
