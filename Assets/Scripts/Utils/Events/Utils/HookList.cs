using System.Collections.Generic;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public class HookList
    {
        private List<Hook> hookList;

        public HookList()
        {
            this.hookList = null;
        }

        public void Invoke(ref Event e)
        {
            if (e != null)
            {
                Hook hook = GetHookOrCreate(e.Id);

                if (hook != null)
                {
                    hook.Invoke(ref e);
                }
            }
        }

        public void Inject(string id, EventUtils.OnEvent onEvent)
        {
            Hook hook = GetHookOrCreate(id);

            if (hook != null)
            {
                hook.Inject(onEvent);
            }
        }

        public void Deject(string id, EventUtils.OnEvent onEvent)
        {
            Hook hook = GetHookOrCreate(id);

            if (hook != null)
            {
                hook.Deject(onEvent);
            }
        }

        public Hook GetHook(string id)
        {
            if (hookList == null)
            {
                hookList = new List<Hook>();
            }

            if (hookList != null)
            {
                foreach (Hook hook in hookList)
                {
                    if (StringUtils.EqualsIgnoreCase(hook.Id, id))
                    {
                        return hook;
                    }
                }
            }

            return null;
        }

        public Hook GetHookOrCreate(string id)
        {
            if (hookList == null)
            {
                hookList = new List<Hook>();
            }

            if (hookList != null)
            {
                Hook hook = GetHook(id);

                if (hook == null)
                {
                    hookList.Add(hook = new Hook(id, $"HOOK_{id}"));
                }

                return hook;
            }

            return null;
        }

        public override string ToString() => $"HookList ()";
    }
}
