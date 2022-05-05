using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityHandler : MonoBehaviour
{
    //THERE ARE THINGS 

    

    public int stability;
    public int crime;

    [SerializeField] GameObject gate;


    public void EnterCity()
    {
        //MOST OF THE TIMES YOU SHOULD ENTER BY THE ENTRANCE.
        //WE SHOULD SET THE ORIGINAL PLACE.
        //SHOULD I CREATE A NEW ONE OR SIMPLY PASS THE DATA?
        //I THINK WE SHOULD SIMPLY COPY THE WHOLE GAMEOBJECT AND PASS IT.
        PlayerHandler.instance.EnterCity(gate);

    }


}

public enum ReputationTypes
{
    Neutral,
    Loved,
    Hated,
    Feared
}