using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaceHandler : MonoBehaviour
{
    public static PlayerPlaceHandler instance;

    //
    PlayerPlaceInventory inventory;
    PlayerPlaceParty party;
    PlayerPlaceControl control;
    private void Awake()
    {
        instance = this;
        inventory = GetComponent<PlayerPlaceInventory>();
        party = GetComponent<PlayerPlaceParty>();
        control = GetComponent<PlayerPlaceControl>();
    }


    public void SetUp(List<CharacterAlly> partyList, List<ItemEntity> itemList)
    {
        party.SetUpPartyList(partyList);
        inventory.SetUpInventoryList(itemList);

    }

    public void PlacePlayerInArea(GameObject target) => control.MoveRoom(target);
    
        
    
}
