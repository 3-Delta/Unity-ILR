using UnityEngine;

namespace Services {
    // 模仿自：Addressables的新版本的ComponentSingleton
    [ExecuteInEditMode]
    public abstract class ComponentSingleton<T> : MonoBehaviour where T : ComponentSingleton<T> {
        private static T instance;

        public static bool Exists => instance != null;

        public static T Instance {
            get {
                if (instance == null) {
                    instance = FindObjectOfType<T>() ?? Create();
                }

                return instance;
            }
        }

        private static T Create() {
            GameObject go = new GameObject();

            if (Application.isPlaying) {
                DontDestroyOnLoad(go);
                go.hideFlags = HideFlags.DontSave;
            }
            else {
                go.hideFlags = HideFlags.HideAndDontSave;
            }

            var inst = go.AddComponent<T>();
            go.name = typeof(T).Name;
            return inst;
        }

        public static void Release() {
            if (Exists) {
                DestroyImmediate(Instance.gameObject);
                instance = null;
            }
        }

        private void Reset() {
            if (instance != null && instance != this) {
                DestroyImmediate(gameObject);
                return;
            }

            instance = this as T;
        }
    }
}