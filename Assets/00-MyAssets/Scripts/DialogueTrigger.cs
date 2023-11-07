using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class DialogueTrigger : MonoBehaviour
{
    // Dialogue variables
    public AudioClip[] voiceLines;
    public bool requiresInput;
    private AudioSource audioSource;
    private int currentLine = 0;
    private bool playerInTrigger = false;
    private GameObject inputText;

    //-----------------------------------------------------------------------------------
    // CORE UNITY FUNCTIONS
    //-----------------------------------------------------------------------------------

    private void Awake()
    {
        CreateAudioSource();
    }

    private void Start()
    {
        SetCollidersAsTriggers();

        if (requiresInput)
            GetInputText();

        InputTextDisplay(); // Most of the text displays will need to be disabled once the game starts
    }

    private void Update()
    {
        HandleDialogue();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetPlayerCollisionStateTo(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetPlayerCollisionStateTo(false);
        }
    }

    //-----------------------------------------------------------------------------------
    // CUSTOM FUNCTIONS
    //-----------------------------------------------------------------------------------

    private void CreateAudioSource()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void SetPlayerCollisionStateTo(bool newState)
    {
        playerInTrigger = newState;
        InputTextDisplay(); // Recheck if the text should be displayed
    }

    private void PlayNextVoiceLine()
    {
        if (voiceLines.Length > 0 && currentLine < voiceLines.Length)
        {
            audioSource.clip = voiceLines[currentLine];
            audioSource.Play();
            if (currentLine < voiceLines.Length - 1)
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
        else if (inputText)
        {
            inputText.SetActive(false);
        }
    }

    private void SetCollidersAsTriggers()
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
    }

    private void GetInputText()
    {
        // The text mesh component should always be on the first child object of this script's object
        inputText = transform.GetChild(0).gameObject;
    }

    private void HandleDialogue()
    {
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
}
