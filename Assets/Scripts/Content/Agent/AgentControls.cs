using UnityEditor;
using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    [RequireComponent(typeof(Agent))]
    public class AgentControls : MonoBehaviour
    {
        private const float GROUND_COLLISION_RADIUS = 0.125f;
        private const float GROUND_COLLISION_OFFSET_X = 0.00f;
        private const float GROUND_COLLISION_OFFSET_Y = 1.00f;

        private const float FLIP_FORCE = 16.0f;

        private const float VELOCITY = 260.0f;
        private const float VELOCITY_DURATION = 0.05f;

        private const float JUMP_FORCE = 1600.0f;

        private const float ATTACK_MELEE_RADIUS = 0.75f;
        private const float ATTACK_MELEE_OFFSET_X = 0.1f;
        private const float ATTACK_MELEE_OFFSET_Y = -0.2f;

        private const float ATTACK_MELEE_DELAY = 0.60f;
        private const float ATTACK_SHOOT_DELAY = 0.10f;

        private const float DUST_TIME = 0.5f;

        [SerializeField]
        private Vector2 velocity;

        [SerializeField]
        private Vector2 velocityNormalized;

        [Space(10)]

        [SerializeField]
        private bool isWalking;

        [SerializeField]
        private bool isJumping;

        [SerializeField]
        private bool isJumpingGrounded;

        [SerializeField]
        private bool isFlipped;

        [Space(10)]

        [SerializeField]
        private bool isAttackMelee;

        [SerializeField]
        private bool isAttackShoot;

        [Space(10)]

        [SerializeField]
        private LayerMask layerMask;

        [Space(10)]

        [SerializeField]
        private float dustTime;

        [SerializeField]
        private float dustTimeTotal;

        [Space(10)]

        [SerializeField]
        private Transform body;

        [SerializeField]
        private Transform bodyArmWithHand;

        [Space(10)]

        [SerializeField]
        private Rigidbody2D rigidbody2d;

        private void Awake()
        {
            AwakeAgent();
        }

        private void AwakeAgent()
        {
            Agent = GetComponent<Agent>();
        }

        private void Start()
        {
            StartAgent();
        }

        private void StartAgent()
        {
            dustTime = 0.0f;
            dustTimeTotal = AgentControls.DUST_TIME;
        }

        private void Update()
        {
            UpdateControls();
        }

        private void UpdateControls()
        {
            if (isFlipped)
            {
                if (rigidbody2d != null)
                {
                    rigidbody2d.gameObject.transform.localScale = Vector2.Lerp(rigidbody2d.gameObject.transform.localScale, new Vector2(-1.0f, 1.0f), Time.deltaTime * AgentControls.FLIP_FORCE);
                }
            }
            else
            {
                if (rigidbody2d != null)
                {
                    rigidbody2d.gameObject.transform.localScale = Vector2.Lerp(rigidbody2d.gameObject.transform.localScale, new Vector2(+1.0f, 1.0f), Time.deltaTime * AgentControls.FLIP_FORCE);
                }
            }

            if (isWalking)
            {
                dustTime += Time.deltaTime;

                if (dustTime >= dustTimeTotal)
                {
                    float x = transform.root.position.x;
                    float y = transform.root.position.y;

                    ParticleUtils.Play(IDs.PARTICLE_ID__AGENT_CONTROLS_WALK, x, y);

                    dustTime = 0.0f;
                    dustTimeTotal = AgentControls.DUST_TIME;
                }
            }
        }

        private void FixedUpdate()
        {
            FixedUpdateControls();
        }

        private void FixedUpdateControls()
        {
            if (rigidbody2d != null)
            {
                isJumpingGrounded = false;

                Vector2 origin = rigidbody2d.gameObject.transform.position;
                Vector2 offset = new Vector3(
                    AgentControls.GROUND_COLLISION_OFFSET_X,
                    AgentControls.GROUND_COLLISION_OFFSET_Y
                );

                Collider2D[] collider2ds = Physics2D.OverlapCircleAll(origin - offset, AgentControls.GROUND_COLLISION_RADIUS, LayerMask.GetMask(IDs.LAYER_ID__WORLD));

                if (collider2ds != null)
                {
                    foreach (Collider2D collider2d in collider2ds)
                    {
                        if (collider2d == null)
                        {
                            continue;
                        }

                        if (collider2d.gameObject == null)
                        {
                            continue;
                        }

                        if (collider2d.gameObject != rigidbody2d.gameObject)
                        {
                            isJumpingGrounded = true;
                        }
                    }
                }
            }

            FixedUpdateControlsWalk();
            FixedUpdateControlsJump();
        }

        private void FixedUpdateControlsWalk()
        {
            if (rigidbody2d != null)
            {
                velocity = velocityNormalized;

                velocity = velocity * AgentControls.VELOCITY;

                velocity = velocity * Time.fixedDeltaTime;

                velocity = Vector2.SmoothDamp(rigidbody2d.velocity, velocity, ref velocity, AgentControls.VELOCITY_DURATION);

                velocity.y = rigidbody2d.velocity.y;

                if (isAttackMelee)
                {
                    velocity = Vector2.zero;
                }

                if (isAttackShoot)
                {
                    velocity = Vector2.zero;
                }

                rigidbody2d.velocity = velocity;
            }

            isWalking = Mathf.Abs(velocityNormalized.x) > 0.0f;

            isWalking = isWalking && !isJumping && isJumpingGrounded;

            isWalking = isWalking && !isAttackMelee;

            isWalking = isWalking && !isAttackShoot;
        }

        private void FixedUpdateControlsJump()
        {
            if (isJumping)
            {
                if (rigidbody2d != null)
                {
                    float x = transform.root.position.x;
                    float y = transform.root.position.y;

                    ParticleUtils.Play(IDs.PARTICLE_ID__AGENT_CONTROLS_JUMP, x, y);

                    Vector2 velocityJumping = DirectionUtils.ToVector(Direction.NORTH);

                    velocityJumping = velocityJumping * 1.0f;

                    velocityJumping = velocityJumping * AgentControls.JUMP_FORCE;

                    rigidbody2d.AddForce(velocityJumping, ForceMode2D.Force);

                    isJumping = false;
                    isJumpingGrounded = false;
                }
            }
        }

        public void ControlsWalk(Vector2 velocity)
        {
            this.velocity = velocity;
            this.velocityNormalized = velocity.normalized;

            if (velocity.x < 0)
            {
                isFlipped = true;
            }

            if (velocity.x > 0)
            {
                isFlipped = false;
            }

            Event.Fire(
                new Event(IDs.EVENT_ID__CONTROLS_WALK)
                    .WithArgument<Agent>(IDs.EVENT_ARGUMENT_ID__AGENT, Agent)
            );
        }

        public void ControlsJump()
        {
            if (isJumping || !isJumpingGrounded)
            {
                return;
            }
            if (isAttackMelee)
            {
                return;
            }

            if (isAttackShoot)
            {
                return;
            }

            isJumping = true;

            Event.Fire(
                new Event(IDs.EVENT_ID__CONTROLS_JUMP)
                    .WithArgument<Agent>(IDs.EVENT_ARGUMENT_ID__AGENT, Agent)
            );
        }

        public void ControlsMelee()
        {
            if (isJumping || !isJumpingGrounded)
            {
                return;
            }

            if (isAttackMelee)
            {
                return;
            }

            if (isAttackShoot)
            {
                return;
            }

            Vector2 origin = rigidbody2d.gameObject.transform.position;
            Vector2 offset = new Vector3(
                AgentControls.ATTACK_MELEE_OFFSET_X,
                AgentControls.ATTACK_MELEE_OFFSET_Y
            );

            if (isFlipped)
            {
                offset = offset * new Vector2(+1.0f, +1.0f);
            }
            else
            {
                offset = offset * new Vector2(-1.0f, +1.0f);
            }

            isAttackMelee = true;

            Watch.NewWatch(AgentControls.ATTACK_MELEE_DELAY, (int tick, bool isFinished) =>
            {
                if (isFinished)
                {
                    isAttackMelee = false;
                }
            });

            Watch.NewWatch(AgentControls.ATTACK_MELEE_DELAY / 3.0f, (int tick, bool isFinished) =>
            {
                if (isFinished)
                {
                    Collider2D[] collider2ds = Physics2D.OverlapCircleAll(origin - offset, AgentControls.ATTACK_MELEE_RADIUS, layerMask);

                    if (collider2ds != null)
                    {
                        foreach (Collider2D collider2d in collider2ds)
                        {
                            if (collider2d == null)
                            {
                                continue;
                            }

                            if (collider2d.gameObject == null)
                            {
                                continue;
                            }

                            if (rigidbody2d != null)
                            {
                                if (rigidbody2d.gameObject != collider2d.gameObject)
                                {
                                    Agent agent = collider2d.gameObject.GetComponent<Agent>();

                                    if (agent != null)
                                    {
                                        agent.InflictDamage();
                                    }
                                }
                            }
                        }
                    }
                }
            });

            Event.Fire(
                new Event(IDs.EVENT_ID__CONTROLS_MELEE)
                    .WithArgument<Agent>(IDs.EVENT_ARGUMENT_ID__AGENT, Agent)
            );
        }

        public void ControlsShoot()
        {
            if (isJumping || !isJumpingGrounded)
            {
                return;
            }

            if (isAttackMelee)
            {
                return;
            }

            if (isAttackShoot)
            {
                return;
            }

            if (ManagerUtils.Instantiate<Bullet>(IDs.PREFAB_ID__BULLET, out Bullet bullet))
            {
                if (isFlipped)
                {
                    bullet.SetVelocity(Direction.WEST.ToVector());
                }
                else
                {
                    bullet.SetVelocity(Direction.EAST.ToVector());
                }

                if (bodyArmWithHand != null)
                {
                    bullet.transform.position = bodyArmWithHand.position;
                }

                bullet.IsEnabled(true);
            }

            if (ManagerUtils.Instantiate<BulletShell>(IDs.PREFAB_ID__BULLET_SHELL, out BulletShell bulletShell))
            {
                if (bodyArmWithHand != null)
                {
                    float rx = MathUtils.NumbersWithDecimals.GetRandom(0.0f, 0.5f) - 0.25f;
                    float ry = 0.0f;

                    bulletShell.transform.position = bodyArmWithHand.position + new Vector3(ry, ry);
                }

                bulletShell.IsEnabled(true);
            }

            if (ManagerUtils.Instantiate<MuzzleFlash>(IDs.PREFAB_ID__MUZZLE_FLASH, out MuzzleFlash muzzleFlash))
            {
                if (bodyArmWithHand != null)
                {
                    muzzleFlash.transform.position = bodyArmWithHand.position;
                }

                if (isFlipped)
                {
                    muzzleFlash.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                }
                else
                {
                    muzzleFlash.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 000.0f);
                }

                muzzleFlash.IsEnabled(true);
            }

            isAttackShoot = true;

            Watch.NewWatch(AgentControls.ATTACK_SHOOT_DELAY, (int tick, bool isFinished) =>
            {
                if (isFinished)
                {
                    isAttackShoot = false;
                }
            });

            Event.Fire(
                new Event(IDs.EVENT_ID__CAMERA_SHAKE)
            );

            Event.Fire(
                new Event(IDs.EVENT_ID__CONTROLS_SHOOT)
                    .WithArgument<Agent>(IDs.EVENT_ARGUMENT_ID__AGENT, Agent)
            );
        }

        public void ControlsShootAim(Vector2 target)
        {
            if (isAttackMelee)
            {
                return;
            }

            if (isAttackShoot)
            {
                return;
            }

            Vector2 lookAt = Camera.main.ScreenToWorldPoint(target);

            Vector2 lookRt = (lookAt - (Vector2)transform.position).normalized;

            if (lookRt.x < 0)
            {
                isFlipped = true;
            }

            if (lookRt.x > 0)
            {
                isFlipped = false;
            }

            Event.Fire(
                new Event(IDs.EVENT_ID__CONTROLS_SHOOT_AIM)
                    .WithArgument<Agent>(IDs.EVENT_ARGUMENT_ID__AGENT, Agent)
            );
        }

        public bool IsWalking()
        {
            return this.isWalking;
        }

        public bool IsJumping()
        {
            return this.isJumping;
        }

        public bool IsJumpingGrounded()
        {
            return this.isJumpingGrounded;
        }

        public bool IsFlipped()
        {
            return this.isFlipped;
        }

        public bool IsAttackShoot()
        {
            return this.isAttackShoot;
        }

        public bool IsAttackMelee()
        {
            return this.isAttackMelee;
        }

        public Agent Agent { get; private set; }

        public override string ToString() => $"Agent ()";
    }
}
