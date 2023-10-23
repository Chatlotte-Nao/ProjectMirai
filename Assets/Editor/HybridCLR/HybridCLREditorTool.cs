using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using HybridCLR.Editor;
using HybridCLR.Editor.Commands;
using HybridCLR.Editor.Installer;
using UnityEditor;
using Debug=UnityEngine.Debug;
public class HybridCLREditorTool
{
    /// <summary>
    /// 可通过Jenkins调用
    /// </summary>
    [MenuItem("Tools/HybridCLR/PreBuild",false,100)]
    public static void PreBuildCmd()
    {
        EditorUserBuildSettings.buildScriptsOnly = false;
        EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
        //var result=
    }
}
