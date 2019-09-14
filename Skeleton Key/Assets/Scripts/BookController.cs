using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class BookController : MonoBehaviour
{
    private Text bookText;
    
    // Start is called before the first frame update
    void Start()
    {
        bookText = GameObject.FindWithTag("BookText").GetComponent<Text>();
        string sourceTextPath = "Assets/Resources/Arabian Nights text.txt"; //locate the source .txt file
        string sourceText = File.ReadAllText(sourceTextPath); //get each line in the file
        Debug.Log("loaded Arabian Nights");
        bookText.text = sourceText;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
