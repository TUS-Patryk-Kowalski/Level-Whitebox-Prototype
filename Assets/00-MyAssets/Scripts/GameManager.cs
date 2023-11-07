using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool hasFlashlight;
    public Flashlight flashlight;
    public TextMeshProUGUI useFlashlight;

    private void Awake()
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

    private void FixedUpdate()
    {
        if (hasFlashlight)
        {
            useFlashlight.text = "F for Flashlight";
        }
    }
    private void Update()
    {
        if (hasFlashlight && Keyboard.current.fKey.wasPressedThisFrame)
        {
            flashlight.Toggle();
        }
    }
}
