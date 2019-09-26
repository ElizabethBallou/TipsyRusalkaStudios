using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BookController _bookController;
    public InkManager _inkManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayBook()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D collider = Physics2D.OverlapPoint(mousePos);

        if (collider != null)
        {
            Debug.Log(collider.gameObject.tag);
            if (collider.gameObject.CompareTag("book"))
            {
                _bookController.SummonBook();
            }
        }
    }
    
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
                    _inkManager.OpenDialoguePanel();
                }
            }
        }
    }
}
