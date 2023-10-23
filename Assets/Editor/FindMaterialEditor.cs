using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FindMaterialEditor : EditorWindow
{
    // private static GameObject _targetGo;

    static CreatMyBox myBox;

    //扩展Hierarchy面板右键弹出菜单
    [MenuItem("GameObject/查找引用/找预制体下所有材质", priority = 0)]
    public static void CheckQuoteCurrentObj(GameObject _targetGo, Material _targetMaterial)
    {
        if (myBox != null)
        {
            myBox.Close();
        }

        //创建一个窗口
        myBox = GetWindow<CreatMyBox>(false, "按材质找子物体");
        //把窗口显示出来
        myBox.Show();
        myBox._targetObj = _targetGo;
        // myBox.t = _targetGo;
        myBox.material = _targetMaterial;
        myBox.FindBtnClick();
    }

    // static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    // {
    //     if (Event.current != null && selectionRect.Contains(Event.current.mousePosition)
    //                               && Event.current.button == 1 && Event.current.type == EventType.MouseUp)
    //     {
    //         GameObject selectedGameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
    //         if (selectedGameObject)
    //         {
    //             _targetGo = selectedGameObject;
    //         }
    //     }
    // }

    // [InitializeOnLoadMethod]
    // static void StartInitializeOnLoadMethod()
    // {
    //     EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
    // }

    public class CreatMyBox : EditorWindow
    {
        public Material material;
        public GameObject _targetObj;
        List<Transform> transforms;
        // public GameObject t;

#if !UWA
        void OnGUI()
        {
            GUILayout.BeginVertical("box", GUILayout.Width(position.width));
            _targetObj =EditorGUILayout.ObjectField(new GUIContent("物体"), _targetObj, typeof(GameObject), true) as GameObject;
            GUILayout.EndVertical();
            
            GUILayout.Space(5);
            
            GUILayout.BeginVertical("box", GUILayout.Width(position.width));
            material = EditorGUILayout.ObjectField(new GUIContent("材质"), material, typeof(Material), true) as Material;
            GUILayout.EndVertical();


            GUILayout.BeginVertical("box", GUILayout.Width(position.width));
            if (GUILayout.Button("查找 "))
            {
                
                FindBtnClick();
            
                // Debug.Log(names);
            }

            GUILayout.EndVertical();
            if (transforms != null)
            {
                for (int i = 0; i < transforms.Count; i++)
                {
                    _targetObj = EditorGUILayout.ObjectField(transforms[i], typeof(GameObject), true) as GameObject;
                }
            }
        }
#endif

        public void FindBtnClick()
        {

            FindGameObjectWithMaterial(_targetObj.transform, material.name);
            Debug.Log(_targetObj.GetInstanceID());
        }

        // List<Material> _materials;

        public List<GameObject> FindGameObjectWithMaterial(Transform targetTr, string materialName)
        {
            List<GameObject> lists = new List<GameObject>();
            transforms ??= new List<Transform>();
            transforms.Clear();

            FindChildParticalMaterial(targetTr, materialName);

            foreach (var item in transforms)
            {
                Debug.Log(item.name);
            }

            return lists;
        }

        void FindChildParticalMaterial(Transform targetTr, string materialName)
        {
            for (int i = 0; i < targetTr.childCount; i++)
            {
                FindChildParticalMaterial(targetTr.GetChild(i), materialName);
            }

            if (targetTr.TryGetComponent<ParticleSystemRenderer>(out var particleSystemRenderer))
            {
                for (int i = 0; i < particleSystemRenderer.sharedMaterials.Length; i++)
                {
                    if (particleSystemRenderer != null && particleSystemRenderer.sharedMaterials[i] != null &&
                        particleSystemRenderer.sharedMaterials[i].name.Contains(materialName))
                    {
                        // Debug.Log(materialName+" "+particleSystemRenderer.materials[i].name);
                        transforms.Add(targetTr);
                    }
                    // Debug.Log(particleSystemRenderer.materials[i].name+" "+materialName+" "+particleSystemRenderer.materials[i].name.Contains(materialName));
                }
            }

            if (targetTr.TryGetComponent<MeshRenderer>(out var meshRenderer))
            {
                for (int i = 0; i < meshRenderer.sharedMaterials.Length; i++)
                {
                    if (meshRenderer != null && meshRenderer.sharedMaterials[i] != null &&
                        meshRenderer.sharedMaterials[i].name.Contains(materialName))
                    {
                        // Debug.Log(materialName+" "+particleSystemRenderer.materials[i].name);
                        transforms.Add(targetTr);
                    }
                    // Debug.Log(particleSystemRenderer.materials[i].name+" "+materialName+" "+particleSystemRenderer.materials[i].name.Contains(materialName));
                }
            }
        }
    }
}
