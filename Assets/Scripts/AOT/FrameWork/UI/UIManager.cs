using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private GameObject _uiCanvas;
    private Dictionary<string, UIInfo> _uiPanelDic = new Dictionary<string, UIInfo>();
    //当前最大Canvas Order In Layer值
    private int _currentOrder;
    //每新生成一个UI界面就递增这个值，每关闭一个界面
    private readonly int _orderIncrement=10;
    /// <summary>
    /// 在第一次使用UIManager，和切换场景后调用一下
    /// </summary>
    public void InitUIManager()
    {
        _uiCanvas = GameObject.Find("Canvas");
    }
    
    public async UniTask OpenUI<T>(string uiPath) where T : BaseUI
    {
        string uiName = typeof(T).ToString();
        if (_uiPanelDic.ContainsKey(uiName))
        {
            UIInfo topUIInfo = _uiPanelDic[uiName];
            if (!topUIInfo.UIPanel.IsActive)
            {
                topUIInfo.UIPanel.SetCanvasOrder(_currentOrder += _orderIncrement);
                await topUIInfo.UIPanel.InitializeAsync();
                await topUIInfo.UIPanel.ShowAsync();
            }
        }
        else
        {
            BaseUI baseUI = Activator.CreateInstance<T>();
            GameObject uiPrefab = await ResourceManager.Instance.LoadPrefabAsync(uiPath);
            uiPrefab.transform.SetParent(_uiCanvas.transform);
            UIComponentData uiData = uiPrefab.GetComponent<UIComponentData>();
            Canvas canvas = uiPrefab.AddComponent<Canvas>();
            //TODO 暂不考虑特效，后期会写个脚本挂特效上，填上特效的层级

            canvas.overrideSorting = true;
            canvas.sortingOrder = (_currentOrder += _orderIncrement);
            UIInfo uiInfo = new UIInfo(_currentOrder, baseUI, uiPath);
            _uiPanelDic.Add(uiName, uiInfo);
            baseUI.SetUIData(uiData);
            await baseUI.InitializeAsync();
            await baseUI.ShowAsync(); 
        }
    }

    public async UniTask HideUI(string uiName)
    {
        if (_uiPanelDic.ContainsKey(uiName))
        {
            await _uiPanelDic[uiName].UIPanel.HideAsync();
        }
        else
        {
            Log.Warning("key not exist!");
        }
    }

    public async UniTask DisposeUI(string uiName)
    {
        if (_uiPanelDic.ContainsKey(uiName))
        {
            await _uiPanelDic[uiName].UIPanel.Dispose();
        }
        else
        {
            Log.Warning("key not exist!");
        }
    }

    public BaseUI GetUI(string uiName)
    {
        if (!_uiPanelDic.ContainsKey(uiName))
        {
            Log.Error("key not exist!");
            return null;
        }
        return _uiPanelDic[uiName].UIPanel;
    }
}

public class UIInfo
{
    public int OrderLayer;
    public BaseUI UIPanel;
    public string UIPrefabPath;
    public UIInfo(int orderLayer,BaseUI uiPanel,string uiPrefabPath)
    {
        OrderLayer = orderLayer;
        UIPanel = uiPanel;
        UIPrefabPath = uiPrefabPath;
    }
}
