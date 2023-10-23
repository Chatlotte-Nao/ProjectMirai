using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class StartScene : BaseScene
{
    [SerializeField] private UIButton Btn_GameStart;
    public override async UniTask InitAsync(object param = null)
    {
        await base.InitAsync(param);
        Log.Debug("Load Start Scene");
        Btn_GameStart.onClick.SubscribeAsync(async () =>
        {
            Log.Debug("Start Game");
        });
    }
}
