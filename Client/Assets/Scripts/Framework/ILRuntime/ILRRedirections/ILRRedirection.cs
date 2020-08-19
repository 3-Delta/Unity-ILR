using System;
using System.Collections.Generic;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;

public static class ILRRedirection
{
    // 最常用的一些方法这里注册
    // 其实自动分析绑定也可以处理，只不过怕漏掉，所以这里额外处理
    public static unsafe void Register(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        // var arrMethods = typeof(UnityEngine.GameObject).GetMethods();
        // foreach (MethodInfo method in arrMethods)
        // {
        //     if (method.Name == "AddComponent" && method.GetGenericArguments().Length == 1)
        //     {
        //         appDomain.RegisterCLRMethodRedirection(method, AddComponent_Generic);
        //     }
        //     else if (method.Name == "AddComponent" && method.GetGenericArguments().Length == 1)
        //     {
        //         appDomain.RegisterCLRMethodRedirection(method, AddComponent);
        //     }
        //     else if (method.Name == "GetComponent" && method.GetGenericArguments().Length == 1)
        //     {
        //         appDomain.RegisterCLRMethodRedirection(method, GetComponent_Generic);
        //     }
        //     else if (method.Name == "GetComponent" && method.GetGenericArguments().Length == 1)
        //     {
        //         appDomain.RegisterCLRMethodRedirection(method, GetComponent);
        //     }
        // }
    }
}
