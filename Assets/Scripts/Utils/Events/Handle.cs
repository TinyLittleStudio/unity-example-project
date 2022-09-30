namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public class Handle
    {
        private static readonly Handle HANDLE = new Handle();

        private Handle()
        {

        }

        public void Invoke(ref Event e)
        {
            if (HookList == null)
            {
                HookList = new HookList();
            }

            if (HookList != null)
            {
                HookList.Invoke(ref e);
            }
        }

        public void Inject(string id, EventUtils.OnEvent onEvent)
        {
            if (HookList == null)
            {
                HookList = new HookList();
            }

            if (HookList != null)
            {
                HookList.Inject(id, onEvent);
            }
        }

        public void Deject(string id, EventUtils.OnEvent onEvent)
        {
            if (HookList == null)
            {
                HookList = new HookList();
            }

            if (HookList != null)
            {
                HookList.Deject(id, onEvent);
            }
        }

        public HookList HookList { get; private set; }

        public static Handle GetHandle()
        {
            return Handle.HANDLE;
        }
    }
}
