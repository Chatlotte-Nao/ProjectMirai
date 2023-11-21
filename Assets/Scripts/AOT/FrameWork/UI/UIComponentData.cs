using UnityEngine;
using System;
using System.Collections.Generic;

public class UIComponentData : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private string[] m_aliases;
#endif

    [SerializeField] private UnityEngine.Object[] m_objects;
    private int m_index;
    public void Reset()
    {
        this.m_index = 0;
    }
    public object CurrentObject()
    {
        if (m_index + 1 <= m_objects.Length)
        {
            return m_objects[m_index++];
        }
        else
        {
            return null;
        }
    }
    public UnityEngine.Object[] Objects
    {
        get { return m_objects; }
    }
    private ItemInfo newItemInfo(UnityEngine.Object ob, ItemInfo info = null)
    {
        if (info == null)
        {
            info = new ItemInfo();
        }
        if (ob != null)
        {
            var list = new List<string>();
            var types = new List<Type>();
            if (ob is GameObject)
            {
                var go = ob as GameObject;
                info.index = addComponentsTypes(go, null, list, types);
                info.gameObject = go;
            }
            else if (ob is Component)
            {
                var co = ob as Component;
                info.index = addComponentsTypes(co.gameObject, co, list, types);
                info.gameObject = co.gameObject;
            }
            else
            {
                var t = ob.GetType();
                list.Add(t.Name);
                types.Add(t);
            }

            info.types = list.ToArray();
            info.components = types.ToArray();
        }
        else
        {
            info.types = new string[1] { "none" };
            info.components = new Type[1] { null };
        }
        return info;
    }
    
    private int addComponentsTypes(GameObject go, Component co, List<string> list, List<Type> types, int index = 0)
    {
        //GameObject引用比较特殊，需要进行特殊处理
        list.Add("GameObject");
        types.Add(typeof(GameObject));
        var cs = go.GetComponents(typeof(Component));
        foreach (var t in cs)
        {
            list.Add(t.GetType().Name);
            types.Add(t.GetType());
        }

        if (co != null)
        {
            var t = co.GetType().Name;
            return list.IndexOf(t);
        }

        return index;
    }
}

public class ItemInfo

{
    public string[] types = { };
    public Type[] components = { };
    public int index = 0;
    public GameObject gameObject;
    public UnityEngine.Object getValue()
    {
        var t = components[index];
        if (t == typeof(GameObject))
        {
            return gameObject;
        }

        return gameObject.GetComponent(t);
    }
}