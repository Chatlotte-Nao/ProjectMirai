using Cysharp.Threading.Tasks;

/// <summary>
/// 管理与战斗相关的所有类
/// </summary>
public class BattleManager : Singleton<BattleManager>
{
    public BattleFSM BattleFsm;
    public void Init()
    {
        BattleFsm = new BattleFSM();
        BattleFsm.InitState();
    }

    public async UniTask RunBattleFsm()
    {
        await BattleFsm.RunFsm();
    }

    public async UniTask FinishBattle(bool IsWin)
    {
        
    }
}