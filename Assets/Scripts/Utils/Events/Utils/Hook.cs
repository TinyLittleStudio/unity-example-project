using System;
using System.Linq;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public class Hook
    {
        private event EventUtils.OnEvent OnEvent;

        private readonly string id;
        private readonly string ident;

        public Hook(string id)
        {
            if (id == null)
            {
                throw new Exception($"Field 'Id' of class 'Hook' cannot be null.");
            }

            if (id != null)
            {
                this.id = id;
                this.id = this.id.Trim();
                this.id = this.id.ToLower();
            }

            this.ident = null;
        }

        public Hook(string id, string ident)
        {
            if (id == null)
            {
                throw new Exception($"Field 'Id' of class 'Hook' cannot be null.");
            }

            if (id != null)
            {
                this.id = id;
                this.id = this.id.Trim();
                this.id = this.id.ToLower();
            }

            this.ident = ident;
        }

        public void Invoke(ref Event e)
        {
            if (e != null)
            {
                string id = e.Id;

                if (StringUtils.EqualsIgnoreCase(id, this.id))
                {
                    if (OnEvent != null)
                    {
                        OnEvent.Invoke(e);
                    }
                }
            }
        }

        public void Inject(EventUtils.OnEvent onEvent)
        {
            if (OnEvent == null || !OnEvent.GetInvocationList().Contains(onEvent))
            {
                OnEvent += onEvent;
            }
        }

        public void Deject(EventUtils.OnEvent onEvent)
        {
            if (OnEvent != null && OnEvent.GetInvocationList().Contains(onEvent))
            {
                OnEvent -= onEvent;
            }
        }

        public string Id => id;

        public string Ident => ident;

        public override string ToString() => $"Hook (Id: {Id}, Ident: {Ident})";
    }
}
