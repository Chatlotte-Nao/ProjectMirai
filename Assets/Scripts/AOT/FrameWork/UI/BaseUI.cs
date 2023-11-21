using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
/// <summary>
/// UI界面的基类
/// </summary>
public class BaseUI
{
    public UIComponentData UIData;
    public bool IsActive => UIData.gameObject.activeSelf;
    private Canvas _canvas;
    
    
    public void SetUIData(UIComponentData uiComponentData)
    {
        UIData = uiComponentData;
        _canvas=UIData.GetComponent<Canvas>();
    }

    public void SetCanvasOrder(int order)
    {
        _canvas.overrideSorting = true;
        _canvas.sortingOrder = order;
        _canvas.transform.SetAsLastSibling();
    }

    public virtual async UniTask InitializeAsync()
    {
        
    }

    public virtual async UniTask ShowAsync()
    {
        //TODO 后面加上UI打开时播放动画的代码
    }

    public virtual async UniTask HideAsync()
    {
        //TODO 后面加上UI隐藏时播放动画的代码
    }

    public virtual async UniTask Dispose()
    {
        //TODO 后面加上UI隐藏时播放动画的代码
    }
}
