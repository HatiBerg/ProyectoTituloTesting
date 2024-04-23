using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public Dialogue dialogue;
    [SerializeField] private GameObject dialogueContainer;
    [SerializeField] private GameObject questionContainer;
    [SerializeField] private TMP_Text nameCharacter;
    [SerializeField] private TMP_Text dialogueText;

    public int lineIndex = 0;
    private bool isPlayerInRange;
    private bool didDialogueStart;

    void Start()
    {
        dialogueContainer.SetActive(true);
        questionContainer.SetActive(false);
    }

    public void UpdateText(int state)
    {
        dialogueContainer.SetActive(true);
        questionContainer.SetActive(false);
        
        switch (state)
        {
            case 0:
                nameCharacter.text = dialogue.dialogues[lineIndex].character.nameCharacter;
                dialogueText.text = dialogue.dialogues[lineIndex].dialogue;
                break;

            case 1:
                if (lineIndex < dialogue.dialogues.Length - 1)
                {
                    lineIndex++;
                    nameCharacter.text = dialogue.dialogues[lineIndex].character.nameCharacter;
                    dialogueText.text = dialogue.dialogues[lineIndex].dialogue;
                }
                else
                {
                    lineIndex = 0;
                    dialogue.finalized = true;
                    if (dialogue.question != null)
                    {
                        dialogueContainer.SetActive(false);
                        questionContainer.SetActive(true);
                        var ask = dialogue.question;
                        DialogueManager.instance.questionController.activeDialogue(ask.choices.Length, ask.question, ask.choices);
                        return;
                    }
                }
                break;
        }
    }
}
