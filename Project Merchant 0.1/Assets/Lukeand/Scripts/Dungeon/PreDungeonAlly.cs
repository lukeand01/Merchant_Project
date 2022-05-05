using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PreDungeonAlly : ButtonBase
{

    CharacterAlly currentAlly;
    bool selected;
    int id;

    #region COMPONENTS
    GameObject slotHolder;
    GameObject selectedGo;
    Image championImage;
    GameObject health;
    GameObject xp;
    GameObject resouce;
    GameObject moral;
    TextMeshProUGUI championName;
    TextMeshProUGUI championLevel;

    #endregion
    private void Awake()
    {
        slotHolder = transform.GetChild(0).gameObject;
        selectedGo = slotHolder.transform.GetChild(0).gameObject;
        championImage = slotHolder.transform.GetChild(1).GetComponent<Image>();
        championName = championImage.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        championLevel = championImage.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        health = slotHolder.transform.GetChild(2).gameObject;
        resouce = slotHolder.transform.GetChild(3).gameObject;
        
    }

    private void Start()
    {
        Observer.instance.EventPreHandleAllySlot += HandleSelection;
    }

    public void SetUp(int newId, CharacterAlly newAlly)
    {
        currentAlly = newAlly;
        id = newId;
        //UPDATE THE UI.
        SetUpUI();
    }

    #region UI
    void SetUpUI()
    {
        championImage.sprite = currentAlly.charBase.charSprite;
        championName.text = currentAlly.charBase.charName;
        championLevel.text = currentAlly.currentLevel.ToString();


        SetUpHealth();
        SetUpResource();
        
    }

    void SetUpHealth()
    {
        Image HealthBar = health.transform.GetChild(0).GetComponent<Image>();
        HealthBar.fillAmount = (float)currentAlly.currentHealth / currentAlly.maxHealth;
        TextMeshProUGUI healthText = HealthBar.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        healthText.text = $"{currentAlly.currentHealth} / {currentAlly.charBase.initialHealth}";
    }

    void SetUpResource()
    {
        Image resourceBar = resouce.transform.GetChild(0).GetComponent<Image>();

        //GREEN FOR STAMINE. BLUE FOR MANA. YELLOW FOR ENERGY.
        if (currentAlly.charBase.resourceType == ResourceType.Stamina)
        {
            resourceBar.color = Color.green;
        }
        if (currentAlly.charBase.resourceType == ResourceType.Mana)
        {
            resourceBar.color = Color.blue;
        }
        if (currentAlly.charBase.resourceType == ResourceType.Energy)
        {
            resourceBar.color = Color.yellow;
        }


        resourceBar.fillAmount = (float)currentAlly.currentResource / currentAlly.maxResource;
        TextMeshProUGUI resourceText = resourceBar.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        resourceText.text = $"{currentAlly.currentResource} / {currentAlly.charBase.initialMaxResource}";
    }

    #endregion


    public override void OnPointerClick(PointerEventData eventData)
    {
        //TRIGGER EVENT. MAKE IT SHINNE OR SOMETHING.
        //



        HandleLocalSelection();
        
    }
    void HandleSelection(int targetID)
    {
        //THIS IS FOR OUTSIDE SOURCES.
        if (targetID != id) return;

        if (!selected)
        {
            Select();
            return;
        }
        UnSelect();
    }

    void HandleLocalSelection()
    {
        //THIS IS FOR THE CLICKING
        if (!selected)
        {
            Select();
            Observer.instance.OnPreChangeAlly(id, true, currentAlly);
            return;
        }
        UnSelect();
        Observer.instance.OnPreChangeAlly(id, false, currentAlly);
    }

    void Select()
    {

        selected = true;
        selectedGo.SetActive(true);
       
    }
    void UnSelect()
    {

        selected = false;
        selectedGo.SetActive(false);
       
    }
}
