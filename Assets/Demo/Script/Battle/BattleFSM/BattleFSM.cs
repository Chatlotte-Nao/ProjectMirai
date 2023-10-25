using System.Collections.Generic;
using Cysharp.Threading.Tasks;


public class BattleFSM
{
    private Dictionary<BattleStateId, BattleStateBase> _battleStateDic = new Dictionary<BattleStateId, BattleStateBase>();

    private BattleStateBase _currentState;

    private PreRoundPrepareState _preRoundPrepareState;
    private RoundStartState _roundStartState;
    private PlayerCommandState _playerCommandState;
    private EnemyCommandState _enemyCommandState;
    private RoundEndState _roundEndState;
    private BattleVictoryState _battleVictoryState;
    private BattleFailState _battleFailState;

    private bool _isBattleFinish;
    public void InitState()
    {
        _preRoundPrepareState = new PreRoundPrepareState(BattleStateId.PreRoundPrepareState);
        _roundStartState = new RoundStartState(BattleStateId.RoundStartState);
        _playerCommandState = new PlayerCommandState(BattleStateId.PlayerCommandState);
        _enemyCommandState = new EnemyCommandState(BattleStateId.EnemyCommandState);
        _roundEndState = new RoundEndState(BattleStateId.RoundEndState);
        _battleVictoryState = new BattleVictoryState(BattleStateId.BattleVictoryState);
        _battleFailState = new BattleFailState(BattleStateId.BattleFailState);
        
        _battleStateDic.Add(BattleStateId.PreRoundPrepareState,_preRoundPrepareState);
        _battleStateDic.Add(BattleStateId.RoundStartState,_roundStartState);
        _battleStateDic.Add(BattleStateId.PlayerCommandState,_playerCommandState);
        _battleStateDic.Add(BattleStateId.EnemyCommandState,_enemyCommandState);
        _battleStateDic.Add(BattleStateId.RoundEndState,_roundEndState);
        _battleStateDic.Add(BattleStateId.BattleVictoryState,_battleVictoryState);
        _battleStateDic.Add(BattleStateId.BattleFailState,_battleFailState);
        
        _currentState = _preRoundPrepareState;
    }

    public async UniTask<bool> Run()
    {
        bool isWin = false;
        while (_currentState!=null)
        {
            _currentState.ResetState();
            _currentState.OnEnterState();
            await _currentState.ExecuteTasks();
            _currentState.OnExitState();
            if (!_isBattleFinish)
            {
                _currentState=await GetNextState();
            }
            else
            {
                isWin = _currentState == _battleVictoryState;
                _currentState = null;
            }
        }

        return isWin;
    }

    private async UniTask<BattleStateBase> GetNextState()
    {
        BattleStateBase nextState=null;
        if (nextState == _battleVictoryState || nextState == _battleFailState)
        {
            _isBattleFinish = true;
        }
        return nextState;
    }
    
}