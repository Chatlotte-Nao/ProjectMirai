using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 通过该类能使得异步方法在同步方法中(未添加async关键字)里调用
/// </summary>
public class AsyncManager : Singleton<AsyncManager>
{
    public void StartAsync(UniTask asyncTask)
    {
        asyncTask.Terminate(e =>
        {
            if (e is OperationCanceledException)
            {
                Log.Debug("cancelled");
            }
            else
            {
                Debug.LogException(e);
            }
        });
    }
    /// <summary>
    /// 通常使用async ()=>{}表达式来调用
    /// </summary>
    public void StartAsync(Func<UniTask> handler)
    {
        StartAsync(handler.Invoke());
    }

    public void StartGuardAsync(UniTask asyncTask)
    {
        StartAsync(GuardAsync(asyncTask));
    }
    /// <summary>
    /// 通常使用async ()=>{}表达式来调用，方法执行期间会阻挡玩家的点击、输入操作
    /// </summary>
    public void StartGuardAsync(Func<UniTask> handler)
    {
        StartGuardAsync(handler.Invoke());
    }

    private async UniTask GuardAsync(UniTask asyncTask)
    {
        //将BlockRayCast预制体显示出来，使其阻挡玩家的点击事件
        //后续可以拓展到键盘、手柄的输入阻挡
    }
}
