using Cysharp.Threading.Tasks;

/// <summary>
/// 游戏开始时初始化一些类
/// </summary>
public class GameInitialize
{
    public async UniTask Initialize()
    {
        UIManager.Instance.InitUIManager();
    }
}