using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class DialogueTrigger : MonoBehaviour
{
    public AudioClip[] voiceLines;
    public bool requiresInput;
    private AudioSource audioSource;
    private int currentLine = 0;
    private bool playerInTrigger = false;
    private GameObject inputText;

    private void Start()
    {
        // Ensure the attached Collider is set as a trigger.
        Collider[] colliders = GetComponents<Collider>();
        foreach (var collider in colliders)
        {
            // Check if the collider is a MeshCollider and if it's not set as concave
            MeshCollider meshCollider = collider as MeshCollider;
            if (meshCollider != null && !meshCollider.convex)
            {
                continue; // Skip this collider
            }

            // Set the isTrigger property to true for all other colliders
            collider.isTrigger = true;
        }

        audioSource = gameObject.AddComponent<AudioSource>();

        if(requiresInput)
            inputText = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        InputTextDisplay();

        // Listen for the Dialogue input action
        if (playerInTrigger && !audioSource.isPlaying && Keyboard.current[Key.E].wasPressedThisFrame && requiresInput) // Replace "Key.Dialogue" with the actual key binding you've set in the new Input System.
        {
            PlayNextVoiceLine();

            if (!requiresInput)
                this.enabled = false;
        }
        else if (playerInTrigger && !audioSource.isPlaying && !requiresInput)
        {
            PlayNextVoiceLine();
        }
    }

    private void PlayNextVoiceLine()
    {
        if (voiceLines.Length > 0 && currentLine < voiceLines.Length)
        {
            audioSource.clip = voiceLines[currentLine];
            audioSource.Play();
            if(currentLine < voiceLines.Length - 1)
            {
                currentLine++;
            }
            else
            {
                this.enabled = false;
            }
        }
    }

    private void InputTextDisplay()
    {
        if (playerInTrigger && inputText)
        {
            inputText.SetActive(true);
        }
        else if(inputText)
        {
            inputText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the tag "Player".
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }
}
