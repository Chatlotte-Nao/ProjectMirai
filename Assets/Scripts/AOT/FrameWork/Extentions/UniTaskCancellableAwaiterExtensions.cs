using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class UniTaskCancellableAwaiterExtensions
{
    public static UniTaskTermination Terminate(this UniTask task, Action<Exception> onError)
    {
        var term = new UniTaskTermination(task.GetAwaiter(), onError);
        term.Subscribe();
        return term;
    }
}
