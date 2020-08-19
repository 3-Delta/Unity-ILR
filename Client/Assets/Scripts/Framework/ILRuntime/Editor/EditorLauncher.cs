using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// 借助编辑器的生命周期，执行一些快捷的操作
[InitializeOnLoad]
public static class EditorLauncher
{
    static EditorLauncher()
    {
        EditorApplication.delayCall += OnCompileCode;
        EditorApplication.playModeStateChanged += OnPlayExit;
    }

    /// 退出播放模式
    private static void OnPlayExit(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            Launch();
        }
    }

    /// Editor代码刷新后执行
    private static void OnCompileCode()
    {
        if (!EditorApplication.isPlaying)
        {
            Launch();
        }
    }

    private static void Launch()
    {
        #region 注册所有管理器/表格/打包配置/其他，让管理器在编辑器下生效,方便测试方便
        #endregion
    }
}