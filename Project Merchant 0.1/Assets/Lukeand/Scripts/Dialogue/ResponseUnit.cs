using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Ink.Runtime;

public class ResponseUnit : ButtonBase
{
    GameObject selected;
    TextMeshProUGUI responseText;

    int order;

    private void Awake()
    {
        selected = transform.GetChild(0).gameObject;
        responseText = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void Setup(Choice currentChoice)
    {
        responseText.text = currentChoice.text;        
        order = currentChoice.index;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        DialogueHandler.instance.Respond(order);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        selected.SetActive(true);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        selected.SetActive(false);
    }
}
