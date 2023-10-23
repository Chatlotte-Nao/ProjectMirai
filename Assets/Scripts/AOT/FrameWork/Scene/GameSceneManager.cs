using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 用来加载构建在Unity Building上的场景
/// </summary>
public class GameSceneManager : Singleton<GameSceneManager>
{

    public string CurrentSceneName { get; private set; }

    public async UniTask LoadSceneAsync<T>(string sceneName, object param = null) where T : BaseScene
    {
        //TODO 显示加载界面
        //TODO 中途进行资源释放等操作
        await SceneManager.LoadSceneAsync(sceneName);
        CurrentSceneName = sceneName;
        GameObject sceneGameObject = new GameObject(sceneName);
        T scene= sceneGameObject.AddComponent<T>();
        await scene.InitAsync(param);
        //TODO 进行UI界面的复原、对应Scene代码的处理等操作
        //TODO 结束掉加载界面
    }
}
