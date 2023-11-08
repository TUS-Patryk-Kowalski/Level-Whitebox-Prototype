using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightPickup : MonoBehaviour
{
    public GameManager gameManager;

    private bool isPlayerInTrigger = false;
    public GameObject voicelineSource;
    private AudioSource playerVoiceline;
    public AudioSource pickup;

    private float time;

    //---------------------------------------------------
    // UNITY FUNCTIONS
    //---------------------------------------------------

    private void Start()
    {
        GrabStartingReferences();
    }

    private void Update()
    {
        HandleFlashlightPickup();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is tagged as Player
        if (other.CompareTag("Player"))
        {
            SetPlayerCollisionStatus(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider is tagged as Player
        if (other.CompareTag("Player"))
        {
            SetPlayerCollisionStatus(false);
        }
    }

    //---------------------------------------------------
    // PRIVATE FUNCTIONS
    //---------------------------------------------------

    private void GrabStartingReferences()
    {
        gameManager = GameManager.instance;
        playerVoiceline = voicelineSource.GetComponent<AudioSource>();
    }

    private void HandleFlashlightPickup()
    {
        // Check if the player is within the trigger collider and if the 'E' key is pressed
        if (isPlayerInTrigger && Keyboard.current.eKey.wasPressedThisFrame && !gameManager.hasFlashlight)
        {
            HasFlashlight(true);

            pickup.Play();
        }

        // Destroy the flashlight object once the player character stops talking
        if (gameManager.hasFlashlight)
        {
            time = time + Time.deltaTime;

            if (playerVoiceline.clip != null && time >= playerVoiceline.clip.length)
            {
                // Destroy flashlight GameObject or destroy it if it should disappear after pickup
                Destroy(gameObject);
            }
        }
    }

    private void HasFlashlight(bool state)
    {
        gameManager.hasFlashlight = state;
    }

    private void SetPlayerCollisionStatus(bool status)
    {
        isPlayerInTrigger = status;
    }
}
