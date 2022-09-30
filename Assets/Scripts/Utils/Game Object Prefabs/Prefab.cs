using System;
using UnityEngine;

using Object = UnityEngine.Object;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public class Prefab
    {
        private readonly string id;

        public Prefab(string id)
        {
            if (id == null)
            {
                throw new Exception($"Field 'Id' of class 'Prefab' cannot be null.");
            }

            if (id != null)
            {
                this.id = id;
                this.id = this.id.Trim();
                this.id = this.id.ToLower();
            }

            SetSource(null);
        }

        public Prefab(string id, Object source)
        {
            if (id == null)
            {
                throw new Exception($"Field 'Id' of class 'Prefab' cannot be null.");
            }

            if (id != null)
            {
                this.id = id;
                this.id = this.id.Trim();
                this.id = this.id.ToLower();
            }

            SetSource(source);
        }

        public bool Instantiate<T>() where T : Object
        {
            return Instantiate<T>(null);
        }

        public bool Instantiate<T>(out T t) where T : Object
        {
            return Instantiate<T>(null, out t);
        }

        public bool Instantiate<T>(Transform root) where T : Object
        {
            return Instantiate<T>(root, out _);
        }

        public bool Instantiate<T>(Transform root, out T t) where T : Object
        {
            t = null;

            if (Source != null)
            {
                if (Source is T prefab)
                {
                    t = GameObject.Instantiate<T>(prefab, root);

                    if (t != null)
                    {
                        if (t is GameObject gameObject)
                        {
                            gameObject.IsEnabled(false);
                        }
                    }
                }
            }

            return t != null;
        }

        public string Id => id;

        public Object GetSource()
        {
            return Source;
        }

        public Object SetSource(Object source)
        {
            return Source = source;
        }

        public T GetPrefab<T>() where T : Object
        {
            if (Source is T t)
            {
                return t;
            }
            else
            {
                return null;
            }
        }

        public T SetPrefab<T>(T prefab) where T : Object
        {
            Source = prefab;

            if (Source is T t)
            {
                return t;
            }
            else
            {
                return null;
            }
        }

        public Object Source { get; private set; }

        public override string ToString() => $"Prefab (Id: {Id})";
    }
}
