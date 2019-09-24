using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class InkManager : MonoBehaviour
{
	[SerializeField] private TextAsset inkJSONAsset;
	
	private Story story;

	[Header("Story Information")] [SerializeField]
	public StoryState CurrentStoryState;

	private bool textDone = false;

	private int buttonNumber;
	
	// UI stuff
	public GameObject textboxPrefab;
	private Canvas uiCanvas;
	private GameObject dialogueBox;
	private TextMeshProUGUI dialogue;
	private string currentText;
	private Button choicebutton1;
	private Button choicebutton2;
	private Button choicebutton3;
	private Button continuebutton;
	private Button exitbutton;

	public int maxCharactersPerBox;

	public void OpenDialoguePanel()
	{
		//Find all the UI components
		uiCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		dialogueBox = Instantiate(textboxPrefab, uiCanvas.transform);
		dialogueBox.GetComponent<Image>().sprite = Resources.Load("Prefabs/Ink Text Box") as Sprite;
		dialogue = dialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		continuebutton = dialogueBox.transform.GetChild(1).GetComponent<Button>();
		exitbutton = dialogueBox.transform.Find("ExitIcon").GetComponent<Button>();
		choicebutton1 = GameObject.FindWithTag("ChoiceButton1").GetComponent<Button>();
		choicebutton1.onClick.AddListener(()=>ChoiceButtonPressed(0));
		choicebutton2 = GameObject.FindWithTag("ChoiceButton2").GetComponent<Button>();
		choicebutton2.onClick.AddListener(()=>ChoiceButtonPressed(1));
		choicebutton3 = GameObject.FindWithTag("ChoiceButton3").GetComponent<Button>();
		choicebutton3.onClick.AddListener(()=>ChoiceButtonPressed(2));
		
		CurrentStoryState = StoryState.EpisodeStart;
		
		continuebutton.gameObject.SetActive(false);
	}

	private void Update()
	{
		//Debug.Log(CurrentStoryState.ToString());

		if (Input.GetKeyDown(KeyCode.Space)) ContinueButtonPressed();
		
		switch (CurrentStoryState)
		{
			case StoryState.EpisodeStart:
				EpisodeStart();
				break;
			case StoryState.TextAppear:
				TextAppearStoryUpdate();
				break;
			case StoryState.EpisodeEnd:
				break;
		}
	}

	private void EpisodeStart()
	{
		TextAsset storyFile = Resources.Load<TextAsset>("Skeleton Key inky file");
		Debug.Log("Story file loaded");
		story = new Story(storyFile.text);

		TextAppearStoryUpdate();
	}
	public void TextAppearStoryUpdate()
	{
		currentText = "";
		dialogue.text = "";
		while (story.canContinue)
		{
			currentText += story.Continue();
		}

		PrintStory(Color.black);
		Debug.Log(story.currentChoices?.Count);
		if (!story.canContinue && story.currentChoices?.Count == 0)
		{
			ExitButtonAppear();
		}
	}
	public void ShowChoiceButtons()
	{
		//Change the Storystate so that the player can choose one of the buttons
		CurrentStoryState = StoryState.WaitForInteraction;
	//Do we have a choice? If so, run the following code...
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
							choicebutton1.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text =
								choice.text.Trim();
							if (textDone)
							{
								choicebutton1.gameObject.SetActive(true);
							}

							break;
						case 1:
							choicebutton2.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text =
								choice.text.Trim();
							if (textDone)
							{
								choicebutton2.gameObject.SetActive(true);
							}
							break;
						case 2:
							choicebutton3.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text =
								choice.text.Trim();
							if (textDone)
							{
								choicebutton3.gameObject.SetActive(true);
							}
							break;
					}
				}
			}
		}
	}

	//Function to print text letter-by-letter
	private void PrintStory(Color textColor)
	{
		// if dialogue.text == "", do nothing
		// remove dialogue.text from the front of current text, then do the rest

		currentText = currentText.Substring(dialogue.text.Length, currentText.Length - dialogue.text.Length);
		
		if (currentText.Length < maxCharactersPerBox)
		{
			dialogue.text = currentText;
			dialogue.color = textColor;
			textDone = true;
			continuebutton.gameObject.SetActive(false);
			ShowChoiceButtons();
			
		}
		else
		{
			dialogue.color = textColor;
			
			for (int i = 199; i < currentText.Length; i++)
			{
				string subStr = currentText.Substring(0, i);
				dialogue.text = subStr;
				
				// check for a "sentence ender" - ".", "...", "?", "!"
				// pause (yield return null) if the last character in dialogue.text (dialogue.text[dialogue.text.Length - 1] is one of those sentence enders

				if (dialogue.text.Length >= maxCharactersPerBox)
				{
					//charNumber = dialogue.text.Length;
					switch (dialogue.text[dialogue.text.Length - 1])
					{
						case '.':
							return;
						case ',':
							return;
						case '-':
							return;
						case '!':
							return;
						case '?':
							return;
					
					}
					continuebutton.gameObject.SetActive(true);
				}
			}
		}
	}

	public void ContinueButtonPressed()
	{
		PrintStory(Color.black);

		if (textDone)
		{
			ShowChoiceButtons();
		}
	}

	// When we click the choice button, tell the story to choose that choice!
	public void ChoiceButtonPressed(int buttonNumber)
	{
		story.ChooseChoiceIndex(buttonNumber);
		CurrentStoryState = StoryState.TextAppear;
		
		choicebutton1.gameObject.SetActive(false);
		choicebutton2.gameObject.SetActive(false);
		choicebutton3.gameObject.SetActive(false);
	}

	public void ExitButtonAppear()
	{
		exitbutton.gameObject.SetActive(true);
	}

}

	public enum StoryState
	{
		IdleStory,
		EpisodeStart,
		TextAppear,
		WaitForInteraction,
		EpisodeEnd
	}


		
