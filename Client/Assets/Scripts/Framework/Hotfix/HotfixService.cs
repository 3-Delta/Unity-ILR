public enum EHotfixType 
{
    ILRuntime,
    PuerTs,
}

public static class HotfixService 
{
    public static EHotfixType hotfixType = EHotfixType.ILRuntime;
}
