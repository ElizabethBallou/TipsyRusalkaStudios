using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using VSCodeEditor;
using Random = System.Random;

public class BookController : MonoBehaviour
{
    private GameObject myBook;
    public TextMeshProUGUI leftPageText;
    public TextMeshProUGUI rightPageText;
    private string sourceText;
    private string RandomString;
    public int ChunkSize; //how many chunks will be printed into the text object
    public int WordChunkLength = 0; //how many words are in each chunk
    private List<string> splitString = new List<string>();
    private int SpaceCounter = 0;
    private string tempString;
    public Button leftButton;
    public Button rightButton;
    public Button exitButton;

    // Start is called before the first frame update
    void Awake()
    {
        //load in all the ui objects
        //myBook = Instantiate(bookPrefab, uiCanvas.transform);
        //leftPageText = GameObject.Find("LeftPage").GetComponent<TextMeshProUGUI>();
        //locate the text objects we will be using to display the book
        //rightPageText = GameObject.Find("RightPage").GetComponent<TextMeshProUGUI>();
        //leftButton = GameObject.Find("RandomizeLeft").GetComponent<Button>();
        //rightButton = GameObject.Find("RandomizeRight").GetComponent<Button>();
        //exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        //add appropriate listeners to buttons
        //leftButton.onClick.AddListener(()=>ChooseRandomString());
        //rightButton.onClick.AddListener(()=>ChooseRandomString());
        //exitButton.onClick.AddListener(()=>OnButtonExit());
        
        //locate the source .txt file
        string sourceTextPath = "Assets/Resources/Arabian Nights text.txt"; 
        //get all the text of the book
        sourceText = File.ReadAllText(sourceTextPath);
        Debug.Log("loaded Arabian Nights");
        for (int i = 0; i < sourceText.Length; i++)
        {
            // add to tempString the character that's being run through in this for loop
            tempString += sourceText[i];
            if (sourceText[i] == ' ')
            {
                SpaceCounter++;
            }

            if (SpaceCounter == WordChunkLength)
            {
                splitString.Add(tempString);
                SpaceCounter = 0;
                //set equal to an empty string
                tempString = "";
            }
        }
        
    }
    
    // Update is called once per frame
    public void SummonBook()
    {
        gameObject.SetActive(true);
        ChooseRandomString();
    }

    public void ChooseRandomString()
    {
        leftPageText.text = ""; //resets the text
        rightPageText.text = "";

        for (int i = 0; i < ChunkSize; i++)
        {
            RandomString = splitString[UnityEngine.Random.Range(0, splitString.Count)];
            leftPageText.text = leftPageText.text + RandomString; 
        }

        for (int i = 0; i < ChunkSize; i++)
        {
            RandomString = splitString[UnityEngine.Random.Range(0, splitString.Count)];
            rightPageText.text = rightPageText.text + RandomString;
        }
    }

    public void OnButtonExit()
    {
        gameObject.SetActive(false);
    }
}