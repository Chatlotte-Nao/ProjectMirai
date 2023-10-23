using UnityEngine;

public class SingletonMonBehaviour<TInstance> : MonoBehaviour where TInstance : MonoBehaviour
{
    private static TInstance _mInstance;

    private static Transform _mRoot;
    
    private static bool HasInstance => _mInstance != null;

    public static TInstance Instance
    {
        get
        {
            if (!HasInstance)
            {
                if (_mRoot == null)
                {
                    var go = GameObject.Find("SingletonRoot");
                    if (go == null)
                    {
                        go = new GameObject("SingletonRoot");
                    }

                    _mRoot = go.transform;
                    DontDestroyOnLoad(_mRoot);
                }

                _mInstance = FindObjectOfType<TInstance>();
                if (_mInstance == null)
                {
                    GameObject go = new GameObject(typeof(TInstance).Name);
                    _mInstance = go.AddComponent<TInstance>();
                    _mInstance.transform.SetParent(_mRoot);
                }
            }

            return _mInstance;
        }
    }
}
