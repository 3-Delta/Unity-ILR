using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;

public static class Singleton {
    class Nested<T> where T : class, new() {
        public static readonly T instance = Activator.CreateInstance<T>();
    }

    // 内联热性
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static T Instance<T>() where T : class, new() {
        return Nested<T>.instance;
    }
}

public static class SingletonRegister<T> {
    private static HashSet<T> hash = new HashSet<T>();

    public static bool IsRegist(T ilType) {
        bool ret = ilType != null && hash.Contains(ilType);
        return ret;
    }
    public static bool Regist(T ilType) {
        if (ilType != null && !IsRegist(ilType)) {
            hash.Add(ilType);
            return true;
        }
        return false;
    }
    public static bool UnRegist(T ilType) {
        if (IsRegist(ilType)) {
            hash.Remove(ilType);
            return true;
        }
        return false;
    }
    public static void Clear() {
        hash.Clear();
    }
}
