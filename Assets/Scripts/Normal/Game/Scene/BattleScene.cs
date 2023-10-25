
using Cysharp.Threading.Tasks;

public class BattleScene : BaseScene
{
    public override async UniTask InitializeAsync(object param = null)
    {
        await base.InitializeAsync(param);
        await BattleManager.Instance.InitializeAsync();
        AsyncManager.Instance.StartAsync(StartBattle);
    }

    private async UniTask StartBattle()
    {
        bool isWin = await BattleManager.Instance.StartBattle();
        await BattleManager.Instance.FinishBattle(isWin);
    }
}