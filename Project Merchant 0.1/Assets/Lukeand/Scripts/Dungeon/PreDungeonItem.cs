using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PreDungeonItem : ButtonBase
{

    public ItemEntity currentEntity;
    bool inventorySide;

    GameObject background;
    Image itemImage;
    TextMeshProUGUI itemNameText;
    TextMeshProUGUI itemQuantityText;


    public void SetUp(bool place, ItemEntity newEntity)
    {
        if(background == null)
        {
            background = transform.GetChild(0).gameObject;
            itemImage = background.transform.GetChild(0).GetComponent<Image>();
            itemNameText = background.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            itemQuantityText = background.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        }
       
        inventorySide = place;
        currentEntity.currentItem = newEntity.currentItem;
        currentEntity.quantity = newEntity.quantity;        

        UpdateUi();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        //I NEED TO DETECT WHERE THIS ITEM SITS.

        if (Input.GetKey(KeyCode.LeftControl))
        {
            HandleUse(5);
            return;
        }

        HandleUse(1);

      
    }

    void HandleUse(int quantity)
    {
        //NEED TO KNOW IF CAN RECEIVE THOUGH.
        //WE CAN UPDATE IT THIS EVERYTIME

        int rightQuantity = quantity;
        rightQuantity = Mathf.Clamp(rightQuantity, 0, currentEntity.quantity);

        ItemEntity newEntity = GetEntity(currentEntity.currentItem, rightQuantity);
        currentEntity.quantity -= rightQuantity;
        Observer.instance.OnPreChangeItem(inventorySide, newEntity);
        //EVERYTIME WE DO THIS WE UPDATE.

        //THEN WE DESTROY IT OR UPDATE UI.
        if (currentEntity.quantity <= 0) Destroy(this.gameObject);

        UpdateUi();

    }
    
    void UpdateUi()
    {
        //UPDATE IMAGE
        //UPDATE NUMBER.
         itemImage.sprite = currentEntity.currentItem.itemSprite;
               
        itemNameText.text = currentEntity.currentItem.itemName;
        itemQuantityText.text = currentEntity.quantity.ToString();

    }
    ItemEntity GetEntity(Item newItem, int quantity)
    {
        ItemEntity newEntity = new ItemEntity();

        newEntity.currentItem = newItem;
        newEntity.quantity = quantity;

        return newEntity;
    }
}
