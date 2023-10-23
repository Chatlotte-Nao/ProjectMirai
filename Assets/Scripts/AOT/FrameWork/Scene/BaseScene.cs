using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 切换场景(非LoadSceneMode为Additive的场景)所需要继承的基类
/// </summary>
public class BaseScene : MonoBehaviour
{
    private Camera _mUICamera;
    
    public virtual async UniTask InitAsync(object param = null)
    {
        InitScene();
    }
    
    private void InitScene()
    {
        if (_mUICamera == null)
        {
            GameObject go = GameObject.Find("UICamera");
            if (go == null)
            {
                go = new GameObject("UICamera");
                go.AddComponent<Camera>();
            }
            _mUICamera = go.GetComponent<Camera>();
            //只保留UI层
            _mUICamera.cullingMask = 1 << LayerMask.NameToLayer("UI");
        }
    }
}
