using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;

public class StoryParser
{
    private Story story;

    private TextAsset myText;

    private Canvas myCanvas;

    private Button myButton;
    public StoryParser(TextAsset text)
    {
        myText = text;
        LoadStory();
    }
    
    private void LoadStory()
    {
        story = new Story(myText.text);
        foreach (string variable in story.variablesState)
        {
            Debug.Log(story.variablesState[variable]);
        }
        Debug.Log("story loaded");
    }

    //Gets next line of the story
    public string NextLine()
    {
        if(story.canContinue)
            return story.Continue();
        return "";
    }

    //Can the story continue
    public bool CanContinue()
    {
        return story.canContinue;
    }
    
    //Do we have a choice to make here
    public bool HasChoice()
    {
        return story.currentChoices != null && story.currentChoices.Count > 0;
    }

    //Get a variable
    public T GetVar<T>(string varName)
    {
        T variable = default(T);
        foreach (string var in story.variablesState)
        {
            if(varName == var)
                variable = (T)story.variablesState[var];
        }

        return (T)variable;
    }
    
    public void LoadNewStory(TextAsset newtext)
    {
        myText = newtext;
        LoadStory();
    }

    public void MakeChoice(int index)
    {
        if(index <= story.currentChoices.Count-1)
            story.ChooseChoiceIndex(index);
    }
    
    public void ChangeVariable(string varName, string newData)
    {
        foreach (string variable in story.variablesState)
        {
            if (variable == varName)
            {
                story.variablesState[variable] = newData;
            }  
        }
    }
    
    // Creates a button showing the choice text. Taken from default Ink code
    public Button CreateChoiceView (string text) {
        // Creates the button from a prefab
        Button choice = myButton;
        choice.transform.SetParent (myCanvas.transform, false);
		
        // Gets the text from the button prefab
        Text choiceText = choice.GetComponentInChildren<Text> ();
        choiceText.text = text;

        // Make the button expand to fit the text
        HorizontalLayoutGroup layoutGroup = choice.GetComponent <HorizontalLayoutGroup> ();
        layoutGroup.childForceExpandHeight = false;

        return choice;
    }
    
}
