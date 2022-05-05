using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    //IT NORMALLY STARTS UNKNOW AND AS THE PLAYER EXPLORE IT APPEARS.
    //BUT JUST DISCOVERED MEANS THAT YOU DONT KNOW WHAT IT CONTAINS AND THAT YOU HAVE NEVER WALKED THERE.

    [SerializeField] AreaState currentState;
    [SerializeField] public GameObject[] paths;
    SpriteRenderer rend;

    Color invisible;
    Color explored;
    Color discovered;

    private void Awake()
    {
        invisible = new Color(0, 0, 0, 0);
        explored = Color.white;
        discovered = Color.gray;
    }
    private void Start()
    {     
        rend = GetComponent<SpriteRenderer>();
        AtributeInitialState();
    }

    public bool CanBeClicked(string originalArea)
    {
        if (currentState == AreaState.Unknown) return false;

        //THEN WE CHECK IF WE CAN.
        for (int i = 0; i < paths.Length; i++)
        {
            if (paths[i].gameObject.name == originalArea) return true;
        }

        return false;
    }
   
    void AtributeInitialState()
    {
        if(currentState == AreaState.Unknown)
        {
            rend.color = invisible;
        }
        if (currentState == AreaState.Discovered)
        {
            rend.color = discovered;
        }
        if (currentState == AreaState.Explored)
        {
            rend.color = explored;
        }


    }

    public void DiscoverArea()
    {
        if (currentState == AreaState.Explored) return;
        currentState = AreaState.Discovered;
        rend.color = discovered;
    }

    public void ExploreArea()
    {
        currentState = AreaState.Explored;
        rend.color = explored;
    }
}
public enum AreaState
{
    Unknown,
    Discovered,
    Explored
}
