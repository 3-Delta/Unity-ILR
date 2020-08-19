using System.IO;
using UnityEditor;
using UnityEngine;

public class ILRInitializer
{
	/*
	[InitializeOnLoadMethod]
	private static void CopyAssemblyFiles()
	{
        if (!Directory.Exists(ILRSettings.AssemblyFolderPath))
        {
            Directory.CreateDirectory(ILRSettings.AssemblyFolderPath);
        }

		// Copy DLL
		string dllSource = Path.Combine(ILRSettings.ScriptAssembliesDir, ILRSettings.HotfixDLLFileName);
		string dllDest = Path.Combine(ILRSettings.AssemblyFolderPath, $"{ILRSettings.HotfixDLLFileName}.bytes");
		File.Copy(dllSource, dllDest, true);
		
		// Copy PDB
		string pdbSource = Path.Combine(ILRSettings.ScriptAssembliesDir, ILRSettings.HotfixPDBFileName);
		string pdbDest = Path.Combine(ILRSettings.AssemblyFolderPath, $"{ILRSettings.HotfixPDBFileName}.bytes");
		File.Copy(pdbSource, pdbDest, true);

		Debug.Log("Copy hotfix assembly files done.");
		AssetDatabase.Refresh();
	}
	*/
}