using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item / Item")]
public class Item : ScriptableObject
{
    //FOR NOW THIS WILL BE THE ONLY CLASS.
    //HOW DO I DECIDED IF THIS ITEM CAN BE USED IN DUNGEON, MAP, NOT USEABLE AT ALL.
    //CONSUMABLES CAN BE USED.
    //WEAPONS CANNOT.
    //FOOD CAN.
    //


    public string itemName;
    public Sprite itemSprite;
    public int spaceUse;
    public int weight;
    



    public virtual Equipable GetWeapon()
    {
        return null;
    }
    public virtual Consumable GetConsumable()
    {
        return null;
    }
    public virtual Food GetFood()
    {
        return null;
    }

}

public enum ItemType
{
    Consumable,
    Weapon,
    Food,
    Goods
}


[System.Serializable]
public class ItemEntity
{
    public Item currentItem;
    public int quantity;



}