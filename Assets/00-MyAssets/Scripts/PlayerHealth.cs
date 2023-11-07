using UnityEngine;
using UnityEngine.UI; // Reference to the UI namespace
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider; // Reference to the Slider UI component
    public TextMeshProUGUI healthNumber;

    public List<AudioClip> audioClips = new List<AudioClip>();
    private int index;

    private AudioSource hurt;

    private Coroutine healthBarCoroutine;

    private void Start()
    {
        hurt = this.AddComponent<AudioSource>();
        hurt.playOnAwake = false;
        currentHealth = maxHealth;
        InitializeHealthUI();
    }

    private void FixedUpdate()
    {
        UpdateHealthUI();
    }

    private void InitializeHealthUI()
    {
        // Set the health slider's max and current value.
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int amount)
    {
        // Select a random index. The second parameter is exclusive, hence audioClips.Count.
        index = Random.Range(0, audioClips.Count);
        hurt.clip = audioClips[index];
        hurt.Play();

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // If there's already a coroutine, stop it before starting a new one
        if (healthBarCoroutine != null)
            StopCoroutine(healthBarCoroutine);

        healthBarCoroutine = StartCoroutine(LerpHealthBar());
    }

    IEnumerator LerpHealthBar()
    {
        float preChangeHealth = healthSlider.value;
        float elapsed = 0f;
        float duration = 1f; // Duration in seconds over which the lerp takes place

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            // Calculate the fraction of the duration that has passed
            float fraction = elapsed / duration;
            // Update the slider value based on the fraction
            healthSlider.value = Mathf.Lerp(preChangeHealth, currentHealth, fraction);
            yield return null;
        }

        // Directly set the health slider value to the current health to avoid any floating-point errors.
        healthSlider.value = currentHealth;
    }

    // Call this method to update the health UI when the player takes damage.
    private void UpdateHealthUI()
    {
        healthSlider.value = currentHealth;
        healthNumber.text = currentHealth.ToString();
    }
}