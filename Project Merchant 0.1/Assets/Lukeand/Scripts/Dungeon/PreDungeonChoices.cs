using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PreDungeonChoices : MonoBehaviour
{
    [Header("HOLDERS")]
    [SerializeField] GameObject alliesHolder;
    [SerializeField] GameObject inventoryItensHolder;
    [SerializeField] GameObject chosenItensHolder;



    #region LISTS

    List<CharacterAlly> chosenAllies = new List<CharacterAlly>();


    List<ItemEntity> inventoryItens = new List<ItemEntity>();
    List<ItemEntity> chosenItens = new List<ItemEntity>();

    #endregion

    #region TEXTS
    [SerializeField] TextMeshProUGUI quantityAlliesText;
    [SerializeField] TextMeshProUGUI quantityItensText;
    #endregion





    #region TEMPLATE
    [Header("TEMPLATE")]
    [SerializeField] GameObject allyTemplate;
    [SerializeField] GameObject itemTemplate;
    [SerializeField] GameObject dungeonPlayerTemplate;
    #endregion

    public string dungeonName;
    private void Start()
    {
        AssignEvents();
        
    }

    private void Update()
    {
       
    }

    void AssignEvents()
    {
        //NEED TO CREATE AN EVENT TO START THE PREPARE INSTEAD.

        Observer.instance.EventPreDungeonPrepare += Prepare;

        Observer.instance.EventPreChangeAlly += GetAlly;
        Observer.instance.EventPreChangeItem += GetItem;
    }






    public void Prepare(string _dungeonName, List<CharacterAlly> currentAllies, List<ItemEntity> _possibleItens)
    {
        Debug.Log("got here " + currentAllies.Count + " " + _possibleItens.Count);

        transform.GetChild(0).gameObject.SetActive(true);

        dungeonName = _dungeonName;

        for (int i = 0; i < currentAllies.Count; i++)
        {
            GameObject newObject = Instantiate(allyTemplate, alliesHolder.transform.position, Quaternion.identity);
            newObject.transform.parent = alliesHolder.transform;
            newObject.GetComponent<PreDungeonAlly>().SetUp(i, currentAllies[i]);
        }

        inventoryItens = _possibleItens;


        for (int i = 0; i < _possibleItens.Count; i++)
        {
            GameObject newObject = Instantiate(itemTemplate, inventoryItensHolder.transform.position, Quaternion.identity);
            newObject.transform.parent = inventoryItensHolder.transform;
            newObject.GetComponent<PreDungeonItem>().SetUp(true, _possibleItens[i]);
        }



    }



    void GetAlly(int id, bool choice, CharacterAlly newAlly)
    {

        if (choice)
        {

            if (chosenAllies.Count >= 3)
            {
                //IF THERE IS MORE THAN 4. THEN I WILL SEND THE INFO BACK TO NOT DO THIS.
                Observer.instance.OnPreHandleAllySlot(id);
                return;
            }

            chosenAllies.Add(newAlly);
            quantityAlliesText.text = $"{chosenAllies.Count} / 3";

            //UPDATE UI.
            //IF THERE IS 

            return;
        }
        //THIS IS SO WE TAKE THAT ALLY OUT OF THE CHOSEN LIST.




        for (int i = 0; i < chosenAllies.Count; i++)
        {
            if (chosenAllies[i].charBase.charName == newAlly.charBase.charName)
            {
                chosenAllies.RemoveAt(i);
                quantityAlliesText.text = $"{chosenAllies.Count} / 3";
                return;
            }
        }
    }


    void GetItem(bool inventorySide, ItemEntity newItem)
    {
        //I NEED FIRST TO DEFINE THE ITENS THAT BE USABLE.

        //THIS THING WILL TELL FROM WHERE THE BUTTON IS.

        if (!inventorySide) InventoryReceive(newItem);

        if (inventorySide) DungeonReceive(newItem);


        //UPDATE THE UI
        UpdateChosenUi();
    }

    void InventoryReceive(ItemEntity newItem)
    {
        //IF ITS A CHOSE ITEM WE GET HERE.
        RemakeList(chosenItens, chosenItensHolder);
        RemakeHolder(true, newItem ,inventoryItens, inventoryItensHolder);

    }
    void DungeonReceive(ItemEntity newItem)
    {
        //IF ITS AN INVENTORY ITEM WE GET HERE.
        RemakeList(inventoryItens, inventoryItensHolder);
        RemakeHolder(false, newItem, chosenItens, chosenItensHolder);

    }

    void RemakeHolder(bool side, ItemEntity newItem, List<ItemEntity> itemList, GameObject holder)
    {
        //THIS IS THE ONE THAT RECEIVES.
        //WE CLEAR THE HOLDER.
        //WE ADD THE ITEM THEN CREATES EVERYTHING ELSE.
        //THE ONE RECEIVING REFERENCES THE LIST NOT THE HOLDER.


        ClearAll(holder);
        //ADD THE ITEM. CHECK IF STACK OR CREATE NEW ONE.
        HandleNewItem(newItem, itemList);
        for (int i = 0; i < itemList.Count; i++)
        {
            GameObject newObject = Instantiate(itemTemplate, holder.transform.position, Quaternion.identity);
            newObject.transform.parent = holder.transform;
            newObject.GetComponent<PreDungeonItem>().SetUp(side, itemList[i]);
        }

    }
    void RemakeList(List<ItemEntity> itemList, GameObject holder)
    {
        //WE DO THIS EVERYTIME TO ALLIGN BOTH.
        //WE ONLY DO THIS TO THE ONE GIVING.
        
        itemList.Clear();
        for (int i = 0; i < holder.transform.childCount; i++)
        {
            ItemEntity newEntity = holder.transform.GetChild(i).GetComponent<PreDungeonItem>().currentEntity;
            if (newEntity.quantity <= 0) continue;
            itemList.Add(newEntity);
        }


    }


    void HandleNewItem(ItemEntity newEntity, List<ItemEntity> newList)
    {
        //THEY ARE ALL NORMALLY STACKED IN THE CRATES.
        //I TAKE A REFENCE LIST TO BRING IT HERE.
        //IF I USE ANY ITEM I NEED TO TELL TEH CRATES THAT I DID USE AN ITEM.
        //BUT I CANT MESS THE ORGANIZATION SO I CANT SIMPLY REDO THEM.
        //I WILL CALL FOR AN ORDER OF REMOVAL TO EACH ITEM THAT WAS SPENT.
        //SO I NEED TO HOLD THE LIST OF SPENT ITENS FOR THE END OF THE DUNGEON.
        


        for (int i = 0; i < newList.Count; i++)
        {
            if(newList[i].currentItem == newEntity.currentItem)
            {
                //WE CHECK IF WE CAN STACK. 
                //THERE IS NO STACK LIMIT SO I SIMPLY ADD TO IT.
                newList[i].quantity += newEntity.quantity;
                return;
            }          
        }
        //IF NOT THEN WE JUST ADD IT. 
        newList.Add(newEntity);
    }
    
    void UpdateChosenUi()
    {
        //
        quantityItensText.text = $"{GetChosenTotalWeight()} / 5";

    }
    void ClearAll(GameObject target)
    {
        for (int i = 0; i < target.transform.childCount; i++)
        {
            Destroy(target.transform.GetChild(i).gameObject);
        }

    }

    int GetChosenTotalWeight()
    {
        int newValue = 0;

        for (int i = 0; i < chosenItens.Count; i++)
        {
            for (int y = 0; y < chosenItens[i].quantity; y++)
            {
                newValue += chosenItens[i].currentItem.weight;

            }


        }

        return newValue;
    }

    public void StartDungeon()
    {
        //WE CREATE A DUNGEON PLAYER AND SEND THE FELLA INTO THE DUNGEON.
        //WE CNA ONLY START IF WE HAVE AT LEAST ONE ALLY.
        //WE DONT NEED TO TAKE ITEMS
        if (chosenAllies.Count == 0) return;
        if (GetChosenTotalWeight() > 5) return; //I NEED TO WARN THAT I HAVE TOO MUCH.


        transform.GetChild(0).gameObject.SetActive(false);

        GameObject dungeonPlayer = Instantiate(dungeonPlayerTemplate, new Vector3(0, 0, 0), Quaternion.identity);
        dungeonPlayer.GetComponent<PlayerPlaceHandler>().SetUp(chosenAllies, chosenItens);
        //WHEN I GET INTO THE PLAYER NEEDS TO BE PUT INTO THE RIGHT ROOM.
        SceneManager.LoadScene(dungeonName);



    }

}
