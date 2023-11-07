using UnityEngine;
using UnityEngine.InputSystem; // Using the new Unity Input System

public class FlashlightPickup : MonoBehaviour
{
    public GameManager gameManager; // Reference to the GameManager which holds the flashlight status

    private bool isPlayerInTrigger = false;
    public GameObject voicelineSource;
    private AudioSource playerVoiceline;
    public AudioSource pickup;

    private float time;

    private void Start()
    {
        gameManager = GameManager.instance;
        playerVoiceline = voicelineSource.GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Check if the player is within the trigger collider and if the 'E' key is pressed
        if (isPlayerInTrigger && Keyboard.current.eKey.wasPressedThisFrame && !gameManager.hasFlashlight)
        {
            PickupFlashlight();

            pickup.Play();
        }

        if (gameManager.hasFlashlight)
        {
            time = time + Time.deltaTime;

            if (time >= playerVoiceline.clip.length)
            {
                // Destry flashlight GameObject or destroy it if it should disappear after pickup
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is tagged as Player
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider is tagged as Player
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    private void PickupFlashlight()
    {
        // Set hasFlashlight to true to indicate that the flashlight has been picked up
        gameManager.hasFlashlight = true;

        // You can add more functionality here, such as playing a pickup sound or animation
        
    }
}
