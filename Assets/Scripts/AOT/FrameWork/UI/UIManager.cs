using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<string, int> _uiOrderDic = new Dictionary<string, int>();
    private Dictionary<string, BaseUI> _uiPanelDic = new Dictionary<string, BaseUI>();
    //当前最大Canvas Order In Layer值
    private int _currentOrder;
    //每新生成一个UI界面就递增这个值，每关闭一个界面
    private readonly int _orderIncrement=10;
    public async UniTask OpenUI<T>(string uiPath) where T: BaseUI
    {
        string uiName = typeof(T).ToString();
        if (_uiPanelDic.ContainsKey(uiName))
        {
            BaseUI uiPanel= _uiPanelDic[uiName];
            
        }
        
        GameObject uiPrefab=await ResourceManager.Instance.LoadPrefabAsync(uiPath);
        T topUI= uiPrefab.GetComponent<T>();
        Canvas canvas= uiPrefab.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = (_currentOrder += _orderIncrement);
        _uiOrderDic.Add(uiName, _currentOrder);
        _uiPanelDic.Add(uiName,topUI);

        await topUI.InitializeAsync();
        await topUI.ShowAsync();
    }


    public async UniTask ShowUI(string uiName)
    {
        
    }
    
    public async UniTask HideUI(string uiName)
    {
        
    }

    public BaseUI GetUI(string uiName)
    {
        if (!_uiPanelDic.ContainsKey(uiName))
        {
            Log.Error("key not exist!");
            return null;
        }
        return _uiPanelDic[uiName];
    }
}
