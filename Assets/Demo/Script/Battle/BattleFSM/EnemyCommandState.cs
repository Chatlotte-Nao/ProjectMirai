
using Cysharp.Threading.Tasks;

public class EnemyCommandState : BattleStateBase
{
    public override async UniTask OnEnterState()
    {
        
    }

    public override async UniTask OnExitState()
    {
        
    }

    public EnemyCommandState(BattleStateId battleStateId) : base(battleStateId)
    {
    }
}