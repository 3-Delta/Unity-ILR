using System;
using System.IO;
using UnityEngine;

public static class ILRSettings
{
    public const string HotfixDLLRelativePath = "../../Resources/Hotfix/bin/Debug/Hotfix.dll";
    public const string HotfixPdbRelativePath = "../../Resources/Hotfix/bin/Debug/Hotfix.pdb";

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
