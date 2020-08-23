using System;
using System.Collections.Generic;

using UnityEngine;

namespace HotFix {

    public class Ger {
        public virtual void Exec(string arg) {
            Debug.LogError("Singleton Ger " + arg);
        }
    }
    public class InherGer : Ger {
        public override void Exec(string arg) {
            Debug.LogError("Singleton InherGer " + arg);
        }
    }
    public class CustomClass {
        public int a = 3;
        public void Exec() {
            Debug.LogError("==========CustomClass11111 Exec " + (a++).ToString());
        }
    }
    public class CustomClass2 {
        public int a = 3000;
        public void Exec() {
            Debug.LogError("==========CustomClass22222 Exec " + (a++).ToString());
        }
    }

    public class TestgGenericClass {

        public static void Initialize() {
            // 泛型类
            Singleton.Instance<CustomClass>().Exec();
            Singleton.Instance<CustomClass2>().Exec();

            Debug.LogError("泛型单例  CustomClass11111111  ");

            Singleton.Instance<GameObject>().name = "TTTTTTTT";
            Debug.LogError("泛型单例  GameObject");

            // 即使在appdomain中已经提前手动注册了相关activitor的重定向，后面依然会自动生成，然后因为先后顺序关系，后注册的不会顶掉先注册的
            // 而且经过测试，必须先手动注册activitor的绑定，不能使用自动生成的绑定，否则必然出错，因为自动生成的都是按照热更类型处理的
            CustomClass cs = Activator.CreateInstance<CustomClass>();
            cs.Exec();
            cs.Exec();
            cs.Exec();

            GameObject go = Activator.CreateInstance<GameObject>();
            Debug.LogError("go.name: " + go.name);

            int inta = Activator.CreateInstance<int>();
            Debug.LogError("inta: " + inta);

            cs = Activator.CreateInstance(typeof(CustomClass)) as CustomClass;
            cs.Exec();

            Type ty = cs.GetType();
            Debug.LogError(ty.Name);

            // 测试内置泛型类
            List<CustomClass> ls = new List<CustomClass>();
            ls.Add(new CustomClass());
            ls.Remove(new CustomClass());
            ls.Contains(new CustomClass());
            int count = ls.Count;

            // 泛型类继承
            //Singleton.Instance<InherGer>().Exec("哈哈");

            Ger ger = Singleton.Instance<InherGer>();
            ger.Exec("哈哈");
        }
    }
}
