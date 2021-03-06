using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

public static class Menu_ILRuntime
{
    // Todo : 将来修改为自动获取所有继承自主工程的类，然后获取adapter
    [MenuItem("Tools/ILRuntime/Generate CLR Adaptor Code by Analysis")]
    private static void GenerateCLRAdaptorByAnalysis()
    {
        var types = new List<Type>();
        //types.Add((typeof(UnityEngine.ScriptableObject)));
        //types.Add((typeof(System.Exception)));
        //types.Add(typeof(System.Collections.IEnumerable));
        types.Add((typeof(IComparable)));
        types.Add((typeof(IComparable<object>)));
        types.Add((typeof(IEnumerable)));
        types.Add((typeof(IEqualityComparer<object>)));

        AdapterCodeGenerater.Generate(types);
    }
    
    [MenuItem("Tools/ILRuntime/Generate CLR Delegate Code by Analysis")]
    private static void GenerateCLRDelegateByAnalysis()
    {
	    AssetDatabase.DeleteAsset(ILRSettings.DelegateAnalysisFolderPath);
	    Directory.CreateDirectory(ILRSettings.DelegateAnalysisFolderPath);
	    
	    string dllFilePath = ILRSettings.HotfixDllFullPath;
	    using (FileStream fs = new FileStream(dllFilePath, FileMode.Open, FileAccess.Read))
	    {
		    ILRuntime.Runtime.Enviorment.AppDomain appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();
		    appDomain.LoadAssembly(fs);
		    ILRService.Init(appDomain);
		    // 生成所有绑定脚本
		    DelegateCodeGenerater.Generate(appDomain, ILRSettings.DelegateAnalysisFolderPath);
	    }

	    AssetDatabase.Refresh();
    }
    
    [MenuItem("Tools/ILRuntime/Generate CLR Binding Code by Analysis")]
    private static void GenerateCLRBindingByAnalysis()
	{
		// 先删除旧代码
		AssetDatabase.DeleteAsset(ILRSettings.BindingAnalysisFolderPath);
		Directory.CreateDirectory(ILRSettings.BindingAnalysisFolderPath);

		// 分析热更DLL来生成绑定代码
		string dllFilePath = ILRSettings.HotfixDllFullPath;
		using (FileStream fs = new FileStream(dllFilePath, FileMode.Open, FileAccess.Read))
		{
            ILRuntime.Runtime.Enviorment.AppDomain appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();
            appDomain.LoadAssembly(fs);
            
            ILRService.Init(appDomain);
            ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(appDomain, ILRSettings.BindingAnalysisFolderPath);
		}

        AssetDatabase.Refresh();
	}
}
