using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class InkManager : MonoBehaviour
{
	[SerializeField] private TextAsset inkJSONAsset;
	private Story story;

	public Canvas canvas;
	public CharacterManager _characterManager;
	public GameManager _gameManager;

	// UI Prefabs
	private Image textboxPrefab;
	private Text textPrefab;
	private Button buttonPrefab;

	//private Image textBox;
	//private Text storyText;
	

	void Start()
	{
		//textboxPrefab = Resources.Load<Image>("Prefabs/Ink Text Box");
		//textBox = (Image) GameObject.Instantiate(textboxPrefab);
		textPrefab = Resources.Load<Text>("Prefabs/Ink Text");
		//storyText = (Text) GameObject.Instantiate(textPrefab);
		buttonPrefab = Resources.Load<Button>("Prefabs/Ink Continue Button");
		//textPrefab.rectTransform.SetParent(textboxPrefab.rectTransform, true);
		//textboxPrefab.enabled = false;
		_characterManager = GetComponent<CharacterManager>();
		_gameManager = GetComponent<GameManager>();
		// Remove the default message
		StartStory();
	}

	// Creates a new Story object with the compiled story which we can then play!
	void StartStory()
	{
		//textboxPrefab.enabled = true;
		story = new Story(inkJSONAsset.text);
		story.BindExternalFunction("place_actor",
			(string leftName, string rightName) => { _characterManager.PlaceActor(leftName, rightName); });
		story.BindExternalFunction("change_emotion",
			(string emotion, int ID) => { _characterManager.ChangeActorEmotion(emotion, ID); });

		RefreshView();
	}

	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	void RefreshView()
	{
		// Remove all the UI on screen
		RemoveChildren();

		// Read all the content until we can't continue any more
		while (story.canContinue)
		{
			// Continue gets the next line of the story
			string text = story.Continue();
			// This removes any white space from the text.
			text = text.Trim();
			// Display the text on screen!
			CreateContentView(text);
		}

		// Display all the choices, if there are any!
		if (story.currentChoices.Count > 0)
		{
			for (int i = 0; i < story.currentChoices.Count; i++)
			{
				Choice choice = story.currentChoices[i];
				Button button = CreateChoiceView(choice.text.Trim());
				// Tell the button what to do when we press it
				button.onClick.AddListener(delegate { OnClickChoiceButton(choice); });
			}
		}
		// If we've read all the content and there's no choices, the story is finished!
		else
		{
			Button choice = CreateChoiceView("End of story.\nRestart?");
			choice.onClick.AddListener(delegate { StartStory(); });
		}
	}

	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton(Choice choice)
	{
		story.ChooseChoiceIndex(choice.index);
		RefreshView();
	}

	// Creates a button showing the choice text
	void CreateContentView(string text)
	{
		Text storyText = Instantiate(textPrefab) as Text;
		storyText.text = text;
		storyText.transform.SetParent(canvas.transform);
	}

	// Creates a button showing the choice text
	Button CreateChoiceView(string text)
	{
		// Creates the button from a prefab
		Button choice = Instantiate(buttonPrefab) as Button;
		choice.transform.SetParent(canvas.transform);

		// Gets the text from the button prefab
		Text choiceText = choice.GetComponentInChildren<Text>();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
		layoutGroup.childForceExpandHeight = false;

		return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	void RemoveChildren()
	{
		for (int i = 0; i < canvas.transform.childCount; i++)
		{
			if (canvas.transform.GetChild(i).CompareTag("destroyable"))
			{
				Destroy(gameObject);
			}
		}
	}

}
