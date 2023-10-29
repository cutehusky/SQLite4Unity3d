using UnityEngine;

namespace SQLite4Unity3d
{
    public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                return _instance;
            }
        }
    }
}