// 宏定义4种热更新方式
#define HOTFIX_ILR
#define HOTFIX_Ts
#define HOTFIX_Native
#define HOTFIX_Reflection

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
