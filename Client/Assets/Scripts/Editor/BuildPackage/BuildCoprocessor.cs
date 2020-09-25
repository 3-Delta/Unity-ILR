using System.Collections.Generic;

using UnityEngine;

// build的时候过滤一些程序集,不支持添加
public class Filter : UnityEditor.Build.IFilterBuildAssemblies {
    public int callbackOrder => 1;

    public string[] OnFilterAssemblies(UnityEditor.BuildOptions buildOptions, string[] assemblies) {
        for (int i = 0, length = assemblies.Length; i < length; ++i) {
            Debug.LogError("assemblies[i]: " + assemblies[i]);
        }

        var list = new List<string>(assemblies);
        list.Add("Library/PlayerScriptAssemblies/Hotfix.dll");
        return list.ToArray();
    }
}
