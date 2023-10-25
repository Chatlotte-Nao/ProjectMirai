using Cysharp.Threading.Tasks;

/// <summary>
/// 管理与战斗相关的所有类
/// </summary>
public class BattleManager : Singleton<BattleManager>
{
    public BattleAdmin BattleAdmin { get;private set; }

    public async UniTask InitializeAsync()
    {
        BattleAdmin = new BattleAdmin();
    }
    
    public async UniTask<bool> StartBattle()
    {
        return await BattleAdmin.Run();
    }

    public async UniTask FinishBattle(bool IsWin)
    {
        
    }
}