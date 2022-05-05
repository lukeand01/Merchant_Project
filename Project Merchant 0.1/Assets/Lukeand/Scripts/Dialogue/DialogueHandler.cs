using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    #region COMPONENTS
    public static DialogueHandler instance;
    TagHandler tagHandler;
    Story currentStory;

    [SerializeField] TextAsset TESTETextAsset;

    #endregion

    #region UI
    GameObject spriteHolder;
   [HideInInspector] public Image portraitRight;
    [HideInInspector] public Image portraitLeft;

    GameObject dialogueBase;

    GameObject conversationPanel;
    [HideInInspector] public TextMeshProUGUI speakerNameText;
    TextMeshProUGUI dialogueText;
    GameObject nextObject;

    GameObject responsePanel;
    GameObject responseHolder;
    GameObject responseSlider;
    #endregion

    #region TEMPLATE
    [Header("Template")]
    [SerializeField] GameObject responseTemplate;

    #endregion


    #region WRITTING STATS
    [SerializeField] float normalWrittingSpeed;
    [SerializeField] float fastWrittingSpeed;
    float currentWrittingSpeed;
    bool writting;


    #endregion

    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else instance = this;

        GetReferences();
    }
    private void Start()
    {
       // SetUpDialogue();
    }
    void GetReferences()
    {
        tagHandler = GetComponent<TagHandler>();
        
        spriteHolder = transform.GetChild(1).gameObject;

        portraitRight = spriteHolder.transform.GetChild(0).gameObject.GetComponent<Image>();
        portraitLeft = spriteHolder.transform.GetChild(1).gameObject.GetComponent<Image>();

        dialogueBase = transform.GetChild(2).gameObject;
   
        conversationPanel = dialogueBase.transform.GetChild(0).gameObject;


        speakerNameText = conversationPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        dialogueText = conversationPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        nextObject = conversationPanel.transform.GetChild(2).gameObject;

        responsePanel = dialogueBase.transform.GetChild(1).gameObject;
        responseHolder = responsePanel.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        responseSlider = responsePanel.transform.GetChild(1).gameObject;
    }

    //ALSO WHEN WE SET UP WE HAVE TO GIVE PLAYER INFO TO THE GLOBALS.
    #region SET UP
   public void SetUpDialogue(TextAsset newStory)
    {
        currentStory = new Story(newStory.text);

        AssignInkVariables();
        StartCoroutine(RaiseCurtain());
    }

    void AssignInkVariables()
    {
        //PLAYER NAME, PLAYER REPUTATION, PLAYER SKILLS, PLAYER MONEY, PLAYER RELATION
        currentStory.variablesState["PlayerName"] = PlayerHandler.instance.playerName;


        #region VARIABLE STATS
        currentStory.variablesState["PlayerSpeech"] = PlayerHandler.instance.playerStatList[PlayerStats.Speech];
        currentStory.variablesState["PlayerBarter"] = PlayerHandler.instance.playerStatList[PlayerStats.Barter];
        currentStory.variablesState["PlayerLogistic"] = PlayerHandler.instance.playerStatList[PlayerStats.Logistic];
        currentStory.variablesState["PlayerMedicine"] = PlayerHandler.instance.playerStatList[PlayerStats.Medicine];
        
        currentStory.variablesState["PlayerWisdom"] = PlayerHandler.instance.playerStatList[PlayerStats.Wisdom]; ;
        currentStory.variablesState["PlayerLuck"] = PlayerHandler.instance.playerStatList[PlayerStats.Luck];
        #endregion

        #region VARIABLE REPUTATION


        #endregion


        #region VARIABLE BOOLEANS


        #endregion
    }

    IEnumerator RaiseCurtain()
    {
        yield return new WaitForSeconds(0.5f);
        //HandlePortraits(false);
        conversationPanel.SetActive(true);
        ContinueDialogue();
    }

    #endregion

    private void Update()
    {

        if (writting)
        {
            //THEN WE CAN ACCELRATE IT.
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentWrittingSpeed = fastWrittingSpeed;
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                currentWrittingSpeed= normalWrittingSpeed;
            }
        }

        
    }

    

    #region BASE DIALOGUE 
    void ContinueDialogue()
    {
        //WE GET THE NEXT PART OF THE DIALOGUE.
        string lastDialogue = "";
        if (currentStory.canContinue)
        {
            
            lastDialogue = currentStory.Continue();
            StartCoroutine(WrittingProcess(lastDialogue));
            tagHandler.HandleTags(currentStory.currentTags);
        }
        else
        {
            dialogueText.text = lastDialogue;
            
            if (currentStory.currentChoices.Count == 0)
            {
                EndConversation();
                return;
            }
            CreateResponseChoices();
        }
    }
    IEnumerator WrittingProcess(string newText)
    {
        //THIS IS ALWAYS CALLED TO DISPLAY THE TEXT. WE ALSO NEED TO SET IMAGE AND NAME HERE.
        writting = true;
        dialogueText.text = "";
        currentWrittingSpeed = normalWrittingSpeed;

        Animator nextAnimator = nextObject.GetComponent<Animator>();

        foreach (char c in newText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(currentWrittingSpeed);
        }
        writting = false;

        nextAnimator.SetBool("Shinning", true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        nextAnimator.SetBool("Shinning", false);

        ContinueDialogue();

    }

    void EndConversation()
    {
        conversationPanel.SetActive(false);
    } 

    #endregion

    #region HANDLE RESPONSES

    void CreateResponseChoices()
    {
        ClearAll();
        List<Choice> choiceList = currentStory.currentChoices;

        RectTransform responseRect = responseHolder.GetComponent<RectTransform>();

        for (int i = 0; i < choiceList.Count; i++)
        {

            responseRect.sizeDelta = new Vector2(responseRect.sizeDelta.x, responseRect.sizeDelta.y + 55);

            GameObject newObject = Instantiate(responseTemplate, responseHolder.transform.position, Quaternion.identity);
            newObject.transform.parent = responseHolder.transform;
            RectTransform objectRect = newObject.GetComponent<RectTransform>();
            objectRect.sizeDelta = new Vector2(485, objectRect.sizeDelta.y);

            ResponseUnit responseScript = newObject.GetComponent<ResponseUnit>();
            responseScript.Setup(choiceList[i]);
        }

        //I HAVE TO DECIDED IF THERE IS A SCROLL.
        //HAVE TO ADJUST SO THEY ARE CLOSE TO EACH OTHER.

        

        responsePanel.SetActive(true);
        conversationPanel.SetActive(false);

    }
    void ClearAll()
    {
        for (int i = 0; i < responseHolder.transform.childCount; i++)
        {
            Destroy(responseHolder.transform.GetChild(i).gameObject);
        }
    }

    public void Respond(int choice)
    {
        responsePanel.SetActive(false);
        conversationPanel.SetActive(true);
        currentStory.ChooseChoiceIndex(choice);
        ContinueDialogue();
    }
    #endregion

   
}
