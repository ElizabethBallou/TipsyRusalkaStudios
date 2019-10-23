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

	public static InkManager instance;
	private Story story;
	private bool textDone;
	private int buttonNumber;
	
	// UI stuff
	public Image blackBackground;
	public Image textboxPrefab;
	private Canvas uiCanvas;
	private Image dialogueBox;
	private TextMeshProUGUI dialogueText;
	private TextMeshProUGUI characterNameText;
	private string currentText;
	private Button choicebutton1;
	private Button choicebutton2;
	private Button choicebutton3;
	private Button continuebutton;
	private Button exitbutton;

	public int maxCharactersPerBox;

	public void Start()
	{
		//establish the singleton
		instance = this;
		
		//Find all the UI components
		uiCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		dialogueBox = Instantiate<Image>(textboxPrefab, uiCanvas.transform);
		dialogueText = dialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		characterNameText = dialogueBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
		
		continuebutton = dialogueBox.transform.GetChild(2).GetComponent<Button>();
		exitbutton = dialogueBox.transform.Find("ExitIcon").GetComponent<Button>();
		choicebutton1 = GameObject.FindWithTag("ChoiceButton1").GetComponent<Button>();
		choicebutton2 = GameObject.FindWithTag("ChoiceButton2").GetComponent<Button>();
		choicebutton3 = GameObject.FindWithTag("ChoiceButton3").GetComponent<Button>();
		
		//add button click functionality to newly instantiated prefab
		exitbutton.onClick.AddListener(()=>ExitTextBox());
		choicebutton1.onClick.AddListener(()=>ChoiceButtonPressed(0));
		choicebutton2.onClick.AddListener(()=>ChoiceButtonPressed(1));
		choicebutton3.onClick.AddListener(()=>ChoiceButtonPressed(2));
		continuebutton.onClick.AddListener(()=>ContinueButtonPressed());
		
		//deactivate the UI so it doesn't get in the way
		blackBackground.gameObject.SetActive(false);
		dialogueBox.gameObject.SetActive(false);
		//
		//
	
		TextAsset storyFile = Resources.Load<TextAsset>("master_dialogue_file");
		Debug.Log("Story file loaded");
		story = new Story(storyFile.text);
	}

	public void OpenDialoguePanel(string characterName)
	{
		blackBackground.gameObject.SetActive(true);
		dialogueBox.gameObject.SetActive(true);
		choicebutton1.gameObject.SetActive(false);
		choicebutton2.gameObject.SetActive(false);
		choicebutton3.gameObject.SetActive(false);
		continuebutton.gameObject.SetActive(false);


		KnotSelection(characterName);
	}

	private void KnotSelection(string characterName)
	{
		bool seenThisCharacter = (bool) EvaluateInkBool("seen_" + characterName);
		if (seenThisCharacter == false)
		{
			story.ChoosePathString(characterName + "_first_conversation_knot");
		}
		
		else
		{
			story.ChoosePathString(characterName + "_default_conversation_knot");
		}
		TextAppearStoryUpdate();
	}

	//For some reason, Unity reads Ink bools as ints (0, 1). This function converts them into bools...which is what they should be (grr)
	public bool EvaluateInkBool(string inkVariableName)
	{
		int inkNumber = (int) story.variablesState[inkVariableName];
		if (inkNumber == 0)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
	
	public void TextAppearStoryUpdate()
	{
		choicebutton1.gameObject.SetActive(false);
		choicebutton2.gameObject.SetActive(false);
		choicebutton3.gameObject.SetActive(false);
		continuebutton.gameObject.SetActive(false);
		textDone = false;
		currentText = "";
		dialogueText.text = "";
		while (story.canContinue)
		{
			currentText += story.ContinueMaximally();
		}

		PrintStory();

	}
	public void ShowChoiceButtons()
	{
		//Do we have a choice? If so, run the following code...
		if (story.currentChoices == null) return;
			
			if (story.currentChoices.Count > 0)
			{
				for (int i = 0; i < story.currentChoices.Count; i++)
				{
					Choice choice = story.currentChoices[i];
					switch (i)
					{
						case 0:
							choicebutton1.GetComponentInChildren<TextMeshProUGUI>().text =
								"- " + choice.text.Trim();
							if (textDone)
							{
								choicebutton1.gameObject.SetActive(true);
							}

							break;
						case 1:
							choicebutton2.GetComponentInChildren<TextMeshProUGUI>().text =
								"- " + choice.text.Trim();
							if (textDone)
							{
								choicebutton2.gameObject.SetActive(true);
							}
							break;
						case 2:
							choicebutton3.GetComponentInChildren<TextMeshProUGUI>().text =
								"- " + choice.text.Trim();
							if (textDone)
							{
								choicebutton3.gameObject.SetActive(true);
							}
							break;
					}
				}
			}
	}

	//Function to print text letter-by-letter
	private void PrintStory()
	{
		//set the character name, which may change mid-conversation
		string characterNameString = (string) story.variablesState["character_name"];
		characterNameText.text = characterNameString;
		// if dialogue.text == "", do nothing
		// remove dialogue.text from the front of current text, then do the rest
		currentText = currentText.Substring(dialogueText.text.Length, currentText.Length - dialogueText.text.Length);
		if (currentText.Length < maxCharactersPerBox)
		{
			dialogueText.text = currentText;
			textDone = true;
			continuebutton.gameObject.SetActive(false);
			ShowChoiceButtons();
			
		}
		else
		{
			for (int i = maxCharactersPerBox; i < currentText.Length; i++)
			{
				//Debug.Log("loop number " + i);
				string subStr = currentText.Substring(0, i);
				dialogueText.text = subStr;
				
				// check for a "sentence ender" - ".", "...", "?", "!"
				// pause (yield return null) if the last character in dialogue.text (dialogue.text[dialogue.text.Length - 1] is one of those sentence enders

				if (dialogueText.text.Length >= maxCharactersPerBox)
				{
					//charNumber = dialogue.text.Length;
					switch (dialogueText.text[dialogueText.text.Length - 1])
					{
						case '.':
							continuebutton.gameObject.SetActive(true);
							return;
						case '-':
							continuebutton.gameObject.SetActive(true);
							return;
						case '!':
							continuebutton.gameObject.SetActive(true);
							return;
						case '?':
							continuebutton.gameObject.SetActive(true);
							return;
						default:
							Debug.Log("No sentence ender here..");
							break;
					}
				}
			}
		}
		if (!story.canContinue && story.currentChoices.Count == 0)
		{
			if (textDone)
			{
				exitbutton.gameObject.SetActive(true);
			}
		}
	}

	public void ContinueButtonPressed()
	{
		PrintStory();

		if (textDone)
		{
			ShowChoiceButtons();
		}
	}

	// When we click the choice button, tell the story to choose that choice!
	public void ChoiceButtonPressed(int buttonNumber)
	{
		story.ChooseChoiceIndex(buttonNumber);
		TextAppearStoryUpdate();
	}

	public void ExitTextBox()
	{
		dialogueBox.gameObject.SetActive(false);
		blackBackground.gameObject.SetActive(false);
	}

}


		
