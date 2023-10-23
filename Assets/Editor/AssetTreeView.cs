using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

//带数据的TreeViewItem
public class AssetViewItem : TreeViewItem
{
    public ReferenceFinderData.AssetDescription data;
}

//资源引用树
public class AssetTreeView : TreeView
{
    //图标宽度
    const float kIconWidth = 18f;
    //列表高度
    const float kRowHeights = 20f;
    public AssetViewItem assetRoot;

    private GUIStyle stateGUIStyle = new GUIStyle { richText = true, alignment = TextAnchor.MiddleCenter };

    //列信息
    enum MyColumns
    {
        Name,
        Path,
        State,
        GameObjectName,
    }

    public AssetTreeView(TreeViewState state,MultiColumnHeader multicolumnHeader):base(state,multicolumnHeader)
    {
        rowHeight = kRowHeights;
        columnIndexForTreeFoldouts = 0;
        showAlternatingRowBackgrounds = true;
        showBorder = false;
        customFoldoutYOffset = (kRowHeights - EditorGUIUtility.singleLineHeight) * 0.5f; // center foldout in the row since we also center content. See RowGUI
        extraSpaceBeforeIconAndLabel = kIconWidth;
    }



    // int GetParentID(int id)
    // {
    //     int reslut = -1;
    //     var item = (AssetViewItem)FindItem(id, rootItem);
    //     if (item != null)
    //     {
    //         
    //         for (int i = id; i >= 0; i--)
    //         {
    //         
    //             Debug.Log(id+" "+item.parent.id);
    //         }
    //     }
    //     return reslut;
    // }

    //响应右击事件
    protected override void ContextClickedItem(int id)
    {
        SetExpanded(id, !IsExpanded(id));
        var item = (AssetViewItem)FindItem(id, rootItem);
        var parentItem = (AssetViewItem)FindItem(item.parent.id, rootItem);
        if (item != null)
        {
            
            for (int i = id; i >= 0; i--)
            {
            
                Debug.Log(id+" "+item.parent.id);
            }
        }

        var tartGetGo = AssetDatabase.LoadAssetAtPath(item.data.path, typeof(UnityEngine.Object)) as GameObject;
        var targetMaterial =
            AssetDatabase.LoadAssetAtPath(parentItem.data.path, typeof(UnityEngine.Material)) as Material;
        FindMaterialEditor.CheckQuoteCurrentObj(tartGetGo, targetMaterial);





    }

    //响应双击事件
    protected override void DoubleClickedItem(int id)
    {
        var item = (AssetViewItem)FindItem(id, rootItem);
        //在ProjectWindow中高亮双击资源
        if (item != null)
        {
            var assetObject = AssetDatabase.LoadAssetAtPath(item.data.path, typeof(UnityEngine.Object));
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = assetObject;
            EditorGUIUtility.PingObject(assetObject);
        }
    }
    
    //生成ColumnHeader
    public static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState(float treeViewWidth)
    {
        var columns = new[]
        {
            //图标+名称
            new MultiColumnHeaderState.Column
            {
                headerContent = new GUIContent("Name"),
                headerTextAlignment = TextAlignment.Center,
                sortedAscending = false,
                width = 200,
                minWidth = 60,
                autoResize = false,
                allowToggleVisibility = false,
                canSort = false        
            },
            //路径
            new MultiColumnHeaderState.Column
            {
                headerContent = new GUIContent("Path"),
                headerTextAlignment = TextAlignment.Center,
                sortedAscending = false,
                width = 360,
                minWidth = 60,
                autoResize = false,
                allowToggleVisibility = false,
                canSort = false
            },
            //状态
            new MultiColumnHeaderState.Column
            {
                headerContent = new GUIContent("State"),
                headerTextAlignment = TextAlignment.Center,
                sortedAscending = false,
                width = 60,
                minWidth = 60,
                autoResize = false,
                allowToggleVisibility = true,
                canSort = false          
            },
            //路径
            new MultiColumnHeaderState.Column
            {
                headerContent = new GUIContent("GameObjectName"),
                headerTextAlignment = TextAlignment.Center,
                sortedAscending = false,
                width = 360,
                minWidth = 60,
                autoResize = false,
                allowToggleVisibility = false,
                canSort = false
            },
        };
        var state = new MultiColumnHeaderState(columns);
        return state;
    }

    protected override TreeViewItem BuildRoot()
    {
        return assetRoot;
    }

    protected override void RowGUI(RowGUIArgs args)
    {
        var item = (AssetViewItem)args.item;
        for(int i = 0; i < args.GetNumVisibleColumns(); ++i)
        {
            CellGUI(args.GetCellRect(i), item, (MyColumns)args.GetColumn(i), ref args);

        }
    }

