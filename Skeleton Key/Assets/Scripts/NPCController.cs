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

    // Update is called once per frame
    public void DisplayTextBox()
    {
        //if the story is idle, then the text box can appear. If it isn't idle, we can't instantiate a second text box
            if (_inkManager.CurrentStoryState == StoryState.IdleStory)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Collider2D collider = Physics2D.OverlapPoint(mousePos);

                if (collider != null)
                {
                    if (collider.gameObject.CompareTag("NPC"))
                    {
                        ClickNPC();
                    }
                }
            }
    }

    public void ClickNPC()
    {
        _inkManager.OpenDialoguePanel();

    }
}
