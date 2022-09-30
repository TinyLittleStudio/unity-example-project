using System.IO;
using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public static class ManagerUtils
    {
        public delegate void OnTick(int tick);
        public delegate void OnTickTime(int tick, float tickTime, float tickTimeTotal);

        public const string DIRECTORY = "Prefabs";

        public static bool Instantiate<T>(string id) where T : Object
        {
            return PrefabUtils.Instantiate<T>(Path.Combine(ManagerUtils.DIRECTORY, $"{Manager.GetManager().GetVersion()}", id), out _);
        }

        public static bool Instantiate<T>(string id, out T t) where T : Object
        {
            return PrefabUtils.Instantiate<T>(Path.Combine(ManagerUtils.DIRECTORY, $"{Manager.GetManager().GetVersion()}", id), out t);
        }

        public static bool Instantiate<T>(string id, Transform root) where T : Object
        {
            return PrefabUtils.Instantiate<T>(Path.Combine(ManagerUtils.DIRECTORY, $"{Manager.GetManager().GetVersion()}", id), root, out _);
        }

        public static bool Instantiate<T>(string id, Transform root, out T t) where T : Object
        {
            return PrefabUtils.Instantiate<T>(Path.Combine(ManagerUtils.DIRECTORY, $"{Manager.GetManager().GetVersion()}", id), root, out t);
        }
    }
}
