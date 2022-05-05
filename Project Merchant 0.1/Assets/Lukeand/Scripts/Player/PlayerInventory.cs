using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //INVENTORY HAS CRATES THAT STORE THE ITEMS.
    //CRATES HAVE A SPACE AND EACH ITENS HAS SPACEUSE.
    //WEIGHT DOES NOT AFFECT, BUT THE WAGON HAS A LIMIT AND ITS SPEED IS AFFECTED.

    //THIS IS THE THING THAT WILL BE SAVED. CAN I SERIALIZE IT?
    //MAYBE I NEED TO USE RESOURCE AND GET ALL THE ITENS THROUGH INT ID.
    public int initialCrate = 4;
    public List<CrateInventory> crateList = new List<CrateInventory>();//THIS IS NOT SUPPOSED TO BE CREATED. INSTEAD IT IS SUPPOSED TO REFERENCE.

    [SerializeField] Item itemA;
    [SerializeField] Item itemB;
    [SerializeField] Item itemC;


    private void Awake()
    {
        
    }

    private void Start()
    {
        CreateWagon();
        ReceiveItens(GetEntity(itemA));
        ReceiveItens(GetEntity(itemB));
        ReceiveItens(GetEntity(itemB));
        ReceiveItens(GetEntity(itemC));
        ReceiveItens(GetEntity(itemC));

    }

    private void Update()
    {
        Teste();
    }

    void Teste()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ReceiveItens(GetEntity(itemA));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ReceiveItens(GetEntity(itemB));
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
           ReceiveItens(GetEntity(itemC));
        }
    }

    #region CREATE WAGON

    //



    void CreateWagon()
    {
        //WHAT IF I NEED TO UPDATE THE WAGON?
        for (int i = 0; i < initialCrate; i++)
        {
            CreateCrate();
        }
        //IN THE END WE TELL THE THING TO DO ITS STUFF.
        
        //I WILL SEND THE CRATE LIST 
        Observer.instance.OnChangeWagon(crateList);

    }
    
    void CreateCrate()
    {
        CrateInventory newCrate = new CrateInventory();
        crateList.Add(newCrate);
        newCrate.spaceLimit = 3;

    }
    

    #endregion

    #region CHANGE INVENTORY

    void UpdateWagon()
    {
        //WE DO THIS EVERYTIME WE GET A NEW ITEM.
        Observer.instance.OnChangeWagon(crateList);
    }

    void ReceiveItens(ItemEntity newItem)
    {
        //FIRST CHECK IF THERE IS ANYWHERE TO STACK.
        //IF NOT- PUT IT IN THE FIRST CRATE WITH SPACE.

        for (int i = 0; i < newItem.quantity; i++)
        {
            //WE CHECK EACH.
            //FIRST IF IT CAN STACK.

            List<int> rightPos = GetListPos(newItem.currentItem);

            int cratePos = rightPos[0];
            int itemListinCrate = rightPos[1];



            if (rightPos.Count != 2) 
            {
                //THIS MEANS THAT NO CRATE HAS SPACE.
                Debug.Log("there is no space in any crate");

                return;
            }
            if(cratePos != -1)
            {
                //WE CAN STACK.
                StackItem(cratePos, itemListinCrate);
            }
            if(cratePos == -1)            
            {
                //WE CAN ADD.
                AddItem(newItem, itemListinCrate);

            }


        }

        UpdateWagon();
    }


    List<int> GetListPos(Item newItem)
    {
        List<int> intList = new List<int>();
        List<int> spaceIntList = new List<int>();


        for (int i = 0; i < crateList.Count; i++)
        {
            //WE CHECK EVERY CRATE.

            if (!crateList[i].CrateHasSpace(newItem.spaceUse)) continue;

            for (int y = 0; y < crateList[i].itemList.Count; y++)
            {
                if(crateList[i].itemList[y].currentItem == newItem)
                {
                    
                    intList.Add(i); //THIS TELLS IN WHAT CRATE.
                    intList.Add(y); //THIS TELLS IN WHAT POSITIONS INSIDE THE CRATE.
                    return intList;                  
                }
                
            }

            if (spaceIntList.Count == 0) spaceIntList.Add(i);
            
        }
        intList.Add(-1); //THIS TELLS THAT WE FOUND NO SAME ITEM. THEREFORE WE MUST ADD.

        if (spaceIntList.Count == 1) intList.Add(spaceIntList[0]);



        return intList;
        
    }

    void AddItem(ItemEntity entity, int rightCrate)
    {
        //
        crateList[rightCrate].itemList.Add(entity);
    }
   
    void StackItem(int cratePos, int itemListinCrate)
    {
        //
        crateList[cratePos].itemList[itemListinCrate].quantity += 1;

    }
    #endregion

    #region GET ONLY USEFUL ITENS

    public List<ItemEntity> GetUsefulItens()
    {
        //I NEED TO SEARCH EVERY CRATE AND TAKE ONLY THE ITENS THAT CAN BE USED 
        List<ItemEntity> newList = new List<ItemEntity>();

        for (int i = 0; i < crateList.Count; i++)
        {
            for (int y = 0; y < crateList[i].itemList.Count; y++)
            {
                if(crateList[i].itemList[y].currentItem.GetConsumable() != null || crateList[i].itemList[y].currentItem.GetFood() != null)
                {
                    newList.Add(crateList[i].itemList[y]);
                }
            }



        }

        return newList;

    }


    #endregion


    ItemEntity GetEntity(Item newItem)
    {
        ItemEntity entity = new ItemEntity();
        entity.currentItem = newItem;
        entity.quantity = 1;
        return entity;
    }

}
/*
  

   void ReceiveAllItens()
   {
       //HERE WE RECEIVE A LIST OF ITENS TO BE ADDED.
       //FROM HERE WE WILL ORGANIZE EVERYTHUING TILL THERE IS NOWHERE ELSE TO PLACE THEM.
       //IF THERE ARE ADDITIONAL ITENS 


   }

   void AddItem(ItemEntity newEntity)//IT SHOULD BE ENTITY BUT NOW IS JUST TESTING.
   {
       //AND ADDED ITEM GOES TO THE FIRST POSSIBLE CRATE.
       //WE FIRST NEED TO TAKE A LOOK IF THERE IS ANY PLACE TO STACK.
       //BUT THERE NEEDS TO BE SPACE. IT ONLY GETS HERE IF THERE IS SPACE.

       bool added = false;


       //SHOULD THE 


       for (int i = 0; i < newEntity.quantity; i++)
       {
           //WE DO THIS TO EVERYITEM QUANTITY
           added = false;
           for (int y = 0; y < crateList.Count; y++)
           {
               //WE CHECK IF THERE IS SPACE.
               if (crateList[y].TotalSpace() + newEntity.currentItem.spaceUse > crateList[y].spaceLimit) continue;
               //THEN WE CHECK IF WE CAN STACK SOMEWHERE HERE
               //BECAUSE TECHINICALY THEM NEWITEM WILL BE NEWENTITY
               if (crateList[y].itemList.Contains(newEntity))
               {
                   crateList[y].AddItem(newEntity.currentItem);
                   added = true;
                   return;
               }
               Debug.Log("there is space but no stacking");

               //WHAT WE DO HERE IS THAT WE ADD IN THE FIRST CRATE THAT WE CAN.

           }
           Debug.Log("got here");

           //IF WE EVER GET HERE IS BECAUSE WE MUST ADD IN THE FIRST CRATE WE CAN.
           //CANT GET HERE 
           if (added) continue;
           for (int z = 0; z < crateList.Count; z++)
           {
               //THIS IS THE THING AGAIN BUT IN THIS ONE WE ARE LOOKING FOR A SPCAE TO PUT IT NOT TO STACK IT.
               if (crateList[z].TotalSpace() + newEntity.currentItem.spaceUse > crateList[z].spaceLimit) continue;
               crateList[z].AddItem(newEntity.currentItem);
           }



       }




       //IF I GET HERE THEN I WILL ADD TO THE FIRST THAT I CAN CRATE.


   }

  

  
   void AddCrate()
   {
       //ADD A BLANK TEMPLATE IMAGE
   }

   void DestroyCrate()
   {
       //WHAT HAPPENS WHEN YOU LOSE A CRATE?
   }
   */