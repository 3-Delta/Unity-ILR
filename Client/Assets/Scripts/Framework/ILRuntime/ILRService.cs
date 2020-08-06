using System;

public static class ILRService
{ 
    public static void Init(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        // todo

#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        appDomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
#if UNITY_EDITOR
        appDomain.DebugService.StartDebugService(56300);
#endif
    }
}
