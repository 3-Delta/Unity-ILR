using System;
using System.Collections.Generic;

/// <summary>
/// 能否进一步进行优化，将所有ref类型全部使用List<Object>代替
/// </summary>
public static class ZeroList {
    private static readonly Dictionary<Type, Object> zeroList = new Dictionary<Type, Object>();
    
    public static int Count {
        get {
            return zeroList.Count;
        }
    }
    
    public static List<T> GetZeroList<T>(this List<T> list)
    {
        return GetZeroList<T>();
    }
    public static List<T> GetZeroList<T>()
    {
        List<T> ret = null;
        Type type = typeof(T);
        if (zeroList.ContainsKey(type))
        {
            ret = zeroList[type] as List<T>;
            ret.Clear();
        }
        else
        {
            // 如何让这个list在执行add的时候，始终失败
            // 这里重写了List,但是因为不是virtual的关系，导致外部list依然调用的是list自己的函数
            ret = new List<T>(0);
            zeroList.Add(type, ret);
        }
        return ret;
    }
}
