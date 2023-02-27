using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    public static MobileInput instance;
    public Vector2 Direction;
    private Vector2 _touchPosition;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var delta = eventData.position - _touchPosition;
        Direction = delta.normalized;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _touchPosition= eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Direction = Vector2.zero;
    }
}
