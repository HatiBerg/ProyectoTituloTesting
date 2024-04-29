using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSpeaker : MonoBehaviour
{
    public List<Dialogue> availableDialogues = new List<Dialogue>();
    [SerializeField] private int dialogIndex = 0;
    public int dialogLineIndex = 0;

    void Start()
    {
        dialogIndex = 0;
        dialogLineIndex = 0;
        
        foreach (var dialog in availableDialogues)
        {
            dialog.finalized = false;
            var q = dialog.question;
            if (q != null)
            {
                foreach (var option in q.choices)
                {
                    option.answer.finalized = false;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Talk();
        }
    }

    public void Talk()
    {
        if (dialogIndex <= availableDialogues.Count - 1)
        {
            if (availableDialogues[dialogIndex].unlocked)
            {
                if (availableDialogues[dialogIndex].finalized)
                {
                    if (UpdateDialog())
                    {
                        DialogueManager.instance.UnlockPlayerController(true);
                        DialogueManager.instance.SetDialogue(availableDialogues[dialogIndex], this);
                    }
                    DialogueManager.instance.SetDialogue(availableDialogues[dialogIndex], this);
                    return;
                }
                DialogueManager.instance.UnlockPlayerController(true);
                DialogueManager.instance.SetDialogue(availableDialogues[dialogIndex], this);
            }
            else
            {
                DialogueManager.instance.UnlockPlayerController(false);
            }
        }
        else
        {
            DialogueManager.instance.UnlockPlayerController(false);
        }
    }

    bool UpdateDialog()
    {
        if (!availableDialogues[dialogIndex].reuse)
            if (dialogIndex < availableDialogues.Count -1)
            {
                dialogIndex++;
                return true;
            }
            else
            {
                return false;
            }
        else
        {
            return true;
        }
    }
}
