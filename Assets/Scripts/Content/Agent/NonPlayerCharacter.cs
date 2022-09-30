using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    [RequireComponent(typeof(Agent))]
    [RequireComponent(typeof(AgentControls))]
    [RequireComponent(typeof(AgentControlsWithAnimations))]
    [RequireComponent(typeof(AgentDialogue))]
    public class NonPlayerCharacter : MonoBehaviour
    {
        private void Awake()
        {
            AwakeAgent();
            AwakeAgentControls();
            AwakeAgentControlsWithAnimations();
            AwakeAgentDialogue();
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

        private void AwakeAgentDialogue()
        {
            AgentDialogue = GetComponent<AgentDialogue>();
        }

        private void Update()
        {
            UpdateNonPlayerCharacter();
        }

        private void UpdateNonPlayerCharacter()
        {
            if (Player.Current != null)
            {
                float threshold = Camera.main.aspect * (Camera.main.orthographicSize * 2.0f) * 1.5f;

                if (Vector2.Distance(transform.position, Player.Current.transform.position) > threshold)
                {
                    GameObject.Destroy(transform.root.gameObject);
                }
            }
        }

        public Agent Agent { get; private set; }

        public AgentControls AgentControls { get; private set; }

        public AgentControlsWithAnimations AgentControlsWithAnimations { get; private set; }

        public AgentDialogue AgentDialogue { get; private set; }

        public override string ToString() => $"NonPlayerCharacter ()";
    }
}
