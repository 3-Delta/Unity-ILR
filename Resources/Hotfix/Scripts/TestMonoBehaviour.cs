using System;
using System.Collections.Generic;
using UnityEngine;

namespace HotFix
{
    class SomeMonoBehaviour : MonoBehaviour
    {
        float time;
        void Awake()
        {
            Debug.Log("!! SomeMonoBehaviour.Awake");
        }

        void Start()
        {
            Debug.Log("!! SomeMonoBehaviour.Start");
        }

        void Update()
        {
            if(Time.time - time > 1)
            {
                Debug.Log("!! SomeMonoBehaviour.Update, t=" + Time.time);
                time = Time.time;
            }
        }

        public void Test()
        {
            Debug.Log("SomeMonoBehaviour");
        }
    }

    class SomeMonoBehaviour2 : MonoBehaviour
    {
        public GameObject TargetGO = null;
        public Texture2D Texture = null;

        public void Test2()
        {
            Debug.Log("!!! SomeMonoBehaviour2.Test2");
        }
    }
    class SomeMonoBehaviour3 : MonoBehaviour 
        {
        public void Exec()
        {
            Debug.Log("!!! SomeMonoBehaviour3 =================");
        }
    }

    public class TestMonoBehaviour
    {
        public static void RunTest(GameObject go)
        {
            SomeMonoBehaviour t = go.AddComponent<SomeMonoBehaviour>();
            Debug.LogError("AddComponent Success! " + (t == null));
        }

        public static void RunTest2(GameObject go)
        {
            go.AddComponent<SomeMonoBehaviour2>();

            var mb = go.GetComponent<SomeMonoBehaviour2>();
            Debug.LogError("mb == null?" + (mb == null));
            mb = go.GetComponentInParent<SomeMonoBehaviour2>();
            Debug.LogError("mb == null?" + (mb == null));
            mb = go.GetComponentInChildren<SomeMonoBehaviour2>();
            Debug.LogError("mb == null?" + (mb == null));

            var mbs = go.GetComponents<SomeMonoBehaviour2>();
            Debug.LogError("mbs == null?" + (mbs == null) + " count:" + mbs.Length);

            go.AddComponent<SomeMonoBehaviour2>();
            mbs = go.GetComponentsInParent<SomeMonoBehaviour2>();
            Debug.LogError("mbs == null?" + (mbs == null) + " count:" + mbs.Length);
            mbs = go.GetComponentsInChildren<SomeMonoBehaviour2>();
            Debug.LogError("mbs == null?" + (mbs == null) + " count:" + mbs.Length);

            go.AddComponent<SomeMonoBehaviour2>();
            mbs = go.GetComponentsInParent<SomeMonoBehaviour2>(true);
            Debug.LogError("mbs == null?" + (mbs == null) + " count:" + mbs.Length);
            mbs = go.GetComponentsInChildren<SomeMonoBehaviour2>(true);
            Debug.LogError("mbs == null?" + (mbs == null) + " count:" + mbs.Length);

            go.TryGetComponent<SomeMonoBehaviour2>(out mb);
            Debug.LogError("mb == null?" + (mb == null));
            mb.Test2();
            go.TryGetComponent<SomeMonoBehaviour3>(out SomeMonoBehaviour3 m3b);
            Debug.LogError("mb == null?" + (m3b == null));

            Debug.Log("!!!TestMonoBehaviour.RunTest2 mb == null? " + (mb == null) + " mb = " + mb);
            mb.Test2();

            Debug.Log("==============");
            float f = (new Vector3(1, 1, 1) + new Vector3(2, 2, 2)).x;
            Debug.Log("==============  " + f);
        }
    }
}
