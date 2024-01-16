using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizeText : MonoBehaviour
{
    public LocalizeType localizeType;
    public string localizeKey;
    private void Awake()
    {
        
    }
    public void SetFont()
    {
        //根据当前语言进行字体变换设置，对TMP组件或Text组件的字体进行更改
    }
    
    public void SetText(LocalizeType type,string key)
    {
        //获取对应语言翻译文本，对TMP组件或Text组件进行赋值
    }
}
public enum LocalizeType
{
    //此处LocalizeType即为对应各个模块的Localize Excel表
    Excel1,
    Excel2,
    Excel3,
}

