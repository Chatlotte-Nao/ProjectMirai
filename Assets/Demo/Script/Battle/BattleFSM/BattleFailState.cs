
using Cysharp.Threading.Tasks;

public class BattleFailState : BattleStateBase
{
    public override async UniTask OnEnterState()
    {
        
    }

    public override async UniTask OnExitState()
    {
        
    }

    public BattleFailState(BattleStateId battleStateId) : base(battleStateId)
    {
    }
}