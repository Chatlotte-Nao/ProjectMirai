using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Stack<BaseUI> _baseUis=new Stack<BaseUI>();



    public async UniTask OpenUI(BaseUI baseUI)
    {
        
    }

    public async UniTask HideUI()
    {
        
    }
    
}
