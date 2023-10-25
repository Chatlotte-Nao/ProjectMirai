using Cysharp.Threading.Tasks;

/// <summary>
/// 进行战斗相关操作的执行，包括加载战斗资源等
/// </summary>
public class BattleAdmin
{
    /// <summary>
    /// 是否重新战斗
    /// </summary>
    public bool IsReplay { get; private set; }
    
    public BattleFSM BattleFsm { get; private set; }
    
    public bool IsAutoBattle { get; private set; }
    
    public bool IsSpeedUp { get; private set; }

    public async UniTask<bool> Run()
    {
        await StartBattle();
        bool isWin = await UpdateBattle();
        return isWin;
    }

    private async UniTask StartBattle()
    {
        BattleFsm = new BattleFSM();
        BattleFsm.InitState();
        //结束Loading界面的加载
        
    }

    private async UniTask<bool> UpdateBattle()
    {
        return await BattleFsm.Run();
    }
}