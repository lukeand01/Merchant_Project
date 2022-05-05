using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CrateInventory : ButtonBase
{
    //IT NEEDS TO HOLD A LIST OF ITENS.

    public List<ItemEntity> itemList = new List<ItemEntity>();
    public int id;

    public int spaceLimit;
    int currentLimit;

    Image fillBar;
    Image selected;

    Color selectedColor;

    #region BASE
    public void SetUp(int _id, CrateInventory newCrate)
    {
        id = _id;
        spaceLimit = 3; //LATER I WILL PUT THIS SOMEWHERE ELSE.
        itemList = newCrate.itemList;
        Observer.instance.EventOpenCrate += HandleSelected;

        GetReferences();
        SetCurrentLimit();
        HandleFillBar();

    }

    void GetReferences()
    {

        selected = gameObject.GetComponent<Image>();
        selectedColor = selected.color;
        selected.color = Color.white;
        fillBar = transform.GetChild(1).GetComponent<Image>();
        
    }

   void SetCurrentLimit()
    {
        currentLimit = 0;
        for (int i = 0; i < itemList.Count; i++)
        {
            for (int y = 0; y < itemList[i].quantity; y++)
            {
                currentLimit += itemList[i].currentItem.spaceUse;
            }
        }
    }


    void HandleFillBar()
    {    
        fillBar.fillAmount = (float)currentLimit / spaceLimit;
    }
    #endregion

    #region 

    public bool CrateHasSpace(int newAddition)
    {
        SetCurrentLimit();
        if (currentLimit >= spaceLimit) return false;

        if (currentLimit >= spaceLimit) Debug.Log("yo this should be false.");

        if (currentLimit + newAddition > spaceLimit) return false;

        return true;
    }

    #endregion
    public override void OnPointerClick(PointerEventData eventData)
    {
        //WE SHOW THE ITENS INSIDE AS A NORMAL LIST.

        //I NEED TO HIGHLIGHT THE SELECTED.
        //BUT I NEED TO BE ABLE TO DISABLE THE SELECTED.
         Observer.instance.OnOpenCrate(id, itemList);
         selected.color = selectedColor;
    }

    void HandleSelected(int _id, List<ItemEntity> ignore)
    {
        if(_id != id)
        {
            selected.color = Color.white;
        }
    }

    private void OnDestroy()
    {
        Observer.instance.EventOpenCrate -= HandleSelected;
    }
}

/*
    public int TotalSpace() => currentSpace; //MAYBE I WILL DO DOUBLE CHECK HERE LATER.
  
    public void SetUp(List<ItemEntity> _itemList)
    {
        itemList = _itemList;
        HandleFillBar();
    }

    


    public void AddItem(Item newItem)
    {
        //I NEED TO CHECK IF I NEED TO STACK INSTEAD.
        //THIS WILL ONLY BE CALLED AFTER I ALREADY CHECKED TOTAL SPACE.

        int ItemListPos = GetItemPosition(newItem);
        if (ItemListPos != -1)
        {
            //THEN TAHT MEANS THAT WE HAVE THE ITEM HERE.
            itemList[ItemListPos].quantity += 1;

        }
        else
        {
            CreateItem(newItem);
        }
               
        currentSpace += newItem.spaceUse;
        HandleFillBar();
    }
    void RemoveItem()
    {
        //THIS IS DIFFERENT BECAUSE I NEED TO INFORM THE UI FROM HERE.

    }


    void CreateItem(Item newItem)
    {
        ItemEntity newEntity = new ItemEntity();
        newEntity.currentItem = newItem;
        newEntity.quantity = 1;

        itemList.Add(newEntity);
    }

    int GetItemPosition(Item newItem)
    {
        //
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].currentItem == newItem) return i;
        }


        return -1;
    }
    #endregion

   

    */