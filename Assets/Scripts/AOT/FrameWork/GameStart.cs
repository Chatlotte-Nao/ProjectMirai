using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 游戏最早启动入口
/// </summary>
public class GameStart : MonoBehaviour
{
    private void Awake()
    {
        
    }

    private async void Start()
    {
        await InitStartScene();
    }

    
    private void Update()
    {
        
    }
    /// <summary>
    /// 第一个场景无需SceneLoad，特殊处理
    /// </summary>
    private async UniTask InitStartScene()
    {
        GameObject startScene= GameObject.Find("StartScene");
        if (startScene != null)
        {
            StartScene scene= startScene.GetComponent<StartScene>();
            await scene.InitAsync();
        }
    }
}
