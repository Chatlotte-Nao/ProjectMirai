using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
/// <summary>
/// 集合各种鼠标事件处理，方便点击时自动播放对应音效
/// </summary>
public class UIButton : MonoBehaviour,IPointerClickHandler,IPointerUpHandler,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{

    [HideInInspector] public UnityEvent onClick = new();
    [HideInInspector] public UnityEvent onClickDown = new();
    [HideInInspector] public UnityEvent onClickUp = new();
    [HideInInspector] public UnityEvent onPointerEnter = new();
    [HideInInspector] public UnityEvent onPointerExit = new();
    //TODO 后续添加长按事件

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onClickUp?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onClickDown?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerExit?.Invoke();
    }

    private void OnDestroy()
    {
        onClick = null;
        onClickDown = null;
        onClickUp = null;
        onPointerEnter = null;
        onPointerExit = null;
    }
}
