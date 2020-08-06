using System;
using System.IO;
using UnityEngine;

[DisallowMultipleComponent]
public class Launcher : MonoBehaviour
{
    private void Start()
    {
        string dllFullPath = ILRSettings.HotfixDllFullPath;
        string pdbFullPath = ILRSettings.HotfixPdbFullPath;
        
        // 加载程序集
        AssemblyProxy.Init();
        AssemblyProxy.CreateStaticMethod("Hotfix.InstanceClass", "StaticFunTest", 0);
        AssemblyProxy.CreateStaticMethod("Hotfix.InstanceClass", "StaticFunTest2", 1);
    }

    private void OnDestroy()
    {
        AssemblyProxy.Clear();
    }
}
