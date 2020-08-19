using System;

public enum EAssemblyLoadType
{
    ByILRuntime,
    ByReflection
}

public static class AssemblyProxy //: IAssembly
{
    private static IAssembly assembly = null;

    public static void Init()
    {
        if (ILRSettings.AssemblyLoadType == EAssemblyLoadType.ByILRuntime)
        {
            assembly = new AssemblyILRuntime();
        }
        else if (ILRSettings.AssemblyLoadType == EAssemblyLoadType.ByReflection)
        {
            assembly = new AssemblyMono();
        }

        assembly.Load();
    }
    public static void Clear()
    {
        assembly.Clear();
        assembly = null;
    }

    public static Type[] GetTypes()
    {
        return assembly.GetTypes();
    }
    public static object CreateInstance(string fullName)
    {
        return assembly.CreateInstance(fullName);
    }
    public static StaticMethod CreateStaticMethod(string typeNameIncludeNamespace, string methodName, int argCount)
    {
        return assembly.CreateStaticMethod(typeNameIncludeNamespace, methodName, argCount);
    }
    public static InstanceMethod CreateInstanceMethod(string typeNameIncludeNamespace, string methodName, ref object refInstance, int argCount)
    {
        return assembly.CreateInstanceMethod(typeNameIncludeNamespace, methodName, ref refInstance, argCount);
    }
}
