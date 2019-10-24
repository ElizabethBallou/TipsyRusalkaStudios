using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private Canvas uiCanvas;
    public GameObject descriptionPanel;
    public GameObject dialoguePanel;
    public GameObject bookPanel;
    private int UIToggler = 0;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        uiCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (UIToggler)
        {
            case 0:
                descriptionPanel.SetActive(false);
                dialoguePanel.SetActive(false);
                bookPanel.SetActive(false);
                break;
            case 1:
                //set description panel open
                descriptionPanel.SetActive(true);
                dialoguePanel.SetActive(false);
                bookPanel.SetActive(false);
                break;
            case 2:
                //set dialogue panel open
                descriptionPanel.SetActive(false);
                dialoguePanel.SetActive(true);
                bookPanel.SetActive(false);
                break;
            case 3:
                //set book panel open
                descriptionPanel.SetActive(false);
                dialoguePanel.SetActive(false);
                bookPanel.SetActive(true);
                break;
        }
    }

    public void SwitchUIState(UIState uiState)
    {
        UIToggler = (int) uiState;
    }
    public void OpenNPCDialogue()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos, Vector3.forward, 1000f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null)
            {
                if (hit[i].collider.gameObject.CompareTag("NPC"))
                {
                    SwitchUIState(UIState.DialoguePanelState);
                    InkManager.instance.OpenDialoguePanel(hit[i].collider.name);
                }
            }
        }
    }

    public void ExamineItem()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos, Vector3.forward, 1000f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null && hit[i].collider.gameObject
                    .GetComponent<InteractionObject>() != null)
            {
                SwitchUIState(UIState.DescriptionPanelState);
                DescriptionController.instance.ShowDescriptionBox(hit[i].collider.gameObject
                    .GetComponent<InteractionObject>().descriptionKey);
            }
        }
    }
    
    public void SummonBook()
    {
        SwitchUIState(UIState.BookPanelState);
        BookController.instance.ChooseRandomString();
    }
}

public enum UIState
{
    NoMotherfuckingUI,
    DescriptionPanelState,
    DialoguePanelState,
    BookPanelState
}
