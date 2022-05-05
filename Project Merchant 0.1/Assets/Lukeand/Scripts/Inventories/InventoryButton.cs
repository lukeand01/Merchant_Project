using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryButton : ButtonBase
{
    [SerializeField] string buttonName;
    [SerializeField] PlayerUIButtonType type;


    TextMeshProUGUI nameText;

    Color originalColor;
   [SerializeField] Color selectedColor;

    Image frontImage;

    bool canBeSelected;
    private void Awake()
    {
        nameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        nameText.text = buttonName;
        frontImage = transform.GetChild(0).GetComponent<Image>();
        originalColor = frontImage.color;
        canBeSelected = true;
           
        if(type == PlayerUIButtonType.Upgrade)
        {

        }
    }
    private void Start()
    {
        if (type == PlayerUIButtonType.Upgrade)
        {
            //THEN WE DISABLE IT, BUT MMORE THAN THAT WE MAKE THE LETTER DARK.
            nameText.color = Color.black;
            canBeSelected = false;
        }
        if(type == PlayerUIButtonType.Player)
        {
            frontImage.color = selectedColor;
        }

        Observer.instance.EventChangePlayerUI += HandleOtherButton;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        //THE ONLY THAT CANT BE ACCESS IS THE UPGRADE ONE.
        if (!canBeSelected)
        {
            Debug.Log("cannot select it");
            return;
        }
        Observer.instance.OnChangePlayerUI(type);
        frontImage.color = selectedColor;
    }
    void HandleOtherButton(PlayerUIButtonType _type)
    {
        if (_type == type) return;

        frontImage.color = originalColor;
        
    }
}

public enum PlayerUIButtonType
{
    Player, 
    Inventory,
    Party,
    Quest,
    Monstorium,
    Upgrade
}