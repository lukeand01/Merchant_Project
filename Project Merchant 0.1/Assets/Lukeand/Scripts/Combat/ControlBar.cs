using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControlBar : MonoBehaviour
{
    [SerializeField] CombatHandler handler;
    [Header("THINGS")]
    float currentWrittingSpeed;
    [SerializeField] float normalWrittingSpeed;
    [SerializeField] float fastWrittingSpeed;



    [SerializeField] GameObject actionBar;
    [SerializeField] GameObject narrationBar;

    bool writting;
    bool waitInput;

    private void Start()
    {
        Observer.instance.EventSkillDescriber += Describer;
    }


    private void Update()
    {
        if (!writting) return;


        if (waitInput)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                waitInput = false;
                //MAYBE I SHOULD CALL IT HERE?

            }
        }
        else
        {
            if (Input.GetKey(KeyCode.E))
            {
                currentWrittingSpeed = fastWrittingSpeed;
            }
            else
            {
                currentWrittingSpeed = normalWrittingSpeed;
            }
        }

       

        


    }






    #region ACTION BAR
    [Header("PANEL")]
    [SerializeField] GameObject skillsPanel;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject descriptionPanel;
    [SerializeField] GameObject charDescriptionPanel;

    [Header("HOLDER")]
    [SerializeField] GameObject championHolder;

    [Header("TEMPLATE")]
    [SerializeField] GameObject skillTemplate;
    public void AllyTurn(AllyCombatSlot newAlly)
    {
        HandleActionBar(true);
        OpenSkills();
        PlaceSkills(newAlly);
        PlacePortrait(newAlly.character);
    }

    void PlacePortrait(CharacterAlly newAlly)
    {
        Image championPortrait = championHolder.transform.GetChild(1).GetComponent<Image>();
        championPortrait.sprite = newAlly.charBase.charSprite;
    }
    void PlaceSkills(AllyCombatSlot newAlly)
    {
        ClearAll(skillsPanel);

        List<SkillBase> skillList = newAlly.character.skills;


        for (int i = 0; i < skillList.Count; i++)
        {
            GameObject newObject = Instantiate(skillTemplate, skillsPanel.transform.position, Quaternion.identity);
            newObject.transform.parent = skillsPanel.transform;
            newObject.GetComponent<SkillActionButton>().SetUp(newAlly, skillList[i]);
        }


    }
    
    void PlaceCharDescription()
    {
        //IN THIS WE SIMPLY UPDATE THE STATS.
        //CLICKING ENEMY SHOULD ALSO SHOW THIS.


    }


    public void OpenSkills()
    {
        skillsPanel.SetActive(true);
        inventoryPanel.SetActive(false);
        narrationBar.SetActive(false);
    }
    public void OpenInventory()
    {
        skillsPanel.SetActive(false);
        inventoryPanel.SetActive(true);
        HandleInventory();
    }
    public void OpenCharDescription()
    {
        skillsPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        descriptionPanel.SetActive(true);
    }

    void Describer(string newText)
    {
        if(newText == "")
        {
            descriptionPanel.SetActive(false);
            return;
        }
        descriptionPanel.SetActive(true);
        TextMeshProUGUI descriptionText = descriptionPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        descriptionText.text = newText;

    }

    public void HandleActionBar(bool choice)
    {
        actionBar.SetActive(choice);
    }
    #endregion

    #region NARRATION BAR
    [Header("NARRATION")]
   [SerializeField] Animator tickImage;
   [SerializeField] TextMeshProUGUI narrationText;


    public void StartNarration(List<string> narrationList)
    {
        //BEFORE THE NARRATION WE CARE ABOUT ANIMATION AND CAMERA SHAKE.

        StartCoroutine(NarrationProcess(narrationList));

    }
    IEnumerator NarrationProcess(List<string> narration)
    {
        int current = 0;


       while(current < narration.Count)
        {
            writting = true;
            narrationBar.SetActive(true);

            narrationText.text = "";

            foreach (char c in narration[current])
            {
                //WHILE 
                narrationText.text += c;
                yield return new WaitForSeconds(currentWrittingSpeed);
            }
            //WE TRIGGER ANIMATIOIN
            //yield return new WaitForSeconds(0.5f);
            tickImage.SetBool("Shinning", true);
            waitInput = true;
            yield return new WaitUntil(() => !waitInput);
            tickImage.SetBool("Shinning", false);
            current += 1;

            
        }
       

        writting = false;
        narrationBar.SetActive(false);
        handler.Transition();
    }





    #endregion

    #region INVENTORY
    void HandleInventory()
    {

    }



    #endregion





    #region HELPER FUNCTIONS

    void ClearAll(GameObject holder)
    {
        for (int i = 0; i < holder.transform.childCount; i++)
        {
            Destroy(holder.transform.GetChild(i).gameObject);
        }
    }

    

    #endregion



    
}
