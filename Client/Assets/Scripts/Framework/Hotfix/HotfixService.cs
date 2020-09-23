public enum EHotfixType 
{
    ILRuntime,
    PuerTs,
    Native, // 原生代码
    Reflection, // 反射  原生执行,可以将外部的Hotfix的dll直接拖拽到工程中, 也可以软链接的形式共享热更源代码或者热更dll
}

public static class HotfixService 
{
    public static EHotfixType hotfixType = EHotfixType.Native;
}
