using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlace : MonoBehaviour
{
    //I NEED TO SHOW WHAT PLACES CONNECT TO THIS.
    public string placeName;
    public List<GameObject> placeList = new List<GameObject>();
    public bool isDungeon;

    public bool HasPlace(GameObject target)
    {
        for (int i = 0; i < placeList.Count; i++)
        {
            if (placeList[i] == target) return true;
        }
        return false;
    }


    //PLACES HAVE A NAME.
    //PLACES 


}
