using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ButtonBase : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{

   Image baseImage;
   [SerializeField] AudioClip baseSound;

    private void Awake()
    {
        baseImage = GetComponent<Image>();
    }


    public virtual void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
