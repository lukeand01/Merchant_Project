using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TagHandler : MonoBehaviour
{
    //INSTEAD OF EVERYTHING IN THE DIALOG HANDLER. I WILL HANDLE TAG HERE.
    DialogueHandler dialogueHandler;

    TextMeshProUGUI speakerNameText;
    Image portraitLeft;
    Image portraitRight;
    private void Start()
    {
        GetReferences();
    }
    void GetReferences()
    {
        dialogueHandler = GetComponent<DialogueHandler>();

        speakerNameText = dialogueHandler.speakerNameText;
        portraitLeft = dialogueHandler.portraitLeft;
        portraitRight = dialogueHandler.portraitRight;
        
       
    }

    const string speaker_Tag = "speaker";
    const string layout_Tag = "layout";
    const string portrait_Tag = "portrait";
    const string transition_Tag = "transition";

    string speaker = "";
    Sprite currentSprite;
   public void HandleTags(List<string> tagList)
    {
        
        foreach (string tag in tagList)
        {
            string[] splitTag = tag.Split(':');
            string firstValue = ""; //ALWAYS TAG
            string secondValue = "";
            string thirdValue = "";
            int tagValueInt = 0;


            if (splitTag.Length < 2 || splitTag.Length > 3)
            {
                //SOMETHING WENT WRONG
                Debug.Log("something went wrong");
            }

            if (splitTag.Length == 2)
            {
                firstValue = splitTag[0].Trim();
                secondValue = splitTag[1].Trim();
            }
            if (splitTag.Length == 3)
            {
                firstValue = splitTag[0].Trim();
                secondValue = splitTag[1].Trim();
                thirdValue = splitTag[2].Trim();
                int.TryParse(thirdValue, out tagValueInt);
            }

            #region HANDLE TAG
            if (firstValue == speaker_Tag) SpeakerTag(secondValue);
            if (firstValue == portrait_Tag) PortraitTag(secondValue);
            if (firstValue == layout_Tag) LayoutTag(secondValue);
            if (firstValue == transition_Tag) HandleTransition(secondValue);
            #endregion
        }
    }

    void SpeakerTag(string value)
    {
        speakerNameText.text = value;
        speaker = value;
    }
    void PortraitTag(string tagValue)
    {
        string[] splitTagValue = tagValue.Split('_');
        string nameValue = splitTagValue[0].Trim();
        currentSprite = Resources.Load<Sprite>($"CharacterSprites/{nameValue}/{tagValue}");
        
    }
    void LayoutTag(string tagValue)
    {
        //WE GET CURRENTSPRITE 
        if(tagValue == "left")
        {
            portraitLeft.gameObject.SetActive(true);
            portraitLeft.sprite = currentSprite;

        }
        if(tagValue == "right")
        {
            portraitRight.gameObject.SetActive(true);
            portraitRight.sprite = currentSprite;
        }

        if(tagValue == "null")
        {
            HandlePortraits(false);
        } 

    }

    void HandlePortraits(bool choice)
    {
        portraitLeft.gameObject.SetActive(choice);
        portraitRight.gameObject.SetActive(choice);
    }

    void HandleTransition(string tagValue)
    {
        //TRANSITIONS ONLY GO TO SCENE.
        //I MIGHT WANT TO USE POSITION.
        Debug.Log("transition to go to " + tagValue);
        SceneManager.LoadScene(tagValue);
    }

    void HandleCombat()
    {
        //I NEED TO HAVE A REFERENCE TO THE ENMIES SOMEWHERE.

    }

    void HandleEvents()
    {
        //HOW TO HANDLE BATTLE?
        //HOW TO PASS THE INFORMATION OF THE NUMBER AND TYPE OF ALLYES AND FROM WHERE.


    }
}


//HOW TO MAKE THE COMBAT? I NEED TO GIVE ENEMY INFO. MAYBE I CAN HOLD THAT INFO IN RESOURCE AND REFERENCE IT WITH CODE.


