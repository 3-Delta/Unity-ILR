using System.IO;
using UnityEngine;

[DisallowMultipleComponent]
public class Launcher : MonoBehaviour
{
    public string dllPath = "../../Resources/Hotfix/bin/Debug/Hotfix.dll";
    public string pdbPath = "../../Resources/Hotfix/bin/Debug/Hotfix.pdb";

    private void Start()
    {
        string dllFullPath = Path.Combine(Application.dataPath, dllPath);
        string pdbFullPath = Path.Combine(Application.dataPath, pdbPath);
        
        // 加载程序集
        // 解析执行
    }
}
