using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalBus
{
    public static SignalBus GlobalSignal = new SignalBus();

    private Dictionary<int, BaseSignal> _signalMap = null;

    public SignalBus()
    {
        _signalMap = new Dictionary<int, BaseSignal>();
    }

    ~SignalBus()
    {
        _signalMap.Clear();
    }

    public Signal GetSignal(int type)
    {
        BaseSignal signal = null;
        if(!_signalMap.TryGetValue(type,out signal))
            _signalMap.Add(type,signal=new Signal());
        return signal as Signal;
    }
    
    public Signal<T> GetSignal<T>(int type)
    {
        BaseSignal signal = null;
        if(!_signalMap.TryGetValue(type,out signal))
            _signalMap.Add(type,signal=new Signal<T>());
        return signal as Signal<T>;
    }
    
    public void AddListener(int type, Action callback)
    {
        GetSignal(type).AddListener(callback);
    }
    
    public void AddListener<T>(int type, Action<T> callback)
    {
        GetSignal<T>(type).AddListener(callback);
    }

    public void RemoveListener(int type, Action callback)
    {
        GetSignal(type).RemoveListener(callback);
    }
    
    public void RemoveListener<T>(int type, Action<T> callback)
    {
        GetSignal<T>(type).RemoveListener(callback);
    }
    
    public void Dispatch(int type)
    {
        GetSignal(type).Dispatch();
    }

    public void Dispatch<T>(int type, T param1)
    {
        GetSignal<T>(type).Dispatch(param1);
    }
}
