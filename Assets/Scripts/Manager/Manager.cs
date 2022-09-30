using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public class Manager
    {
        private static readonly Manager MANAGER = new Manager();

        private event ManagerUtils.OnTick OnTick;
        private event ManagerUtils.OnTickTime OnTickTime;

        private Version version;

        private float tick;
        private float tickTime;

        private Manager()
        {
            this.version = Version.MAJOR;

            this.tick = 0.0f;
            this.tickTime = 0.0f;
        }

        private void OnEvent(Event e)
        {
            if (e != null)
            {
                OnEventVersion(e, e.GetValue<Version>(IDs.EVENT_ARGUMENT_ID__VERSION, Version.MAJOR));
            }
        }

        private void OnEventVersion(Event e, Version version)
        {
            if (this.version != version)
            {
                InvokeGame();
            }
        }

        private void OnPerformedVersionSwitchMajor(InputAction.CallbackContext callbackContext)
        {
            if (IsGame())
            {
                if (GetVersion() != Version.MAJOR)
                {
                    SetVersion(Version.MAJOR);
                }
            }
        }

        private void OnPerformedVersionSwitchMinor(InputAction.CallbackContext callbackContext)
        {
            if (IsGame())
            {
                if (GetVersion() != Version.MINOR)
                {
                    SetVersion(Version.MINOR);
                }
            }
        }

        public void InvokeMenu()
        {
            Context = Context.MENU;

            Event.Fire(
                new Event(IDs.EVENT_ID__MANAGER_CONTEXT)
                    .WithArgument<Context>(IDs.EVENT_ARGUMENT_ID__CONTEXT, Context.MENU)
            );

            Event.Fire(
                new Event(IDs.EVENT_ID__MANAGER_CONTEXT_MENU)
                    .WithArgument<Context>(IDs.EVENT_ARGUMENT_ID__CONTEXT, Context.GAME)
            );

            SceneUtils.Load(IDs.SCENE_ID__MENU, (Scene a, Scene b) =>
            {
                Event.Fire(
                    new Event(IDs.EVENT_ID__SCENE_MENU)
                );
            });
        }

        public void InvokeGame()
        {
            Context = Context.GAME;

            Event.Fire(
                new Event(IDs.EVENT_ID__MANAGER_CONTEXT)
                    .WithArgument<Context>(IDs.EVENT_ARGUMENT_ID__CONTEXT, Context.GAME)
            );

            Event.Fire(
                new Event(IDs.EVENT_ID__MANAGER_CONTEXT_GAME)
                    .WithArgument<Context>(IDs.EVENT_ARGUMENT_ID__CONTEXT, Context.GAME)
            );

            SceneUtils.Load(IDs.SCENE_ID__GAME, (Scene a, Scene b) =>
            {
                Event.Fire(
                    new Event(IDs.EVENT_ID__SCENE_GAME)
                );
            });
        }

        public void Launch()
        {
            if (IsMenu())
            {
                return;
            }

            if (IsGame())
            {
                return;
            }

            Control = new Control();
            Control.Enable();
            Control.Map.VersionSwitchMinor.performed += OnPerformedVersionSwitchMinor;
            Control.Map.VersionSwitchMajor.performed += OnPerformedVersionSwitchMajor;

            World = new World();

            Handle.GetHandle().Inject(IDs.EVENT_ID__VERSION, OnEvent);

            InvokeMenu();
        }

        public void Update()
        {
            tickTime += Time.deltaTime;

            while (tickTime >= Settings.TICK_TIME)
            {
                tick++;

                tickTime -= Settings.TICK_TIME;

                if (tick % (1.0f / Settings.TICK_TIME) == 0)
                {
                    tick = 0;
                }

                if (OnTick != null)
                {
                    OnTick.Invoke((int)tick);
                }

                if (OnTickTime != null)
                {
                    OnTickTime.Invoke((int)tick, tickTime, Settings.TICK_TIME);
                }
            }
        }

        public void Inject(ManagerUtils.OnTick onTick)
        {
            if (OnTick == null || !OnTick.GetInvocationList().Contains(onTick))
            {
                OnTick += onTick;
            }
        }

        public void Deject(ManagerUtils.OnTick onTick)
        {
            if (OnTick != null && OnTick.GetInvocationList().Contains(onTick))
            {
                OnTick -= onTick;
            }
        }

        public void Inject(ManagerUtils.OnTickTime onTickTime)
        {
            if (OnTickTime == null || !OnTickTime.GetInvocationList().Contains(onTickTime))
            {
                OnTickTime += onTickTime;
            }
        }

        public void Deject(ManagerUtils.OnTickTime onTickTime)
        {
            if (OnTickTime != null && OnTickTime.GetInvocationList().Contains(onTickTime))
            {
                OnTickTime -= onTickTime;
            }
        }

        public bool IsMenu()
        {
            return Context == Context.MENU;
        }

        public bool IsGame()
        {
            return Context == Context.GAME;
        }

        public Context Context { get; private set; }

        public Control Control { get; private set; }

        public Version GetVersion()
        {
            return this.version;
        }

        public Version SetVersion(Version version)
        {
            Event.Fire(
                new Event(IDs.EVENT_ID__VERSION)
                    .WithArgument<Version>(IDs.EVENT_ARGUMENT_ID__VERSION, version)
            );

            return this.version = version;
        }

        public World World { get; private set; }

        public override string ToString() => $"Manager ()";

        public static Manager GetManager()
        {
            return Manager.MANAGER;
        }
    }
}
