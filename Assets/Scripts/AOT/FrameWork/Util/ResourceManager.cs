using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 资源管理器、进行各种类型资源的加载、释放
/// </summary>
public class ResourceManager : Singleton<ResourceManager>
{
    




    public async UniTask<GameObject> LoadPrefabAsync(string prefabPath,bool isAot=false)
    {
#if UNITY_EDITOR
        GameObject go= AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        return go;
#endif
    }

    public GameObject LoadPrefab(string name,bool isAot=false)
    {
        return null;
    }
}
