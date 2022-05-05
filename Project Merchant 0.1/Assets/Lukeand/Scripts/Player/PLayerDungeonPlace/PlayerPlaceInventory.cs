using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaceInventory : MonoBehaviour
{
    //THIS IS THE INVENTORY WHEN YOU ARE INSISDE DUNGEON OR OTHER PLACES.
    //THE USE OF RESOURCE WILL BE DONE HERE INSTEAD.


    List<ItemEntity> itemList = new List<ItemEntity>();

    public void SetUpInventoryList(List<ItemEntity> _itemList)
    {
        itemList = _itemList;
    }




}
