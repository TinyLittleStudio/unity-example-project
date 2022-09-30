using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    [RequireComponent(typeof(Agent))]
    public class AgentDialogue : MonoBehaviour
    {
        private const float DIALOGUE_TIME_PER_CHARACTER = 0.10f;
        private const float DIALOGUE_TIME_PER_CHARACTER_BASIS = 0.75f;

        private const float DIALOGUE_DELAY = 2.00f;

        private const float DIALOGUE_TYPEWRITE = 0.05f;
        private const float DIALOGUE_TYPEWRITE_PADDING = 16.00f;

        [Header("Settings")]

        [SerializeField]
        private Queue<String> dialogues;

        [Space(10)]

        [SerializeField]
        private float displayTime;

        [SerializeField]
        private float displayTimeTotal;

        [SerializeField]
        private float delayTime;

        [SerializeField]
        private float delayTimeTotal;

        [Space(10)]

        [SerializeField]
        private string dialogue;

        [SerializeField]
        private string dialogueWithTypewrite;

        [SerializeField]
        private int index;

        [SerializeField]
        private float typewriteTime;

        [SerializeField]
        private float typewriteTimeTotal;

        [Space(10)]

        [SerializeField]
        private RectTransform dialogueContainer;

        [SerializeField]
        private RectTransform dialogueContainerBackground;

        [SerializeField]
        private Text dialogueText;

        private void Awake()
        {
            AwakeAgent();
        }

        private void AwakeAgent()
        {
            Agent = GetComponent<Agent>();
        }

        private void Update()
        {
            UpdateDialogue();
        }

        private void UpdateDialogue()
        {
            if (dialogue == null)
            {
                if (delayTime < delayTimeTotal)
                {
                    delayTime += Time.deltaTime;

                    return;
                }
            }

            if (dialogueContainer != null)
            {
                dialogueContainer.transform.localScale = transform.localScale * 0.0625f;
            }

            UpdateDialogueSelect();
            UpdateDialogueSelectDisplay();
            UpdateDialogueSelectDisplayTypewrite();
        }

        private void UpdateDialogueSelect()
        {
            if (dialogue != null)
            {
                return;
            }

            if (dialogues != null)
            {
                if (dialogues.Count > 0)
                {
                    dialogue = dialogues.Dequeue();

                    if (dialogue != null)
                    {
                        displayTime = 0.0f;
                        displayTimeTotal = (dialogue.Length * AgentDialogue.DIALOGUE_TIME_PER_CHARACTER) + AgentDialogue.DIALOGUE_TIME_PER_CHARACTER_BASIS;

                        delayTime = 0.0f;
                        delayTimeTotal = AgentDialogue.DIALOGUE_DELAY;

                        typewriteTime = 0.0f;
                        typewriteTimeTotal = AgentDialogue.DIALOGUE_TYPEWRITE;

                        index = 0;
                    }
                }
            }
        }

        private void UpdateDialogueSelectDisplay()
        {
            if (dialogue == null)
            {
                return;
            }

            displayTime += Time.deltaTime;

            if (displayTime >= displayTimeTotal)
            {
                if (dialogueContainer != null)
                {
                    dialogueContainer.IsEnabled(false);
                }

                if (dialogueContainerBackground != null)
                {
                    dialogueContainerBackground.IsEnabled(false);
                }

                if (dialogueText != null)
                {
                    dialogueText.text = null;
                }

                dialogue = null;
                dialogueWithTypewrite = null;
            }
        }

        private void UpdateDialogueSelectDisplayTypewrite()
        {
            if (dialogue == null)
            {
                return;
            }

            typewriteTime += Time.deltaTime;

            if (typewriteTime >= typewriteTimeTotal)
            {
                if (dialogue != null)
                {
                    if (dialogue.Length > index)
                    {
                        dialogueWithTypewrite = dialogueWithTypewrite + dialogue[index];
                    }
                }

                index++;

                if (dialogueContainer != null)
                {
                    dialogueContainer.IsEnabled(true);
                }

                if (dialogueContainerBackground != null)
                {
                    float textSizeDelta = AgentDialogue.DIALOGUE_TYPEWRITE_PADDING;

                    foreach (char c in dialogueWithTypewrite)
                    {
                        if (dialogueText.font.GetCharacterInfo(c, out CharacterInfo characterInfo, dialogueText.fontSize))
                        {
                            textSizeDelta += characterInfo.advance;
                        }
                    }

                    dialogueContainerBackground.sizeDelta = new Vector2(textSizeDelta, dialogueContainerBackground.sizeDelta.y);
                    dialogueContainerBackground.IsEnabled(true);
                }

                if (dialogueText != null)
                {
                    dialogueText.text = dialogueWithTypewrite;
                }

                typewriteTime -= typewriteTimeTotal;
            }
        }

        public void NewDialogue()
        {
            NewDialogue("...");
        }

        public void NewDialogue(string text)
        {
            if (dialogues == null)
            {
                dialogues = new Queue<String>();
            }

            if (dialogues != null)
            {
                dialogues.Enqueue(text);
            }
        }

        public Agent Agent { get; private set; }

        public override string ToString() => $"AgentDialogue ()";
    }
}
