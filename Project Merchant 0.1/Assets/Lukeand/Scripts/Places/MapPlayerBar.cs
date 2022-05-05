using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MapPlayerBar : MonoBehaviour
{
    GameObject enterButton;
    MapPlace placeScript;

    private void Start()
    {
        GetReferences();
        Observer.instance.EventArrivedPlace += ArrivedPlace;
    }

    void GetReferences()
    {
        enterButton = transform.GetChild(1).gameObject;
    }
    void ArrivedPlace(MapPlace targetPlace)
    {
        //SHOW UI.
        //I SHOULDNT BE ABLE TO MOVE WHEN IM TAKING THAT DECISION.
        if (targetPlace == null)
        {
            enterButton.SetActive(false);
            return;
        }
        enterButton.SetActive(true);
        enterButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Enter " + targetPlace.placeName.ToString();

        placeScript = targetPlace;
    }

    public void Enter()
    {
        if(placeScript == null)
        {
            Debug.LogError("You cannot enter place because something didnt work");
            return;
        }

        //WE TELL THE PLAYER WHERE WE ENTERED SO THAT WHEN WE LEAVE WE LEAVE IN THAT POSITION.


        if (placeScript.isDungeon)
        {
            Observer.instance.OnPreDungeonPrepare(placeScript.placeName, PlayerHandler.instance.GetPartyList(), PlayerHandler.instance.GetUsefulItem());
            return;
        }


        SceneManager.LoadScene(placeScript.placeName.ToString());



    }

    
}
