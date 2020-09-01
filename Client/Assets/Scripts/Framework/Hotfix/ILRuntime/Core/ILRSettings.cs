using System;
using System.IO;
using UnityEngine;

public static class ILRSettings
{
    public const string HotfixDLLRelativePath = "../../Resources/Hotfix/ILRuntime/bin/Debug/Hotfix.dll";
    public const string HotfixPdbRelativePath = "../../Resources/Hotfix/ILRuntime/bin/Debug/Hotfix.pdb";
    
    /// <summary>
    /// 设定的自动生成的绑定脚本文件夹路径
    /// </summary>
    public const string BindingAnalysisFolderPath = "Assets/Scripts/Framework/Hotfix/ILRuntime/Bindings/Analysis";
    
    /// <summary>
    /// 设定的自动生成的delegate脚本文件夹路径
    /// </summary>
    public const string DelegateAnalysisFolderPath = "Assets/Scripts/Framework/Hotfix/ILRuntime/Delegates/Analysis";
    
    /// <summary>
    /// 设定的自动生成的适配脚本文件夹路径
    /// </summary>
    public const string AdaptorAnalysisFolderPath = "Assets/Scripts/Framework/Hotfix/ILRuntime/Adapters/Analysis";

    public static string HotfixDllFullPath
    {
        get
        {
#if UNITY_EDITOR
            return Path.Combine(Application.dataPath, HotfixDLLRelativePath);
#endif
            return null;
        }
    }
    public static string HotfixPdbFullPath
    {
        get
        {
#if UNITY_EDITOR
            return Path.Combine(Application.dataPath, HotfixPdbRelativePath);
#endif
            return null;
        }
    }

    public static EAssemblyLoadType AssemblyLoadType { get; set; } = EAssemblyLoadType.ByILRuntime;
}
