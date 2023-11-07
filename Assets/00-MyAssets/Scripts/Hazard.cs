using UnityEngine;
using System.Collections;

public class HazardDamage : MonoBehaviour
{
    public int damagePerSecond = 5;

    private Coroutine damageCoroutine; // A reference to the running coroutine

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            damageCoroutine = StartCoroutine(DamagePlayerOverTime(other.gameObject)); // Start and store the coroutine
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine); // Stop the referenced coroutine
            damageCoroutine = null; // Clear the reference
        }
    }

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
}