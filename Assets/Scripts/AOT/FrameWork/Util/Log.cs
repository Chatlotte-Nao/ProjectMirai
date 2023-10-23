using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 日志类
/// </summary>
public static class Log
{
    public static void Debug(object message)
    {
        UnityEngine.Debug.Log(message);
    }

    public static void Warning(object message)
    {
        UnityEngine.Debug.LogWarning(message);
    }

    public static void Error(object message)
    {
        UnityEngine.Debug.LogError(message);
    }
}
