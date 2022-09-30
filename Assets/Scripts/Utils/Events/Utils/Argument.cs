using System;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public class Argument
    {
        private string key;

        private object raw;

        public Argument(string key, object raw = null)
        {
            if (key == null)
            {
                throw new Exception($"Field 'Key' of class 'Argument' cannot be null.");
            }

            if (key != null)
            {
                this.key = key;
                this.key = this.key.Trim();
                this.key = this.key.ToLower();
            }

            this.raw = raw;
        }

        public string Key => key;

        public object Raw => raw;

        public T GetValue<T>(T defaultValue = default)
        {
            if (raw is T t)
            {
                return t;
            }

            return defaultValue;
        }

        public T SetValue<T>(T defaultValue = default)
        {
            raw = defaultValue;

            if (raw is T t)
            {
                return t;
            }

            return defaultValue;
        }

        public override string ToString() => $"Argument (Key: {Key}, Raw: {Raw})";
    }
}
