using System;
using System.Collections.Generic;
using ILRuntime.Runtime.Intepreter;

public static class ILRService
{ 
    public static void Init(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        // Action
        RegisterMethodDelegate(appDomain);
        // Func
        RegisterFuncDelegate(appDomain);
        // Converter
        RegisterDelegateConverter(appDomain);
        
        // 继承适配器【热更类型继承框架类型】
        RegisterAdaptor(appDomain);
        
        // 值类型绑定【本质也是重定向】
        RegisterBinder(appDomain);

        // 重定向【热更调用框架的函数】
        RegisterRedirection(appDomain);
                
        // wainning from CLRBindingTestClass.cs : 初始化CLR绑定请放在初始化的最后一步
        // 绑定文件【本质也是重定向】
        RegisterBinding(appDomain);

#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        appDomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
#if UNITY_EDITOR
        appDomain.DebugService.StartDebugService(56300);
#endif
    }

    private static void RegisterMethodDelegate(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterMethodDelegate<List<object>>();
        appDomain.DelegateManager.RegisterMethodDelegate<ILTypeInstance>();
        appDomain.DelegateManager.RegisterMethodDelegate<UInt32>();
        appDomain.DelegateManager.RegisterMethodDelegate<Int32>();
        appDomain.DelegateManager.RegisterMethodDelegate<String>();
        appDomain.DelegateManager.RegisterMethodDelegate<Boolean>();
        appDomain.DelegateManager.RegisterMethodDelegate<Single>();
        appDomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Vector3>();
        appDomain.DelegateManager.RegisterMethodDelegate<int>();
        appDomain.DelegateManager.RegisterMethodDelegate<string>();
    }

    private static void RegisterFuncDelegate(ILRuntime.Runtime.Enviorment.AppDomain appDomain) {
        appDomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance>();
        appDomain.DelegateManager.RegisterFunctionDelegate<int, string>();
    }
    private static void RegisterDelegateConverter(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((act) =>
        {
            return new UnityEngine.Events.UnityAction(() =>
            {
                ((System.Action)act)();
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<System.Predicate<System.Reflection.ConstructorInfo>>((act) =>
        {
            return new System.Predicate<System.Reflection.ConstructorInfo>((obj) =>
            {
                return ((Func<System.Reflection.ConstructorInfo, System.Boolean>)act)(obj);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<TestDelegateMethod>((action) =>
        {
            return new TestDelegateMethod((a) =>
            {
                ((System.Action<int>)action)(a);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<TestDelegateFunction>((action) =>
        {
            return new TestDelegateFunction((a) =>
            {
                return ((System.Func<int, string>)action)(a);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<float>>((action) =>
        {
            return new UnityEngine.Events.UnityAction<float>((a) =>
            {
                ((System.Action<float>)action)(a);
            });
        });
    }
    private static void RegisterAdaptor(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        ManualAdapterRegister.Register(appDomain);
        AnalysisAdapterRegister.Register(appDomain);
    }
    private static void RegisterBinder(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.RegisterValueTypeBinder(typeof(UnityEngine.Vector2), new Vector2Binder());
        appDomain.RegisterValueTypeBinder(typeof(UnityEngine.Vector3), new Vector3Binder());
        appDomain.RegisterValueTypeBinder(typeof(UnityEngine.Quaternion), new QuaternionBinder());
    }
    private static void RegisterRedirection(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        ILRRedirection.Register(appDomain);
    }

    private static void RegisterBinding(ILRuntime.Runtime.Enviorment.AppDomain appDomain) 
    {
        ILRuntime.Runtime.Generated.CLRBindings.Initialize(appDomain);
        ILRuntime.Runtime.Generated.CLRManualBindings.Initialize(appDomain);
    }
}
