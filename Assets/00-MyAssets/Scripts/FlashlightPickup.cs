using UnityEngine;
using UnityEngine.InputSystem; // Using the new Unity Input System

public class FlashlightPickup : MonoBehaviour
{
    public GameManager gameManager; // Reference to the GameManager which holds the flashlight status

    private bool isPlayerInTrigger = false;


    private void Start()
    {
        gameManager = GameManager.instance;
    }
    private void Update()
    {
        // Check if the player is within the trigger collider and if the 'E' key is pressed
        if (isPlayerInTrigger && Keyboard.current.eKey.wasPressedThisFrame)
        {
            PickupFlashlight();
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

        // Destry flashlight GameObject or destroy it if it should disappear after pickup
        Destroy(gameObject);
    }
}
