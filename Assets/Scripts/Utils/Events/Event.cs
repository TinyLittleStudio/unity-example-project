using System;
using System.Collections.Generic;
using System.Linq;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public class Event
    {
        private readonly string id;

        private List<Argument> arguments;

        public Event(string id)
        {
            if (id == null)
            {
                throw new Exception($"Field 'Id' of class 'Event' cannot be null.");
            }

            if (id != null)
            {
                this.id = id;
                this.id = id.Trim();
                this.id = id.ToLower();
            }

            this.arguments = null;
        }

        public Event(string id, params Argument[] arguments)
        {
            if (id == null)
            {
                throw new Exception($"Field 'Id' of class 'Event' cannot be null.");
            }

            if (id != null)
            {
                this.id = id;
                this.id = id.Trim();
                this.id = id.ToLower();
            }

            this.arguments = new List<Argument>(arguments);
        }

        public Event WithArgument<T>(string key, T raw)
        {
            return WithArgument<T>(key, raw, out _);
        }

        public Event WithArgument<T>(string key, T raw, out T value)
        {
            value = SetValue(key, raw);

            return this;
        }

        public string Id => id;

        public T GetValue<T>(string key, T defalutValue = default)
        {
            if (arguments == null)
            {
                arguments = new List<Argument>();
            }

            if (arguments != null)
            {
                Argument argument = arguments.FirstOrDefault(argumentTemp => StringUtils.EqualsIgnoreCase(argumentTemp.Key, key));

                if (argument == null)
                {
                    argument = new Argument(key);

                    arguments.Add(argument);
                }

                if (argument != null)
                {
                    return argument.GetValue<T>(defalutValue);
                }
            }

            return defalutValue;
        }

        public T SetValue<T>(string key, T defalutValue = default)
        {
            if (arguments == null)
            {
                arguments = new List<Argument>();
            }

            if (arguments != null)
            {
                Argument argument = arguments.FirstOrDefault(argumentTemp => StringUtils.EqualsIgnoreCase(argumentTemp.Key, key));

                if (argument == null)
                {
                    argument = new Argument(key);

                    arguments.Add(argument);
                }

                if (argument != null)
                {
                    return argument.SetValue<T>(defalutValue);
                }
            }

            return defalutValue;
        }

        public override string ToString() => $"Event (Id: {Id})";

        public static Event Fire(Event e)
        {
            Handle.GetHandle().Invoke(ref e);

            return e;
        }
    }
}
