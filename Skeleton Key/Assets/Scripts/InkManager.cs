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
	private Story story;
	private bool textDone;
	private int buttonNumber;
	
	// UI stuff
	public Image blackBackground;
	public Image textboxPrefab;
	private Canvas uiCanvas;
	private Image dialogueBox;
	private TextMeshProUGUI dialogueText;
	private string currentText;
	private Button choicebutton1;
	private Button choicebutton2;
	private Button choicebutton3;
	private Button continuebutton;
	private Button exitbutton;

	public int maxCharactersPerBox;

	public void Start()
	{
		//Find all the UI components
		uiCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		dialogueBox = Instantiate<Image>(textboxPrefab, uiCanvas.transform);
		dialogueText = dialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		continuebutton = dialogueBox.transform.GetChild(1).GetComponent<Button>();
		exitbutton = dialogueBox.transform.Find("ExitIcon").GetComponent<Button>();
		exitbutton.onClick.AddListener(()=>ExitTextBox());
		choicebutton1 = GameObject.FindWithTag("ChoiceButton1").GetComponent<Button>();
		choicebutton1.onClick.AddListener(()=>ChoiceButtonPressed(0));
		choicebutton2 = GameObject.FindWithTag("ChoiceButton2").GetComponent<Button>();
		choicebutton2.onClick.AddListener(()=>ChoiceButtonPressed(1));
		choicebutton3 = GameObject.FindWithTag("ChoiceButton3").GetComponent<Button>();
		choicebutton3.onClick.AddListener(()=>ChoiceButtonPressed(2));
		continuebutton.onClick.AddListener(()=>ContinueButtonPressed());
		
		blackBackground.gameObject.SetActive(false);
		dialogueBox.gameObject.SetActive(false);
	}

	public void OpenDialoguePanel()
	{
		blackBackground.gameObject.SetActive(true);
		dialogueBox.gameObject.SetActive(true);
		choicebutton1.gameObject.SetActive(false);
		choicebutton2.gameObject.SetActive(false);
		choicebutton3.gameObject.SetActive(false);
		continuebutton.gameObject.SetActive(false);
		TextAsset storyFile = Resources.Load<TextAsset>("Skeleton Key inky file");
		Debug.Log("Story file loaded");
		story = new Story(storyFile.text);

		TextAppearStoryUpdate();
	}

	private void Update()
	{
	}
	
	public void TextAppearStoryUpdate()
	{
		currentText = "";
		dialogueText.text = "";
		while (story.canContinue)
		{
			currentText += story.Continue();
		}

		PrintStory();
		if (!story.canContinue && story.currentChoices?.Count == 0)
		{
			exitbutton.gameObject.SetActive(true);
		}
	}
	public void ShowChoiceButtons()
	{
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
							choicebutton1.GetComponent<TextMeshProUGUI>().text =
								choice.text.Trim();
							if (textDone)
							{
								choicebutton1.gameObject.SetActive(true);
							}

							break;
						case 1:
							choicebutton2.GetComponent<TextMeshProUGUI>().text =
								choice.text.Trim();
							if (textDone)
							{
								choicebutton2.gameObject.SetActive(true);
							}
							break;
						case 2:
							choicebutton3.GetComponent<TextMeshProUGUI>().text =
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
	private void PrintStory()
	{
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
		
		choicebutton1.gameObject.SetActive(false);
		choicebutton2.gameObject.SetActive(false);
		choicebutton3.gameObject.SetActive(false);
	}

	public void ExitTextBox()
	{
		dialogueBox.gameObject.SetActive(false);
		blackBackground.gameObject.SetActive(false);
	}

}


		
