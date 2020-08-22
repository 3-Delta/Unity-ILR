using System;

public class Singleton<T> where T : class, new() {
    private static T instance = null;
    
    public static T Instance {
        get {
            if (instance == null) {
                instance = Activator.CreateInstance<T>();
            }
            return instance;
        }
    }

    protected Singleton() {
    }

    public void Release() {
        instance = null;
    }
}