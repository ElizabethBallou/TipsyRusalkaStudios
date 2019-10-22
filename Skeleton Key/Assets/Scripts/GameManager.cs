using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BookController _bookController;
    public InkManager _inkManager;
    public GameObject bookPrefab;

    private Canvas uiCanvas;
    public static GameManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Elizabeth you fucked up there's another gamemanager in the scene!'");
            Destroy(gameObject);
            return;
        }

        instance = this;
        uiCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        GameObject newBook = Instantiate(bookPrefab, uiCanvas.transform);
        
        newBook.SetActive(false);
        _bookController = newBook.GetComponent<BookController>();

    }

    public void Start()
    {
        NPCController.instance.RoomNPCSetter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayItem()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos, Vector3.forward, 1000f);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider != null)
                {
                    if (hit[i].collider.gameObject.CompareTag("NPC"))
                    {
                        //this may not always be the case BUT FOR NOW IT IS
                        _inkManager.OpenDialoguePanel(hit[i].collider.name);
                    }
                    
                    else if (hit[i].collider.gameObject.CompareTag("book"))
                    {
                        _bookController.SummonBook();
                    }
                }
            }
            
    }
}
