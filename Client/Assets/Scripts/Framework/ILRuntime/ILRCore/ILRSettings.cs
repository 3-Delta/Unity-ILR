using System;
using System.IO;
using UnityEngine;

public static class ILRSettings
{
    public const string HotfixDLLRelativePath = "../../Resources/Hotfix/bin/Debug/Hotfix.dll";
    public const string HotfixPdbRelativePath = "../../Resources/Hotfix/bin/Debug/Hotfix.pdb";
    
    /// <summary>
    /// 设定的自动生成的绑定脚本文件夹路径
    /// </summary>
    public const string BindingAnalysisFolderPath = "Assets/Scripts/Framework/ILRuntime/ILRBindings/Analysis";
    
    /// <summary>
    /// 设定的自动生成的delegate脚本文件夹路径
    /// </summary>
    public const string DelegateAnalysisFolderPath = "Assets/Scripts/Framework/ILRuntime/ILRDelegates";
    
    /// <summary>
    /// 设定的自动生成的适配脚本文件夹路径
    /// </summary>
    public const string AdaptorAnalysisFolderPath = "Assets/Scripts/Framework/ILRuntime/ILRAdapters/Analysis";

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
