using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 用来处理拖动UI界面时，如果有粒子特效不被Mask隐藏的情况
/// </summary>
public class ParticleSystemMask : MonoBehaviour
{
    private Renderer[] _meshes;

    private void OnEnable()
    {
        CalculateMaskRect();
    }

    private void OnRectTransformDimensionsChange()
    {
        CalculateMaskRect();
    }

    private void Start()
    {
        _meshes = GetComponentsInChildren<Renderer>(true);
        SetOrderInLayer();
        CalculateMaskRect();
    }
    
    private void CalculateMaskRect()
    {
        var corners = new Vector3[4];
        var scroll = transform.FindInParents<ScrollRect>();
        if (scroll)
        {
            Mask mask = scroll.gameObject.GetComponentInChildren<Mask>();
            if (mask != null)
            {
                RectTransform rectTransform=mask.transform as RectTransform;
                rectTransform.GetWorldCorners(corners);
                SetShaderClip(new Vector4(corners[0].x,corners[0].y,corners[2].x,corners[2].y));
            }
            else
            {
                RectMask2D rectMask2D = scroll.gameObject.GetComponentInChildren<RectMask2D>();
                if (rectMask2D != null)
                {
                    RectTransform rectTransform=rectMask2D.transform as RectTransform;
                    rectTransform.GetWorldCorners(corners);
                    SetShaderClip(new Vector4(corners[0].x,corners[0].y,corners[2].x,corners[2].y));
                }
            }
        }
    }
    
    private void SetShaderClip(Vector4 maskRect)
    {
        if (_meshes != null && _meshes.Length > 0)
        {
            foreach (var mesh in _meshes)
            {
                var material = mesh.material;
                material.SetVector("_MaskRect",maskRect);
                //配合shader中加上这段shader来使用，能够将Mask区域外的元素给裁剪掉
                // #ifdef _MASK_RECT_ON
                // float clipValue = (IN.worldPos.x >= _MaskRect[0] );
                // clipValue *= (IN.worldPos.x <= _MaskRect[2]);
                // clipValue *= (IN.worldPos.y >= _MaskRect[1]);
                // clipValue *= (IN.worldPos.y <= _MaskRect[3]);
                // clip( clipValue - 0.01f );
                // #endif
            }
        }
    }

    private void SetOrderInLayer()
    {
        //TODO 获得当前ui界面的canvasorder
        int order=0;
        var particles= GetComponentsInChildren<ParticleSystem>(true);
        foreach (var particle in particles)
        {
            particle.GetComponent<Renderer>().sortingOrder = order;
        }
    }
}
