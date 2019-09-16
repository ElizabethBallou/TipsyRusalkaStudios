using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Random = System.Random;

public class BookController : MonoBehaviour
{
    private Text bookText;
    private string RandomString;
    public int ChunkSize; //how many chunks will be printed into the text object
    public int WordChunkLength = 0; //how many words are in each chunk
    private List<string> splitString = new List<string>();
    private int SpaceCounter = 0;
    private string tempString;

    // Start is called before the first frame update
    void Start()
    {
        bookText = GameObject.FindWithTag("BookText")
            .GetComponent<Text>(); //locate the text object we will be using to display the book
        string sourceTextPath = "Assets/Resources/Arabian Nights text.txt"; //locate the source .txt file
        string sourceText = File.ReadAllText(sourceTextPath); //get all the text of the book
        Debug.Log("loaded Arabian Nights");
        bookText.text = sourceText;
        Debug.Log("The number of characters in this book is " + sourceText.Length);
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
        
        //splitString = sourceText.Split(new string[] {" "}, StringSplitOptions.None); //split the text into an array of 1-word strings
        //Debug.Log("The number of words in this book is " + splitString.Length);
    }
    
    // Update is called once per frame
    void Update()
    {
    }

    public void OnButtonClick()
    {
        ChooseRandomString();
    }

    public void ChooseRandomString()
    {
        bookText.text = ""; //resets the text

        for (int i = 0; i < ChunkSize; i++)
        {
            RandomString = splitString[UnityEngine.Random.Range(0, splitString.Count)];
            bookText.text = bookText.text + RandomString; 
        }
    }
}