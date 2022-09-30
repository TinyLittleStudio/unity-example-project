using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    [RequireComponent(typeof(Agent))]
    [RequireComponent(typeof(AgentControls))]
    public class AgentControlsWithAnimations : MonoBehaviour
    {
        private const string ANIMATION_KEY_WALKING = "isWalking";
        private const string ANIMATION_KEY_JUMPING = "isJumping";

        private const string ANIMATION_KEY_ATTACK_MELEE = "isAttackMelee";
        private const string ANIMATION_KEY_ATTACK_SHOOT = "isAttackShoot";

        [Header("Settings")]

        [SerializeField]
        private Animator animations;

        private void Awake()
        {
            AwakeAgent();
            AwakeAgentControls();
        }

        private void AwakeAgent()
        {
            Agent = GetComponent<Agent>();
        }

        private void AwakeAgentControls()
        {
            AgentControls = GetComponent<AgentControls>();
        }

        private void LateUpdate()
        {
            LateUpdateControlsWithAnimations();
        }

        private void LateUpdateControlsWithAnimations()
        {
            if (AgentControls == null)
            {
                return;
            }

            if (AgentControls != null)
            {
                if (animations != null)
                {
                    animations.SetBool(AgentControlsWithAnimations.ANIMATION_KEY_WALKING, AgentControls.IsWalking());
                    animations.SetBool(AgentControlsWithAnimations.ANIMATION_KEY_JUMPING, AgentControls.IsJumping() || !AgentControls.IsJumpingGrounded());

                    animations.SetBool(AgentControlsWithAnimations.ANIMATION_KEY_ATTACK_MELEE, AgentControls.IsAttackMelee());
                    animations.SetBool(AgentControlsWithAnimations.ANIMATION_KEY_ATTACK_SHOOT, AgentControls.IsAttackShoot());
                }
            }
        }

        public Agent Agent { get; private set; }

        public AgentControls AgentControls { get; private set; }

        public override string ToString() => $"AgentControlsWithAnimations ()";
    }
}
