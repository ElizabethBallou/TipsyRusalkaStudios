using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BookController _bookController;
    public GameObject bookPrefab;

    private Canvas uiCanvas;
    public static GameManager instance;
    
    public LayerMask interactObj;

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
        
    }

    public void Start()
    {
        UIManager.instance.SwitchUIState(UIState.NoMotherfuckingUI);
        NPCController.instance.RoomNPCSetter();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ShowMouseHover();
    }

    

    private void ShowMouseHover()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos, Vector3.forward, 1000f, interactObj);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null)
            {
                hit[i].collider.gameObject.GetComponent<InteractionObject>().ShowOutline();
            }
        }
        
        
    }
}
