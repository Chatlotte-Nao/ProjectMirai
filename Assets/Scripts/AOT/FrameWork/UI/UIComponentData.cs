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