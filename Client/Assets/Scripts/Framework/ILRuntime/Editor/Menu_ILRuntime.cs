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

        AdapterGenerater.Generate(types);
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

            // Crossbind Adapter is needed to generate the correct binding code

            ILRService.Init(appDomain);
            // Instead of
            // ManualAdapterRegister.Register(appDomain);
            // AnalysisAdapterRegister.Register(appDomain);

            // Todo 生成原理？？？
            // 生成所有绑定脚本
            ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(appDomain, ILRSettings.BindingAnalysisFolderPath);
		}

        AssetDatabase.Refresh();
	}
}
