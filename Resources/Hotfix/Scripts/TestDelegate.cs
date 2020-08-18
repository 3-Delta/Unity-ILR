using System;
using System.Collections.Generic;

namespace HotFix
{
    public class TestDelegate
    {
        static TestDelegateMethod delegateMethod;
        static TestDelegateFunction delegateFunc;
        static Action<string> delegateAction;

        public static void Initialize()
        {
            delegateMethod = Method;
            delegateFunc = Function;
            delegateAction = Action;
        }

        public static void RunTest()
        {
            delegateMethod(123);
            var res = delegateFunc(456);
            UnityEngine.Debug.Log("!! TestDelegate.RunTest res = " + res);
            delegateAction("rrr");
        }

        public static void Initialize2()
        {
            UnityEngine.Debug.Log("!! Initialize2  测试 += Delegate.Combine");
            DelegateDemo.TestMethodDelegate += Method;
            DelegateDemo.TestFunctionDelegate += Function;
            DelegateDemo.TestActionDelegate += Action;
        }

        public static void RunTest2()
        {
            DelegateDemo.TestMethodDelegate(123);
            var res = DelegateDemo.TestFunctionDelegate(456);
            UnityEngine.Debug.Log("!! TestDelegate.RunTest2 res" + res);
            DelegateDemo.TestActionDelegate("rrr");
        }

        public static void Initialize3() {
            UnityEngine.Debug.Log("!! TestDelegate.RunTest3 +=" );
            DelegateDemo.onLoaded += Method;
        }

        public static void Initialize4() {
            UnityEngine.Debug.Log("!! TestDelegate.RunTest4 AddLoadedListener");
            DelegateDemo.RemoveLoadedListener(Method);
            DelegateDemo.AddLoadedListener(Method);
        }

        static void Method(int a)
        {
            UnityEngine.Debug.Log("!! TestDelegate.Method, a = " + a);
        }

        static string Function(int a)
        {
            return a.ToString();
        }

        static void Action(string a)
        {
            UnityEngine.Debug.Log("!! TestDelegate.Action, a = " + a);
        }
    }
}
