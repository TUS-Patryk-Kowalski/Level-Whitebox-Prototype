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

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach (var collider in colliders)
        {
            MeshCollider meshCollider = collider as MeshCollider;
            if (meshCollider != null && !meshCollider.convex)
            {
                continue;
            }
            collider.isTrigger = true;
        }

        if(requiresInput)
            inputText = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        InputTextDisplay();

        if (playerInTrigger && !audioSource.isPlaying && Keyboard.current[Key.E].wasPressedThisFrame && requiresInput)
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
