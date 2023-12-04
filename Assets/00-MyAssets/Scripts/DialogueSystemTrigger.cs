using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueSystemTrigger : MonoBehaviour
{
    public DialogueSystem dialogueSystem;

    private void Start()
    {
        dialogueSystem = GetComponent<DialogueSystem>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Keyboard.current.eKey.wasPressedThisFrame)
        {
            dialogueSystem.StartDialogue(dialogueSystem.);
        }
    }
}
