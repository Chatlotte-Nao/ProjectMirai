using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 用来处理UniTask异步过程中取消的操作
/// </summary>
public class UniTaskTermination
{
    private readonly CancellationToken? _cancellationToken;
    private readonly Action _completed;
    private readonly Action<Exception> _onError;
    private UniTask.Awaiter _taskAwaiter;

    public UniTaskTermination(UniTask.Awaiter taskAwaiter, Action<Exception> onError)
    {
        _taskAwaiter = taskAwaiter;
        _onError = onError;
    }
    
    public UniTaskTermination(UniTask.Awaiter taskAwaiter, Action<Exception> onError,Action completed)
    {
        _taskAwaiter = taskAwaiter;
        _onError = onError;
        _completed = completed;
    }
    
    public UniTaskTermination(UniTask.Awaiter taskAwaiter, Action<Exception> onError,Action completed,
        CancellationToken cancellationToken)
    {
        _taskAwaiter = taskAwaiter;
        _onError = onError;
        _completed = completed;
        _cancellationToken = cancellationToken;
    }

    public void Subscribe()
    {
        _taskAwaiter.OnCompleted(OnCompleted);
    }

    private void OnCompleted()
    {
        try
        {
            _taskAwaiter.GetResult();
            //检查并处理取消请求，满足条件情况下抛出异常，终止任务
            if (_cancellationToken.HasValue && _cancellationToken.Value.IsCancellationRequested)
            {
                throw new TaskCanceledException();
            }
            _completed?.Invoke();
        }
        catch (Exception e)
        {
            _onError(e);
        }
    }
}
