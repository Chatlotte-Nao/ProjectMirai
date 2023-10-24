
using System;
using System.Collections.Generic;
using System.Linq;

public class Signal : BaseSignal
{
    public event Action Listener=delegate {  };
    
    public event Action OnceListener=delegate {  };

    public void AddListener(Action callback)
    {
        Listener = AddUnique(Listener, callback);
    }

    public void AddOnce(Action callback)
    {
        OnceListener = AddUnique(OnceListener, callback);
    }

    public void RemoveListener(Action callback)
    {
        Listener -= callback;
    }

    public void Dispatch()
    {
        Listener();
        OnceListener();
        OnceListener=delegate {  };
        base.Dispatch(null);
    }

    private Action AddUnique(Action listeners, Action callback)
    {
        if (!listeners.GetInvocationList().Contains(callback))
        {
            listeners += callback;
        }

        return listeners;
    }
}

public class Signal<T> : BaseSignal
{
    public event Action<T> Listener=delegate {  };
    
    public event Action<T> OnceListener=delegate {  };

    public void AddListener(Action<T> callback)
    {
        Listener = AddUnique(Listener, callback);
    }

    public void AddOnce(Action<T> callback)
    {
        OnceListener = AddUnique(OnceListener, callback);
    }
    
    public void RemoveListener(Action<T> callback)
    {
        Listener -= callback;
    }


    public void Dispatch(T type)
    {
        Listener(type);
        OnceListener(type);
        OnceListener=delegate {  };
        object[] args = { type };
        base.Dispatch(args);
    }

    private Action<T> AddUnique(Action<T> listeners, Action<T> callback)
    {
        if (!listeners.GetInvocationList().Contains(callback))
        {
            listeners += callback;
        }

        return listeners;
    }
}