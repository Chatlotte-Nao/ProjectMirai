using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public static class EventExtentsions
{
    /// <summary>
    /// 普通地调用异步方法
    /// </summary>
    public static void SubscribeAsync(this UnityEvent evt,Func<UniTask> handler)
    {
        evt.AddListener(delegate { AsyncManager.Instance.StartAsync(handler); });
    }
    /// <summary>
    /// 调用异步方法时，会阻止玩家点击输入事件的响应
    /// </summary>
    public static void GuardSubscribeAsync(this UnityEvent evt,Func<UniTask> handler)
    {
        evt.AddListener(delegate { AsyncManager.Instance.StartGuardAsync(handler); });
    }
}
