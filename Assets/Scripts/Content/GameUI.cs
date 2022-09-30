using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public class GameUI : MonoBehaviour
    {
        private const int THRESHOLD = 100;

        [Header("Settings")]

        [SerializeField]
        private Transform container;

        [SerializeField]
        private Transform containerTransition;

        [Space(10)]

        [SerializeField]
        private Transform bulletUi;

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
            Handle.GetHandle().Inject(IDs.EVENT_ID__USER_INTERFACE, OnEventUserInterface);

            Handle.GetHandle().Inject(IDs.EVENT_ID__MANAGER_CONTEXT, OnEventManagerContext);
        }

        private void OnDisable()
        {
            OnDisableManager();
        }

        private void OnDisableManager()
        {
            Handle.GetHandle().Deject(IDs.EVENT_ID__USER_INTERFACE, OnEventUserInterface);

            Handle.GetHandle().Deject(IDs.EVENT_ID__MANAGER_CONTEXT, OnEventManagerContext);
        }

        private void OnEventUserInterface(Event e)
        {
            OnEventUserInterfaceRefresh(e);
        }

        private void OnEventUserInterfaceRefresh(Event e)
        {
            int total = 0;

            if (Player.Current != null)
            {
                total = Player.Current.GetAmmunition();

                if (total > GameUI.THRESHOLD)
                {
                    total = GameUI.THRESHOLD;
                }
            }

            if (bulletUi != null)
            {
                int count = bulletUi.transform.childCount;

                for (int i = 0; i < Mathf.Abs(total - count); i++)
                {
                    if (total < count)
                    {
                        Transform transform = bulletUi.transform.GetChild((bulletUi.transform.childCount - 1) - i);

                        if (transform != null)
                        {
                            if (transform.gameObject != null)
                            {
                                GameObject.Destroy(transform.gameObject);
                            }
                        }
                    }

                    if (total > count)
                    {
                        if (ManagerUtils.Instantiate<UserInterface>(IDs.PREFAB_ID__UI_BULLET, bulletUi, out UserInterface userInterface))
                        {
                            userInterface.IsEnabled(true);
                        }
                    }
                }
            }
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
                container.IsEnabled(false);
            }

            if (containerTransition != null)
            {
                containerTransition.IsEnabled(false);
            }
        }

        private void OnContextGame()
        {
            if (container != null)
            {
                container.IsEnabled(true);
            }

            if (containerTransition != null)
            {
                containerTransition.IsEnabled(true);
            }
        }

        public override string ToString() => $"GameUI ()";
    }
}
