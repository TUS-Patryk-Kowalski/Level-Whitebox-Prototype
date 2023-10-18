using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class DialogueTrigger : MonoBehaviour
{
    public AudioClip[] voiceLines;
    private AudioSource audioSource;
    private int currentLine = 0;
    private bool playerInTrigger = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        GetComponent<Collider>().isTrigger = true;  // Ensure the attached Collider is set as a trigger.
    }

    private void Update()
    {
        // Listen for the Dialogue input action
        if (playerInTrigger && !audioSource.isPlaying && Keyboard.current[Key.E].wasPressedThisFrame) // Replace "Key.Dialogue" with the actual key binding you've set in the new Input System.
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
