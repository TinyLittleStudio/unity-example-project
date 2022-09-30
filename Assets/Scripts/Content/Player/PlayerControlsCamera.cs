using UnityEngine;
using UnityEngine.InputSystem;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    [RequireComponent(typeof(Player))]
    public class PlayerControlsCamera : MonoBehaviour
    {
        private const float CAMERA_VELOCITY = 6.00f;

        private const float CAMERA_OFFSET_X = 0.00f;
        private const float CAMERA_OFFSET_Y = 2.00f;

        private const float CAMERA_LOOK_FORWARD = 2.00f;

        private const float CAMERA_SHAKE = 0.25f;
        private const float CAMERA_SHAKE_FALLOFF = 16.00f;

        private const float CAMERA_CONTROL_SHAKE_TIME = 0.10f;
        private const float CAMERA_CONTROL_SHAKE_INTENSITY_X = 0.75f;
        private const float CAMERA_CONTROL_SHAKE_INTENSITY_Y = 0.50f;

        [Header("Settings")]

        [SerializeField]
        private float shakeX;

        [SerializeField]
        private float shakeY;

        [SerializeField]
        private float shakeIntensity;

        [Space(10)]

        [SerializeField]
        private Camera targetCamera;

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
            OnEnableEvents();
        }

        private void OnEnableEvents()
        {
            Handle.GetHandle().Inject(IDs.EVENT_ID__CAMERA_SHAKE, OnEvent);
        }

        private void OnDisable()
        {
            OnDisableEvents();
        }

        private void OnDisableEvents()
        {
            Handle.GetHandle().Deject(IDs.EVENT_ID__CAMERA_SHAKE, OnEvent);
        }

        private void Update()
        {
            UpdateCamera();
        }

        private void UpdateCamera()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }

            if (shakeIntensity > 0.0f)
            {
                shakeX = Random.Range(-1.0f, +1.0f) * shakeIntensity;
                shakeY = Random.Range(-1.0f, +1.0f) * shakeIntensity;

                shakeIntensity -= Time.deltaTime * PlayerControlsCamera.CAMERA_SHAKE_FALLOFF;
            }
            else
            {
                shakeX = 0.0f;
                shakeY = 0.0f;

                shakeIntensity = 0.0f;
            }
        }

        private void FixedUpdate()
        {
            FixedUpdateCamera();
        }

        private void FixedUpdateCamera()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }

            if (targetCamera == null)
            {
                targetCamera = Camera.main;
            }

            if (targetCamera != null)
            {
                Vector3 target = transform.position;
                Vector3 offset = new Vector3(
                    PlayerControlsCamera.CAMERA_OFFSET_X,
                    PlayerControlsCamera.CAMERA_OFFSET_Y
                );

                if (Player != null)
                {
                    if (Player.AgentControls != null)
                    {
                        if (Player.AgentControls.IsFlipped())
                        {
                            offset.x += 0.0f - PlayerControlsCamera.CAMERA_LOOK_FORWARD;
                        }
                        else
                        {
                            offset.x += 0.0f + PlayerControlsCamera.CAMERA_LOOK_FORWARD;
                        }
                    }
                }

                target = target + offset;
                target = Vector2.Lerp(targetCamera.transform.position, target, Time.deltaTime * PlayerControlsCamera.CAMERA_VELOCITY);

                target.x = target.x + shakeX;
                target.y = target.y + shakeY;

                targetCamera.transform.position = new Vector3(target.x, target.y, targetCamera.transform.position.z);
            }
        }

        private void OnEvent(Event e)
        {
            OnEventCamera(e);
        }

        private void OnEventCamera(Event e)
        {
            if (e == null)
            {
                return;
            }

            Shake();
        }

        public void Shake()
        {
            Shake(PlayerControlsCamera.CAMERA_SHAKE);
        }

        public void Shake(float shakeIntensity)
        {
            if (Gamepad.current != null)
            {
                Gamepad.current.SetMotorSpeeds(
                    PlayerControlsCamera.CAMERA_CONTROL_SHAKE_INTENSITY_X,
                    PlayerControlsCamera.CAMERA_CONTROL_SHAKE_INTENSITY_Y
                );
            }

            Watch.NewWatch(PlayerControlsCamera.CAMERA_CONTROL_SHAKE_TIME, (int tick, bool isFinished) =>
            {
                if (isFinished)
                {
                    InputSystem.ResetHaptics();
                }
            });

            this.shakeX = 0.0f;
            this.shakeY = 0.0f;

            this.shakeIntensity = shakeIntensity;
        }

        public Player Player { get; private set; }

        public override string ToString() => $"PlayerControlsCamera ()";
    }
}
