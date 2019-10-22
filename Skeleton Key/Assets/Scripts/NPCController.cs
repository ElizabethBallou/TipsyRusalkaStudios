using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class Conversation
{
    public TextAsset text;
    public string knotName;
}
public class NPCController : MonoBehaviour
{

    public static NPCController instance;
    private List<GameObject> NPCList = new List<GameObject>();
    private GameObject[] NPCfiller;
    public List<string> Keys = new List<string>();
    public List<Conversation> Values = new List<Conversation>();
    public Dictionary<string, Conversation> CharacterData = new Dictionary<string, Conversation>();
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
        
    }

    public void RoomNPCSetter()
    {
       NPCfiller = GameObject.FindGameObjectsWithTag("NPC");
       
        foreach (GameObject NPC in NPCfiller)
        { 
            NPCList.Add(NPC);
            Debug.Log("Loaded NPC: " + NPC.name);
        }
    }
    
    
}
