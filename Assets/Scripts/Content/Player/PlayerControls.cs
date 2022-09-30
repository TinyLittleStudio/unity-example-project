using UnityEngine;
using UnityEngine.InputSystem;

using static UnityEngine.InputSystem.InputAction;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    [RequireComponent(typeof(Player))]
    public class PlayerControls : MonoBehaviour
    {
        [Header("Settings")]

        [SerializeField]
        private Transform target;

        private void Awake()
        {
            AwakePlayer();
        }

        private void AwakePlayer()
        {
            Player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            OnEnableControl();
        }

        private void OnEnableControl()
        {
            Manager manager = Manager.GetManager();

            if (manager != null)
            {
                if (manager.Control != null)
                {
                    manager.Control.Map.MoveWalk.performed += OnPerformedMoveWalk;
                    manager.Control.Map.MoveJump.performed += OnPerformedMoveJump;

                    manager.Control.Map.Shoot.performed += OnPerformedShoot;
                    manager.Control.Map.ShootAim.performed += OnPerformedShootAim;
                    manager.Control.Map.ShootAimWithJoystick.performed += OnPerformedShootAimWithJoystick;

                    manager.Control.Map.Melee.performed += OnPerformedMelee;

                    manager.Control.Map.MoveWalk.canceled += OnCanceledMoveWalk;
                    manager.Control.Map.MoveJump.canceled += OnCanceledMoveJump;
                }
            }
        }

        private void OnDisable()
        {
            OnDisableControl();
        }

        private void OnDisableControl()
        {
            Manager manager = Manager.GetManager();

            if (manager != null)
            {
                if (manager.Control != null)
                {
                    manager.Control.Map.MoveWalk.performed -= OnPerformedMoveWalk;
                    manager.Control.Map.MoveJump.performed -= OnPerformedMoveJump;

                    manager.Control.Map.Shoot.performed -= OnPerformedShoot;
                    manager.Control.Map.ShootAim.performed -= OnPerformedShootAim;
                    manager.Control.Map.ShootAimWithJoystick.performed -= OnPerformedShootAimWithJoystick;

                    manager.Control.Map.MoveWalk.canceled -= OnCanceledMoveWalk;
                    manager.Control.Map.MoveJump.canceled -= OnCanceledMoveJump;
                }
            }

            InputSystem.ResetHaptics();
        }

        private void OnPerformedMoveWalk(CallbackContext callbackContext)
        {
            Vector2 velocity = callbackContext.ReadValue<Vector2>();

            if (Player != null)
            {
                if (Player.AgentControls != null)
                {
                    Player.AgentControls.ControlsWalk(velocity);
                }
            }
        }

        private void OnPerformedMoveJump(CallbackContext callbackContext)
        {
            if (Player != null)
            {
                if (Player.AgentControls != null)
                {
                    Player.AgentControls.ControlsJump();
                }
            }
        }

        private void OnPerformedShoot(CallbackContext callbackContext)
        {
            if (Player != null)
            {
                if (Player.AgentControls != null)
                {
                    if (Player.AgentControls.IsAttackMelee())
                    {
                        return;
                    }

                    if (Player.AgentControls.IsAttackShoot())
                    {
                        return;
                    }

                    if (Player.AgentControls.IsJumping() || !Player.AgentControls.IsJumpingGrounded())
                    {
                        return;
                    }

                    if (Player.GetAmmunition() > 0)
                    {
                        Player.SetAmmunition(Player.GetAmmunition() - 1);
                        
                        Player.AgentControls.ControlsShoot();
                        
                        Event.Fire(
                            new Event(IDs.EVENT_ID__USER_INTERFACE)
                        );
                    }
                }
            }
        }

        private void OnPerformedShootAim(CallbackContext callbackContext)
        {
            Vector2 target = callbackContext.ReadValue<Vector2>();

            if (Player != null)
            {
                if (Player.AgentControls != null)
                {
                    Player.AgentControls.ControlsShootAim(target);
                }
            }
        }

        private void OnPerformedShootAimWithJoystick(CallbackContext callbackContext)
        {
            Vector2 target = callbackContext.ReadValue<Vector2>();

            if (Camera.main != null)
            {
                target = Camera.main.WorldToScreenPoint((Vector2)gameObject.transform.position + target);
            }

            if (Player != null)
            {
                if (Player.AgentControls != null)
                {
                    Player.AgentControls.ControlsShootAim(target);
                }
            }
        }

        private void OnPerformedMelee(CallbackContext callbackContext)
        {
            if (Player != null)
            {
                if (Player.AgentControls != null)
                {
                    Player.AgentControls.ControlsMelee();
                }
            }
        }

        private void OnCanceledMoveWalk(CallbackContext callbackContext)
        {
            Vector2 velocity = Vector2.zero;

            if (Player != null)
            {
                if (Player.AgentControls != null)
                {
                    Player.AgentControls.ControlsWalk(velocity);
                }
            }
        }

        private void OnCanceledMoveJump(CallbackContext callbackContext)
        {

        }

        public Player Player { get; private set; }

        public override string ToString() => $"PlayerControls ()";
    }
}
