using System;
using System.Collections.Generic;

using UnityEngine;

namespace HotFix {

    //public class Ger : Singleton<Ger> {
    //    public void Exec(string arg) {
    //        Debug.LogError("Singleton Exec " + arg);
    //    }
    //}
    public class CustomClass {
        public void Exec() {
            Debug.LogError("CustomClass Exec ");
        }
    }

    public class TestgGenericClass {

        public static void Initialize() {
            // 泛型类继承
            //Ger.Instance.Exec("TestgGenericClass Call Singleton Ger");

            // 泛型类
            //Singleton<CustomClass>.Instance.Exec();

            // 测试内置泛型类
            List<CustomClass> ls = new List<CustomClass>();
            ls.Add(new CustomClass());
            ls.Remove(new CustomClass());
            ls.Contains(new CustomClass());
        }
    }
}
