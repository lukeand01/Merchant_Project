using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemInventory : ButtonBase
{
    ItemEntity currentEntity;

    Image itemImage;
    TextMeshProUGUI itemName;
    TextMeshProUGUI itemQuantity;

    public void SetUp(ItemEntity newEntity)
    {
        currentEntity = newEntity;

        GetReferences();

        HandleUI();
    }
    void GetReferences()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        itemQuantity = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }
    void HandleUI()
    {
        itemImage.sprite = currentEntity.currentItem.itemSprite;
        itemName.text = currentEntity.currentItem.itemName.ToString();
        itemQuantity.text = currentEntity.quantity.ToString();

    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        //TRIGGER AN EVENT FOR OBSERVER.
        Observer.instance.OnOpenObserver(currentEntity);

    }
}
