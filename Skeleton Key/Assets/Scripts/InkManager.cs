using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class InkManager : MonoBehaviour
{
	[SerializeField] private TextAsset inkJSONAsset;

	public Coroutine printer;

	private Story story;
	//private StoryParser storyParser;


	[Header("Story Information")] [SerializeField]
	public StoryState CurrentStoryState;

	private bool textDone = false;

	public CharacterManager _characterManager;
	public GameManager _gameManager;

	// UI stuff
	private Canvas uiCanvas;
	private Image dialogueBox;
	private Text dialogue;
	private string currentText;
	private Button choicebutton1;
	private Button choicebutton2;
	private Button choicebutton3;

	[Header("Text Speed")] public float TextSpeed = 0.01f;

	[Header("Character Information")] [SerializeField]
	private string currentCharName;

	public Character[] CharacterData;

	private Dictionary<string, Character> characters;

	void Start()
	{
		//Find all the UI components
		uiCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		Debug.Log("Found the canvas!");
		dialogueBox = GameObject.FindWithTag("DialogueBox").GetComponent<Image>();
		Debug.Log("Found the dialogue box!");
		dialogue = dialogueBox.transform.GetChild(0).GetComponent<Text>();
		Debug.Log("Found the dialogue text!");
		choicebutton1 = GameObject.FindWithTag("ChoiceButton1").GetComponent<Button>();
		choicebutton2 = GameObject.FindWithTag("ChoiceButton2").GetComponent<Button>();
		choicebutton3 = GameObject.FindWithTag("ChoiceButton3").GetComponent<Button>();
		Debug.Log("Found the buttons!");
		_characterManager = GetComponent<CharacterManager>();
		_gameManager = GetComponent<GameManager>();

		characters = new Dictionary<string, Character>();
		foreach (Character character in CharacterData)
		{
			characters.Add(character.Name, character);
		}

		CurrentStoryState = StoryState.EpisodeStart;

		choicebutton1.gameObject.SetActive(false);
		choicebutton2.gameObject.SetActive(false);
		choicebutton3.gameObject.SetActive(false);

	}

	private void Update()
	{
		switch (CurrentStoryState)
		{
			case StoryState.EpisodeStart:
				EpisodeStart();
				break;
			case StoryState.Printing:
				PrintingStoryUpdate();
				break;
			case StoryState.Reading:
				ReadingStoryUpdate();
				break;
			case StoryState.EpisodeEnd:
				EpisodeEnd();
				break;
		}
	}

	private void PrintingStoryUpdate()
	{
		currentText = "";
		while (story.canContinue)
		{
			currentText += story.Continue();
		}
		
		CurrentStoryState = StoryState.Reading;
		PrintStory(Color.black);
		
		Debug.Log("Current Choices: " + story.currentChoices.Count);

		//Do we have a choice
		if (story.currentChoices.Count > 0)
		{
			if (story.currentChoices == null) return;
			
			if (story.currentChoices.Count > 0)
			{
				for (int i = 0; i < story.currentChoices.Count; i++)
				{
					Choice choice = story.currentChoices[i];
					switch (i)
					{
						case 0:
							choicebutton1.transform.GetChild(0).gameObject.GetComponent<Text>().text =
								choice.text.Trim();
							choicebutton1.gameObject.SetActive(true);
							break;
						case 1:
							choicebutton2.transform.GetChild(0).gameObject.GetComponent<Text>().text =
								choice.text.Trim();
							choicebutton2.gameObject.SetActive(true);
							break;
						case 2:
							choicebutton3.transform.GetChild(0).gameObject.GetComponent<Text>().text =
								choice.text.Trim();
							choicebutton3.gameObject.SetActive(true);
							break;
					}
					// Button button = storyParser.CreateChoiceView(choice.text.Trim());
					// Tell the button what to do when we press it
					// button.onClick.AddListener(delegate { OnClickChoiceButton(choice); });

				}
			}

			//The conversation has no choices
			else
			{
				choicebutton2.gameObject.SetActive(true);
				choicebutton2.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Continue";
			}
		}
		
	}
	
	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton (Choice choice) {
		story.ChooseChoiceIndex (choice.index);
	}

	private void PrintStory(Color textColor)
	{
		if (printer != null) StopCoroutine(printer);
		printer = StartCoroutine(PrintText(textColor));
	}

	//Update while the player is reading
	private void ReadingStoryUpdate()
	{
		/*if (textDone && (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)))
		{
			CurrentStoryState = StoryState.Printing;
		}*/
	}

	private void EpisodeStart()
	{
		TextAsset storyFile = Resources.Load<TextAsset>("Skeleton Key inky file");
		Debug.Log("Story file loaded");
		story = new Story(storyFile.text);
		CurrentStoryState = StoryState.Printing;
	}

	private void EpisodeEnd()
	{
		//chapter++;
		dialogue.text = "";
		currentCharName = "";
	}

	//Coroutine to print text letter-by-letter
	private IEnumerator PrintText(Color textColor)
	{
		for (int i = 0; i < currentText.Length; i++)
		{
			string subStr = currentText.Substring(0, i);
			dialogue.text = subStr;
			dialogue.color = textColor;

			if (Input.GetMouseButtonDown(0))
			{
				dialogue.text = currentText;
				break;
			}
			
			// check for a "sentence ender" - ".", "...", "?", "!"
			// pause (yield return null) if the last character in dialogue.text (dialogue.text[dialogue.text.Length - 1] is one of those sentence enders
			// continue on mouse down or click on button or whatever.
			// "clear" the text w/ continue is pressed AND "\n" is the beginning of the next string
			
			yield return new WaitForSeconds(TextSpeed);
			
		}

		textDone = true;
	}

	public void ChoiceButtonPressed(int buttonNumber)
	{
		story.ChooseChoiceIndex(buttonNumber);
		CurrentStoryState = StoryState.Printing;
		
		choicebutton1.gameObject.SetActive(false);
		choicebutton2.gameObject.SetActive(false);
		choicebutton3.gameObject.SetActive(false);
	}

	public void Quit()
	{
		Application.Quit();
	}
	
}

	public enum StoryState
	{
		EpisodeStart,
		Printing,
		Reading,
		EpisodeEnd
	}

	[Serializable]
	public struct Character
	{
		public string Name;
		public Sprite Sprite;
		public Color TextColor;
	}


		
