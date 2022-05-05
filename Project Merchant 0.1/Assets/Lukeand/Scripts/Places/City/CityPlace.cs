using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CityPlace : ButtonBase
{
    //THIS IS THE ACTUAL AREA. YOU CAN CLICK AND GO.
    [SerializeField] GameObject respectiveUI;
    [SerializeField] bool initiallyVisible;
    GameObject basePortrait;

    private void Start()
    {       
        basePortrait = transform.GetChild(0).gameObject;
        basePortrait.SetActive(initiallyVisible);
    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        respectiveUI.SetActive(true);

        //

    }

    
}
