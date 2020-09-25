using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Launcher : MonoBehaviour
{
    private void Awake() {
    }
}

// 测试将热更dll放到unity工程中,能否互相调用,结果显示可以互相调用.
//public class TestClassss {
//    public TestClassss() {
//        Debug.LogError("=========");
//    }

//    public static void Func() {
//        Debug.LogError("*********");
//    }
//    public void Gunc() {
//        Debug.LogError("+++++++++");
//    }
//}
