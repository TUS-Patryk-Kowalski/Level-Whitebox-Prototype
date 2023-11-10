using UnityEngine;
using System.Collections;
using StarterAssets;

public class HazardDamage : MonoBehaviour
{
    public int damagePerSecond = 5;

    public bool modifyPlayerSpeed;
    public float updatedPlayerMoveSpeed;
    public float updatedPlayerSprintSpeed;

    private Coroutine damageCoroutine;

    private FirstPersonController playerController;

    private float originalWalk;
    private float originalSprint;

    //---------------------------------------------------
    // UNITY FUNCTIONS
    //---------------------------------------------------

    private void OnTriggerEnter(Collider other)
    {
        if(playerController == null)
        {
            playerController = other.GetComponent<FirstPersonController>();
        }

        if (other.CompareTag("Player"))
        {
            originalWalk = playerController.MoveSpeed;
            originalSprint = playerController.SprintSpeed;

            StartCoroutine(other);

            // Update the player's movement speeds if the hazard is meant to modify them
            if (modifyPlayerSpeed)
            {
                playerController.ModifyPlayerSpeeds(updatedPlayerMoveSpeed, updatedPlayerSprintSpeed);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && damageCoroutine != null)
        {
            playerController.ModifyPlayerSpeeds(originalWalk, originalSprint);
            StopCoroutine();
        }
    }

    //---------------------------------------------------
    // PRIVATE FUNCTIONS
    //---------------------------------------------------

    IEnumerator DamagePlayerOverTime(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        // As long as the player health exists
        while (playerHealth != null && playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(damagePerSecond);
            yield return new WaitForSeconds(1); // Wait for 1 second before dealing damage again
        }
    }

    private void StartCoroutine(Collider info)
    {
        damageCoroutine = StartCoroutine(DamagePlayerOverTime(info.gameObject));
    }

    private void StopCoroutine()
    {
        StopCoroutine(damageCoroutine); // Stop the referenced coroutine
        damageCoroutine = null; // Clear the reference
    }
}