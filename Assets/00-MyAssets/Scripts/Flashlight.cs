using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    // Flashlight variables
    public Light flashlightLight;
    public AudioSource flashlightSound;

    //---------------------------------------------------
    // CUSTOM FUNCTIONS
    //---------------------------------------------------
    public void Toggle()
    {
        flashlightLight.enabled = !flashlightLight.enabled;
        flashlightSound.Play();
    }
}
