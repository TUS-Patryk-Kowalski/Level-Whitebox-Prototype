using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour // Make it a MonoBehaviour if it's going to be attached to a GameObject
{
    public Light flashlightLight;
    public AudioSource flashlightSound;

    public void Toggle()
    {
        flashlightLight.enabled = !flashlightLight.enabled;
        flashlightSound.Play();
    }
}
