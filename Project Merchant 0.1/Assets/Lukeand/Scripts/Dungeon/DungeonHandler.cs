using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonHandler : MonoBehaviour
{
    //MAYBE WE HOLD ALL DUNGEON POSSIBLE ENEMIES HERE.
    //WHAT DO WE DO WHEN ITS A BOSS?
    //



    [SerializeField] GameObject initialRoom;
    [SerializeField] GameObject savedRoom; //BUT THIS IS FOR LATER.

    private void Start()
    {
        PlacePlayer();
    }


    public void PlacePlayer()
    {
        //IF I SAVE I WILL PUT THE SAVE HERE.
        if(savedRoom != null)
        {

        }


        PlayerPlaceHandler.instance.PlacePlayerInArea(initialRoom);
    }
    

}
