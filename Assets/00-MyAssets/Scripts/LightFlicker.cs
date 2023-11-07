using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float minIntensityMultiplier = 0.2f;
    public float maxIntensityMultiplier = 0.9f;
    public float flickerSpeed = 0.25f;
    public float minPauseTime = 0.3f;
    public float maxPauseTime = 1f;
    public float maxAudioOffset = 12f;

    private Light flickerLight;
    private AudioSource audioSource;
    private float originalIntensity;

    //---------------------------------------------------
    // CORE UNITY FUNCTIONS
    //---------------------------------------------------

    private void Start()
    {
        GrabStartingVariables();
        SetStartingVariables();

        StartCoroutine(FlickerLightRoutine());
    }

    //---------------------------------------------------
    // CUSTOM FUNCTIONS
    //---------------------------------------------------

    private void GrabStartingVariables()
    {
        flickerLight = GetComponentInChildren<Light>();
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private void SetStartingVariables()
    {
        originalIntensity = flickerLight.intensity;
    }

    private IEnumerator FlickerLightRoutine()
    {
        while (true)
        {
            float flickerDuration = Random.Range(0.1f, 1.2f);
            float flickerStartTime = Time.time;

            // Start playing the sound
            if (!audioSource.isPlaying)
            {
                audioSource.time = Random.Range(0f, maxAudioOffset);
                audioSource.Play();
            }

            while (Time.time - flickerStartTime < flickerDuration)
            {
                float randomizer = Random.Range(0f, 1f);
                if (randomizer > flickerSpeed)
                {
                    float minIntensity = originalIntensity * minIntensityMultiplier;
                    float maxIntensity = originalIntensity * maxIntensityMultiplier;

                    flickerLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, randomizer);
                }
                yield return null;
            }

            // Stop playing the sound
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            flickerLight.intensity = originalIntensity;
            float pauseDuration = Random.Range(minPauseTime, maxPauseTime);
            yield return new WaitForSeconds(pauseDuration);
        }
    }
}
