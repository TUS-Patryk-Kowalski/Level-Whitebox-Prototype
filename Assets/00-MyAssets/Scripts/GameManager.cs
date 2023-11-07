using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //Game manager reference
    public static GameManager instance;

    //Flashlight variables
    public bool hasFlashlight;
    public Flashlight flashlight;
    public TextMeshProUGUI useFlashlight;

    //---------------------------------------------------
    // CORE UNITY FUNCTIONS
    //---------------------------------------------------

    private void Awake()
    {
        CreateSingletonPattern();
    }

    private void FixedUpdate()
    {
        FlashlightUseUIText();
    }

    private void Update()
    {
        ToggleFlashlight();
    }

    //---------------------------------------------------
    // CUSTOM FUNCTIONS
    //---------------------------------------------------

    private void CreateSingletonPattern()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void FlashlightUseUIText()
    {
        if (hasFlashlight)
        {
            useFlashlight.text = "F for Flashlight";
        }
    }

    private void ToggleFlashlight()
    {
        if (hasFlashlight && Keyboard.current.fKey.wasPressedThisFrame)
        {
            flashlight.Toggle();
        }
    }
}