    //绘制列表中的每项内容
    void CellGUI(Rect cellRect,AssetViewItem item,MyColumns column, ref RowGUIArgs args)
    {
        
        CenterRectUsingSingleLineHeight(ref cellRect);
        switch (column)
        {
            case MyColumns.Name:
                {
                    var iconRect = cellRect;
                    iconRect.x += GetContentIndent(item);
                    iconRect.width = kIconWidth;
                    if (iconRect.x < cellRect.xMax)
                    {
                        var icon = GetIcon(item.data.path);
                        if(icon != null)
                            GUI.DrawTexture(iconRect, icon, ScaleMode.ScaleToFit);
                    }                        
                    args.rowRect = cellRect;
                    base.RowGUI(args);
                }
                break;
            case MyColumns.Path:
                {
                    // if (Path.GetExtension(item.data.path)==".prefab")
                    // {
                    //     
                    //     GUI.Label(cellRect, "AA");
                    //     
                    // }
                    GUI.Label(cellRect, item.data.path);
                }
                break;
            case MyColumns.State:
                {
                    GUI.Label(cellRect, ReferenceFinderData.GetInfoByState(item.data.state),stateGUIStyle);
                }
                break;
            // case MyColumns.GameObjectName:
            //     {
            //         if (Path.GetExtension(item.data.path)==".prefab")
            //         {
            //             
            //             var assetObject = AssetDatabase.LoadAssetAtPath(item.data.path, typeof(UnityEngine.Object)) as GameObject;
            //             if (assetObject is not null)
            //             {
            //                 var parentItem = (AssetViewItem)FindItem(item.parent.id, rootItem);
            //                 
            //                 var lists = FindGameObjectWithMaterial(assetObject.transform,Path.GetFileNameWithoutExtension(parentItem.data.name));
            //                 string objStr="";
            //                 foreach (var item2 in lists)
            //                 {
            //                     objStr += " " + item2.name;
            //                 }
            //                 GUI.Label(cellRect, objStr);
            //                 Debug.Log(item.data.name+" "+ objStr);
            //             }
            //             
            //         }
            //     }
            //     break;
        }
    }
    
    
    // List<Transform> transforms;
    //  public List<GameObject> FindGameObjectWithMaterial(Transform targetTr, string materialName)
    //     {
    //         List<GameObject> lists = new List<GameObject>();
    //         transforms ??= new List<Transform>();
    //         transforms.Clear();
    //
    //
    //         FindChildParticalMaterial(targetTr, materialName);
    //
    //         foreach (var item in transforms)
    //         {
    //             Debug.Log(item.name);
    //         }
    //
    //
    //         return lists;
    //     }
    //
    //
    //     void FindChildParticalMaterial(Transform targetTr, string materialName)
    //     {
    //         for (int i = 0; i < targetTr.childCount; i++)
    //         {
    //             FindChildParticalMaterial(targetTr.GetChild(i), materialName);
    //         }
    //
    //         if (targetTr.TryGetComponent<ParticleSystemRenderer>(out var particleSystemRenderer))
    //         {
    //             for (int i = 0; i < particleSystemRenderer.sharedMaterials.Length; i++)
    //             {
    //                 if (particleSystemRenderer != null && particleSystemRenderer.sharedMaterials[i] != null &&
    //                     particleSystemRenderer.sharedMaterials[i].name.Contains(materialName))
    //                 {
    //                     // Debug.Log(materialName+" "+particleSystemRenderer.materials[i].name);
    //                     transforms.Add(targetTr);
    //                 }
    //                 // Debug.Log(particleSystemRenderer.materials[i].name+" "+materialName+" "+particleSystemRenderer.materials[i].name.Contains(materialName));
    //             }
    //         }
    //
    //         if (targetTr.TryGetComponent<MeshRenderer>(out var meshRenderer))
    //         {
    //             for (int i = 0; i < meshRenderer.sharedMaterials.Length; i++)
    //             {
    //                 if (meshRenderer != null && meshRenderer.sharedMaterials[i] != null &&
    //                     meshRenderer.sharedMaterials[i].name.Contains(materialName))
    //                 {
    //                     
    //                     // Debug.Log(materialName+" "+particleSystemRenderer.materials[i].name);
    //                     transforms.Add(targetTr);
    //                 }
    //                 // Debug.Log(particleSystemRenderer.materials[i].name+" "+materialName+" "+particleSystemRenderer.materials[i].name.Contains(materialName));
    //             }
    //         }
    //     }
    
    
    
    
    
    
    
    
    
    
    
    
    

    //根据资源信息获取资源图标
    private Texture2D GetIcon(string path)
    {
        Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
        if (obj != null)
        {
            Texture2D icon = AssetPreview.GetMiniThumbnail(obj);
            if (icon == null)
                icon = AssetPreview.GetMiniTypeThumbnail(obj.GetType());
            return icon;
        }
        return null;
    }    
}
