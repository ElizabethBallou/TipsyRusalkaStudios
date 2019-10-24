using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionController : MonoBehaviour
{

    public static DescriptionController instance;
    
    public Image DescriptionBox;

    private TextMeshProUGUI DescriptionText;

    private Button DescriptionExitButton;

    private string DescriptionString;

    private Dictionary<string, string> Descriptions;

    void Awake()
    {
        instance = this;
        Debug.Log("the description controller exists");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        DescriptionText = DescriptionBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        DescriptionExitButton = DescriptionBox.transform.GetChild(1).GetComponent<Button>();
        DescriptionBox.gameObject.SetActive(false);
        
        LoadDescriptions();
    }

    public void LoadDescriptions()
    {
        Descriptions = new Dictionary<string, string>();
        var descriptionsFile = Resources.Load<TextAsset>("Descriptions/Descriptions");
        var splitDescriptions = descriptionsFile.text.Split('\n');
        for (int i = 0; i < splitDescriptions.Length; i++)
        {
            var splitLine = splitDescriptions[i].Split('\t');
            Descriptions.Add(splitLine[0], splitLine[1]);
        }

        Debug.Log(Descriptions["Desk"]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDescriptionBox(string key)
    {
        DescriptionBox.gameObject.SetActive(true);
        DescriptionText.text = Descriptions[key];
    }

    public void CloseDescriptionBox()
    {
        UIManager.instance.SwitchUIState(UIState.NoMotherfuckingUI);
    }
}
