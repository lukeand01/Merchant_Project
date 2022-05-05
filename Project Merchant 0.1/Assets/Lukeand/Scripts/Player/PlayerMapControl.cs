using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMapControl : MonoBehaviour
{
    //THIS CONTROLS MAP, CITY, DUNEGON AND ALIKE MOVEMENT.
    //THIS SHOULD BE DISABLED WHEN WE DONT NEED IT.

    [SerializeField] LayerMask interactLayer;
    [SerializeField] Camera playerCamera;
    
    #region MAP PLACES
    GameObject targetPlace;
    GameObject currentTargetPlace;
   [SerializeField] GameObject originalPlace;
    #endregion

    [SerializeField] PathStates pathState;

    //DO I NEED TO CALL SET UP?

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.I)) 
        {
            Observer.instance.OnHandleMenu(0);
            

        }



        if (PersonalUIController.instance.IsMenuOpen()) return;
        HandleMapZoom();
        HandleMouseInput();


        if (pathState == PathStates.Moving) CheckArrival();
    }
    #region PLAYERINPUT
    void HandleMouseInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, interactLayer);

        if (hit.collider == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            GetInfo(hit.collider.GetComponent<MapPlace>());
        }

        if (pathState == PathStates.Moving) return;

        if (Input.GetMouseButtonDown(1))
        {
            //I MOVE TOWARDS THE STUFF
            if (hit.collider == null) return;
            MoveOrder(hit.collider.gameObject);
        }
    }

     
    void HandleMapZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (playerCamera.orthographicSize <= 3) return;
            
            playerCamera.orthographicSize -= 1;

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (playerCamera.orthographicSize >= 10) return;

            playerCamera.orthographicSize += 1;
        }
    }

    void DragMap()
    {
        //WE CAN DRAG THE MAP AND SEE EVERYTHING.
        
    }
    #endregion

    void CheckArrival()
    {
        if ((transform.position - currentTargetPlace.transform.position).magnitude <= 0.01f) Arrived();
    }
    void Arrived()
    {
        pathState = PathStates.Arrived;
        originalPlace = targetPlace;
        //SHOW UI.
        Observer.instance.OnArrivedPlace(originalPlace.GetComponent<MapPlace>());
    }

    void MoveOrder(GameObject target)
    {

        MapPlace placeScript = originalPlace.GetComponent<MapPlace>();

        if (!placeScript.HasPlace(target)) return;

        targetPlace = target;
        currentTargetPlace = target;
        StartMove();
        Observer.instance.OnArrivedPlace(null);
    }

    void StartMove()
    {
        //YOU ACTUALLY START MOVING.
        pathState = PathStates.Moving;
        transform.DOMove(currentTargetPlace.transform.position, 5);
    }

    void GetInfo(MapPlace targetPlace)
    {
        
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            if (originalPlace != null) return;

            originalPlace = GameObject.Find("StartPlace");
            Debug.Log("i got the first original place");
        }
    }



    public void EnterCity(GameObject newGate)
    {
        originalPlace = newGate;



    }
}

public enum PathStates
{
    Idle,
    Moving,
    Camp,
    Menu,
    Arrived
}