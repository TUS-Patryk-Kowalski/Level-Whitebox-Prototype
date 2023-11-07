using UnityEngine;
using System.Collections;

public class HazardDamage : MonoBehaviour
{
    public int damagePerSecond = 5;

    private Coroutine damageCoroutine;

    //---------------------------------------------------
    // CORE UNITY FUNCTIONS
    //---------------------------------------------------

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && damageCoroutine != null)
        {
            StopCoroutine();
        }
    }

    //---------------------------------------------------
    // CUSTOM FUNCTIONS
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