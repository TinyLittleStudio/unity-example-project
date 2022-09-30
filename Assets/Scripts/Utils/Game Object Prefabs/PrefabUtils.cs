using System.Collections.Generic;
using UnityEngine;

using Object = UnityEngine.Object;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public static class PrefabUtils
    {
        private static List<Prefab> prefabs;

        private static Prefab GetPrefab<T>(string id) where T : Object
        {
            if (PrefabUtils.prefabs == null)
            {
                PrefabUtils.prefabs = new List<Prefab>();
            }

            if (PrefabUtils.prefabs != null)
            {
                foreach (Prefab prefab in PrefabUtils.prefabs)
                {
                    if (StringUtils.EqualsIgnoreCase(prefab.Id, id))
                    {
                        return prefab;
                    }
                }
            }

            return null;
        }

        private static Prefab GetPrefabOrCreate<T>(string id) where T : Object
        {
            if (PrefabUtils.prefabs == null)
            {
                PrefabUtils.prefabs = new List<Prefab>();
            }

            if (PrefabUtils.prefabs != null)
            {
                Prefab prefab = PrefabUtils.GetPrefab<T>(id);

                if (prefab == null)
                {
                    prefab = new Prefab(id);
                    prefab.SetSource(Resources.Load<T>(id));

                    PrefabUtils.prefabs.Add(prefab);
                }

                if (prefab != null)
                {
                    return prefab;
                }
            }

            return null;
        }

        public static bool HasPrefab<T>(string id) where T : Object
        {
            Prefab prefab;

            prefab = PrefabUtils.GetPrefabOrCreate<T>(id);

            return prefab != null;
        }

        public static bool HasPrefab<T>(string id, out Prefab prefab) where T : Object
        {
            prefab = PrefabUtils.GetPrefabOrCreate<T>(id);

            return prefab != null;
        }

        public static bool Instantiate<T>(string id) where T : Object
        {
            return PrefabUtils.Instantiate<T>(id, null);
        }

        public static bool Instantiate<T>(string id, out T t) where T : Object
        {
            return PrefabUtils.Instantiate<T>(id, null, out t);
        }

        public static bool Instantiate<T>(string id, Transform root) where T : Object
        {
            return PrefabUtils.Instantiate<T>(id, root, out _);
        }

        public static bool Instantiate<T>(string id, Transform root, out T t) where T : Object
        {
            t = null;

            if (PrefabUtils.HasPrefab<T>(id, out Prefab prefab))
            {
                return prefab.Instantiate<T>(root, out t);
            }

            return false;
        }
    }
}
