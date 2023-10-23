using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 相当于空白图片，只需要它的射线检测功能
/// </summary>
public class UIRayCast : Image
{
    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        base.OnPopulateMesh(toFill);
        //不渲染任何数据
        toFill.Clear();
    }
}
