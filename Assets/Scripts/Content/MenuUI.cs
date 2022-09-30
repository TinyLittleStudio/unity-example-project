using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public class MenuUI : MonoBehaviour
    {
        [Header("Settings")]

        [SerializeField]
        private Transform container;

        [SerializeField]
        private Transform containerTransition;

        private void Awake()
        {
            AwakeContainer();
            AwakeContainerTransition();
        }

        private void AwakeContainer()
        {
            if (container != null)
            {
                container.IsEnabled(false);
            }
        }

        private void AwakeContainerTransition()
        {
            if (containerTransition != null)
            {
                containerTransition.IsEnabled(false);
            }
        }

        private void Start()
        {
            StartManager();
        }

        private void StartManager()
        {
            OnContext(Manager.GetManager().Context);
        }

        private void OnEnable()
        {
            OnEnableManager();
        }

        private void OnEnableManager()
        {
            Handle.GetHandle().Inject(IDs.EVENT_ID__MANAGER_CONTEXT, OnEventManagerContext);
        }

        private void OnDisable()
        {
            OnDisableManager();
        }

        private void OnDisableManager()
        {
            Handle.GetHandle().Deject(IDs.EVENT_ID__MANAGER_CONTEXT, OnEventManagerContext);
        }

        private void OnEventManagerContext(Event e)
        {
            OnEventManagerContextRefresh(e);
        }

        private void OnEventManagerContextRefresh(Event e)
        {
            if (e != null)
            {
                OnContext(e.GetValue<Context>(IDs.EVENT_ARGUMENT_ID__CONTEXT, Context.NONE));
            }
        }

        private void OnContext(Context context)
        {
            switch (context)
            {
                case Context.MENU:
                    OnContextMenu();
                    break;

                case Context.GAME:
                    OnContextGame();
                    break;

                default:
                    break;
            }
        }

        private void OnContextMenu()
        {
            if (container != null)
            {
                container.IsEnabled(true);
            }

            if (containerTransition != null)
            {
                containerTransition.IsEnabled(true);
            }

            Manager.GetManager().InvokeGame();
        }

        private void OnContextGame()
        {
            if (container != null)
            {
                container.IsEnabled(false);
            }

            if (containerTransition != null)
            {
                containerTransition.IsEnabled(false);
            }
        }

        public override string ToString() => $"MenuUI ()";
    }
}
