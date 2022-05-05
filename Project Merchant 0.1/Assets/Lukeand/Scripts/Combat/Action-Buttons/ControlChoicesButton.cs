using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ControlChoicesButton : ButtonBase
{
    public UnityEvent choiceEvent;


    public override void OnPointerClick(PointerEventData eventData)
    {
        choiceEvent.Invoke();
    }
}
