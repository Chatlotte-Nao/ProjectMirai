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
    
    public BattleStateId BattleStateID { get; private set; }

    public abstract UniTask OnEnterState();

    public abstract UniTask OnExitState();
    /// <summary>
    /// 在这一个状态执行的方法队列，遍历执行各个方法
    /// </summary>
    private Queue<Func<UniTask>> _tasks;

    public BattleStateBase(BattleStateId battleStateId)
    {
        this.BattleStateID = battleStateId;
    }
    
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
        //关闭自身State的ExecuteTasks行为
        IsReady = true;
    }
    
    //将行为表示为命令Command
    
    
}

public enum BattleStateId
{
    PreRoundPrepareState=0,
    RoundStartState,
    PlayerCommandState,
    EnemyCommandState,
    RoundEndState,
    BattleVictoryState,
    BattleFailState
}