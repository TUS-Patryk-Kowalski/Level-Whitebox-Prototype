using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporarySettings : MonoBehaviour
{
    public int maxFrameRate;

    public Material skyMaterial;
    public float rotationMultiplier;
    private float rotation;

    //---------------------------------------------------
    // CORE UNITY FUNCTIONS
    //---------------------------------------------------

    void Awake()
    {
        SetFramerate(maxFrameRate);
    }

    private void Update()
    {
        skyMaterial.SetFloat("_Rotation", rotation = rotation + rotationMultiplier * Time.deltaTime);
    }

    //---------------------------------------------------
    // CUSTOM FUNCTIONS
    //---------------------------------------------------

    private void SetFramerate(int targetFrameRate)
    {
        if (targetFrameRate == 0)
        {
            targetFrameRate = 60;
        }
        Application.targetFrameRate = targetFrameRate;
    }
}
