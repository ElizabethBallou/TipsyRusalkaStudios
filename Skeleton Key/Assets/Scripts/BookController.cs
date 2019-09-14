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
    private int bookCharacterNumber = 0;
    private char[] allBookChars;
    private string RandomWord;
    private string[] splitString;

    // Start is called before the first frame update
    void Start()
    {
        bookText = GameObject.FindWithTag("BookText")
            .GetComponent<Text>(); //locate the text object we will be using to display the book
        string sourceTextPath = "Assets/Resources/Arabian Nights text.txt"; //locate the source .txt file
        string sourceText = File.ReadAllText(sourceTextPath); //get all the text of the book
        Debug.Log("loaded Arabian Nights");
        bookText.text = sourceText;

        foreach (char x in sourceText)
        {
            bookCharacterNumber++;
        }

        Debug.Log("The number of characters in this book is " + bookCharacterNumber);
        splitString = sourceText.Split(new string[] {" "}, StringSplitOptions.None); //split the text into an array of 1-word strings
        Debug.Log("The number of words in this book is " + splitString.Length);
    }
    
    // Update is called once per frame
    void Update()
    {
    }

    public void OnButtonClick()
    {
        ChooseRandomWord();
    }

    public void ChooseRandomWord()
    {
        //RandomWord = splitString[Random.Range(0, splitString.Length)];
    }
}