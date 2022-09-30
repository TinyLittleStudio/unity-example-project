using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    [RequireComponent(typeof(Agent))]
    [RequireComponent(typeof(AgentControls))]
    [RequireComponent(typeof(AgentControlsWithAnimations))]
    public class Player : MonoBehaviour
    {
        public static Player Current { get; set; }

        [Header("Settings")]

        [SerializeField]
        private int ammunition;

        private void Awake()
        {
            AwakeAgent();
            AwakeAgentControls();
            AwakeAgentControlsWithAnimations();
        }

        private void AwakeAgent()
        {
            Agent = GetComponent<Agent>();
        }

        private void AwakeAgentControls()
        {
            AgentControls = GetComponent<AgentControls>();
        }

        private void AwakeAgentControlsWithAnimations()
        {
            AgentControlsWithAnimations = GetComponent<AgentControlsWithAnimations>();
        }

        private void Start()
        {
            StartAmmunition();
        }

        private void StartAmmunition()
        {
            ammunition = 24;

            Event.Fire(
                new Event(IDs.EVENT_ID__USER_INTERFACE)
            );
        }

        public Agent Agent { get; private set; }

        public AgentControls AgentControls { get; private set; }

        public AgentControlsWithAnimations AgentControlsWithAnimations { get; private set; }

        public int GetAmmunition()
        {
            return this.ammunition;
        }

        public int SetAmmunition(int ammunition)
        {
            return this.ammunition = ammunition;
        }

        public override string ToString() => $"Player ()";
    }
}
