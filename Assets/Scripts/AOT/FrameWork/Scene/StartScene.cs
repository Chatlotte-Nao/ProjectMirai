using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class StartScene : BaseScene
{
    [SerializeField] private UIButton Btn_GameStart;
    [SerializeField] private UIButton GuideTestBtn;
    [SerializeField] private RectTransform Trans_Canvas;
    [SerializeField] private RectTransform Trans_FocusMask;
    public override async UniTask InitializeAsync(object param = null)
    {
        await base.InitializeAsync(param);
        Log.Debug("Load Start Scene");
        // Btn_GameStart.onClick.SubscribeAsync(async () =>
        // {
        //     Log.Debug("Start Game");
        // });
        // GuideTestBtn.onClick.SubscribeAsync(async () =>
        // {
        //     Log.Debug("触发引导");
        // });
        // SetFocusPosition(GuideTestBtn.GetComponent<RectTransform>());
        GameInitialize initialize = new GameInitialize();
        await initialize.Initialize();
        
        
    }

    private void SetFocusPosition(RectTransform buttonRect)
    {
        Vector2 size = Vector2.Scale(buttonRect.rect.size, transform.lossyScale);
        var rect = new Rect((Vector2)buttonRect.position-(size * buttonRect.pivot), size);
        var center = buttonRect.rect.center;
        var uicamera = Camera.main;
        Vector2 centerScreen = uicamera.WorldToScreenPoint(center);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(Trans_Canvas, centerScreen, uicamera, out Vector2 t);
        Trans_FocusMask.anchoredPosition = buttonRect.anchoredPosition;
        Trans_FocusMask.sizeDelta = buttonRect.rect.size * buttonRect.localScale;
    }
}
