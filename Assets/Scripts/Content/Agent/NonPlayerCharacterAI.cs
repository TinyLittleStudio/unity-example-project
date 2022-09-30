using UnityEngine;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    [RequireComponent(typeof(NonPlayerCharacter))]
    public class NonPlayerCharacterAI : MonoBehaviour
    {
        private const float TARGET_DISTANCE_PURSUE = 4.0f;
        private const float TARGET_DISTANCE_PURSUE_ATTACK = 0.75f;

        private const float IDLE_TIME = 4.0f;

        private const float DIALOGUE_DELAY_MIN = 4.0f;
        private const float DIALOGUE_DELAY_MAX = 12.0f;

        [Header("Settings")]

        [SerializeField]
        private State state;

        [Space(10)]

        [SerializeField]
        private float x;

        [SerializeField]
        private float y;

        [SerializeField]
        private float distance;

        [SerializeField]
        private Direction direction;

        [Space(10)]

        [SerializeField]
        private float time;

        [SerializeField]
        private float timeTotal;

        [Space(10)]

        [SerializeField]
        private float dialogueTime;

        [SerializeField]
        private float dialogueTimeTotal;

        [SerializeField]
        private string[] dialogues;

        private void Awake()
        {
            AwakeNonPlayerCharacter();
        }

        private void AwakeNonPlayerCharacter()
        {
            NonPlayerCharacter = GetComponent<NonPlayerCharacter>();
        }

        private void Update()
        {
            if (Player.Current != null)
            {
                x = Player.Current.transform.position.x;
                y = Player.Current.transform.position.y;
            }

            UpdateNonPlayerCharacter();
            UpdateNonPlayerCharacterAI();

            UpdateNonPlayerCharacterDialogue();
        }

        private void UpdateNonPlayerCharacter()
        {
            if (NonPlayerCharacter == null)
            {
                return;
            }

            if (NonPlayerCharacter.AgentControls == null)
            {
                return;
            }

            if (x >= transform.position.x)
            {
                direction = Direction.EAST;
            }

            if (x <= transform.position.x)
            {
                direction = Direction.WEST;
            }

            distance = Vector2.Distance(new Vector2(x, y), transform.position);
        }

        private void UpdateNonPlayerCharacterAI()
        {
            if (distance < NonPlayerCharacterAI.TARGET_DISTANCE_PURSUE)
            {
                state = State.PURSUE;
            }

            if (distance < NonPlayerCharacterAI.TARGET_DISTANCE_PURSUE_ATTACK)
            {
                state = State.PURSUE_ATTACK;
            }

            time += Time.deltaTime;

            if (time >= timeTotal)
            {
                if (!StateUtils.IsPursuit(state))
                {
                    switch (MathUtils.Numbers.GetRandom(0, 2))
                    {
                        case 0:
                            state = State.IDLE;
                            break;

                        case 1:
                            state = State.IDLE_JUMP;
                            break;

                        case 2:
                            state = State.IDLE_WALK;
                            break;

                        default:
                            break;
                    }
                }

                time = 0.0f;
                timeTotal = NonPlayerCharacterAI.IDLE_TIME;
            }

            switch (state)
            {
                case State.IDLE:
                    break;

                case State.IDLE_WALK:
                    NonPlayerCharacter.AgentControls.ControlsWalk(new Vector2(1.0f, 0.0f) * direction.ToVector());
                    break;

                case State.IDLE_JUMP:
                    NonPlayerCharacter.AgentControls.ControlsJump();
                    break;

                case State.PURSUE:
                    NonPlayerCharacter.AgentControls.ControlsWalk(new Vector2(1.0f, 0.0f) * direction.ToVector());
                    break;

                case State.PURSUE_ATTACK:
                    NonPlayerCharacter.AgentControls.ControlsWalk(new Vector2(0.0f, 0.0f));
                    NonPlayerCharacter.AgentControls.ControlsMelee();
                    break;

                default:
                    break;
            }
        }

        private void UpdateNonPlayerCharacterDialogue()
        {
            if (dialogues != null)
            {
                if (dialogues.Length > 0)
                {
                    dialogueTime += Time.deltaTime;

                    if (dialogueTime >= dialogueTimeTotal)
                    {
                        NonPlayerCharacter.AgentDialogue.NewDialogue(dialogues[MathUtils.Numbers.GetRandom(0, dialogues.Length - 1)]);

                        dialogueTime = 0.0f;
                        dialogueTimeTotal = MathUtils.NumbersWithDecimals.GetRandom(
                            0.0f,
                            NonPlayerCharacterAI.DIALOGUE_DELAY_MAX - NonPlayerCharacterAI.DIALOGUE_DELAY_MIN
                        ) + NonPlayerCharacterAI.DIALOGUE_DELAY_MIN;
                    }
                }
            }
        }

        public NonPlayerCharacter NonPlayerCharacter { get; private set; }

        public override string ToString() => $"NonPlayerCharacterAI ()";
    }
}
