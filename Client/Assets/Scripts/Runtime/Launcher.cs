using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Launcher : MonoBehaviour
{
}

#if UNITY_EDITOR
public class Filter : UnityEditor.Build.IFilterBuildAssemblies
{
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
#endif
