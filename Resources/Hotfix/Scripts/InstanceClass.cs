﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HotFix {
    public class InstanceClass {
        private int id;

        public InstanceClass() {
            UnityEngine.Debug.Log("!!! InstanceClass::InstanceClass()");
            this.id = 0;
        }

        public InstanceClass(int id) {
            UnityEngine.Debug.Log("!!! InstanceClass::InstanceClass() id = " + id);
            this.id = id;
        }

        public int ID {
            get { return id; }
        }

        // static method
        public static void StaticFunTest() {
            int a = 1;
            a += 2;
            UnityEngine.Debug.Log("!!! InstanceClass.StaticFunTest()" + a.ToString());
        }

        public static void StaticFUncTestDateTime() {
            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0);
            time = time.AddSeconds(DateTime.Now.Second - 8 * 3600).ToLocalTime();
            UnityEngine.Debug.LogError("time:" + time);

            UnityEngine.Vector3 v3 = new UnityEngine.Vector3(1f, 2f, 3f);
            UnityEngine.Debug.Log("!!! InstanceClass.StaticFunTest()" + v3.ToString());
        }

        public static void StaticFunc() {
            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(DateTime.Now.Second);
            time = time.AddSeconds(-56).ToLocalTime();
            UnityEngine.Debug.LogError(time.ToLongTimeString() + "   " + time.ToString("H-m-s") + "  " + time.Second + "  " + time.Ticks);
        }

        public static void StaticFunTest2(int a) {
            UnityEngine.Debug.Log("!!! InstanceClass.StaticFunTest2(), a=" + a);
        }

        public static void GenericMethod<T>(T a) {
            UnityEngine.Debug.Log("!!! InstanceClass.GenericMethod(), a=" + a);
        }

        public void RefOutMethod(int addition, out List<int> lst, ref int val) {
            val = val + addition + id;
            lst = new List<int>();
            lst.Add(id);
        }
    }


}
