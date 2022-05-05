using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaceControl : MonoBehaviour
{
    //THIS CONTROLS THE MOVEMENT.
    //THIS CONTROLS INTERACTIONS WITH THE ROOM AND THE PROCESS.

   [SerializeField] GameObject currentArea;
    

    private void Update()
    {


        PlayerMovement();

    }

    void CorridorMovement()
    {
        //
        if (Input.GetKey(KeyCode.D))
        {


            return;
        }
        if (Input.GetKey(KeyCode.A))
        {

        }



    }
    void PlayerMovement()
    {
        //DETECT IF CLICK AREA.
        //DECIDE WHAT KIND OF AREA IT IS.

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);



        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider == null) return;
            if (hit.collider.GetComponent<Area>() == null) return;
            if (hit.collider.GetComponent<Area>().CanBeClicked(currentArea.name) == false) return;

            if (hit.collider == currentArea) 
            {
                HandleCurrentArea();
                return;
            }

            //OPEN AN OPTION MENU.
            //IN THE OPTION MENU YOU HAVE THE MOVE.
            //WHAT CAN YOU DO?





            MoveRoom(hit.collider.gameObject);


            if (hit.collider.GetComponent<Room>() != null) HandleRoom(hit.collider.gameObject);
            if (hit.collider.GetComponent<Corridor>() != null) HandleCorridor();

        }

    }
    void HandleCurrentArea()
    {
        //THIS IS THE CURRENT AREA THE PLAYER IS.
        //THERE ARE OTHER THINGS YOU CAN DO WHEN YOU ARE IN TEH ROOM.
        //


    }

    void HandleRoom(GameObject target)
    {
        //TELEPORT TO THE MIDDLE OF THE ROOM.
        //HOW DOES MOVING FROM ONE ROOM TO THE NEXT WORK?
        //WHAT OPTIONS DO YOU HAVE IN THE ROOM YOU ARE AND THE NEXT ROOM?

        //SHOW ALL THE OPTIONS THAT MY PART CAN CURRENTLY DO.
        //SCOUT ROOM, 
       


    }
    public void MoveRoom(GameObject target)
    {
        Area areaScript = target.GetComponent<Area>();
        currentArea = target;
        Transform actualTarget = target.transform.GetChild(0).gameObject.transform;
        transform.localPosition = new Vector3(actualTarget.position.x, actualTarget.position.y, -1);

        //WHEN WE MOVE TO A NEW AREA WE CHECK ITS PATHS.
        areaScript.ExploreArea();
        DiscoverNearArea(areaScript);

    }

    void DiscoverNearArea(Area areaScript)
    {
        for (int i = 0; i < areaScript.paths.Length; i++)
        {
            areaScript.paths[i].GetComponent<Area>().DiscoverArea();
        }
    }



    void HandleCorridor()
    {
        //IN CORRIDOR WE START D AND A INPUT FOR FORWARD AND BACK MOVEMENT.
        //YOU START IN THE CLOSEST END OF THE CORRIDOR.
        //YOU GO TO TEH FARTHEST WITH D.


    }

}
public enum PlayerPlaceAllows
{

}
