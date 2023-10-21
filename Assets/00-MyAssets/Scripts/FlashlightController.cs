using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;
    public AudioSource flashlightSound;

    private void Update()
    {
        if (Keyboard.current[Key.F].wasPressedThisFrame)
        {
            Toggle();
        }
    }

    private void Toggle()
    {
        flashlight.enabled = !flashlight.enabled;
        flashlightSound.Play();
    }
}
