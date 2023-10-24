using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

/// <summary>
/// 状态基类
/// </summary>
public abstract class BattleStateBase
{
    public bool IsReady { get; private set; }
    
    public bool IsPause { get; private set; }
    
    public int BattleStateID { get; private set; }

    protected abstract UniTask OnEnterState();

    protected abstract UniTask OnExitState();
    /// <summary>
    /// 在这一个状态执行的方法队列，遍历执行各个方法
    /// </summary>
    private Queue<Func<UniTask>> _tasks;

    public virtual void ResetState()
    {
        IsReady = false;
        _tasks.Clear();
    }

    public void PauseState()
    {
        IsPause = true;
    }

    public void ResumeState()
    {
        IsPause = false;
    }
    /// <summary>
    /// 处于该状态期间一直遍历所需要执行的方法队列，但是不会卡住线程，这里可以说实际上相当于状态机状态在Update
    /// </summary>
    public async UniTask ExecuteTasks()
    {
        while (!IsReady || IsPause)
        {
            while (_tasks.Count>0)
            {
                var task = _tasks.Dequeue();
                await task.Invoke();
            }
            //保证异步操作不堵塞主线程
            await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
        }
    }

    protected void EnqueueTask(Func<UniTask> task)
    {
        _tasks.Enqueue(task);
    }

    protected void ReadyForNextState()
    {
        IsReady = true;
    }
    
    
    
    
}