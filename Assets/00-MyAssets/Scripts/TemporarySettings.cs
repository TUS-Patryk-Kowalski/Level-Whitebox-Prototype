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
    // UNITY FUNCTIONS
    //---------------------------------------------------

    private void Awake()
    {
        SetFramerate(maxFrameRate);
    }

    private void Update()
    {
        skyMaterial.SetFloat("_Rotation", rotation = rotation + rotationMultiplier * Time.deltaTime);
    }

    //---------------------------------------------------
    // PUBLIC FUNCTIONS
    //---------------------------------------------------

    public void SetFramerate(int targetFrameRate)
    {
        if (targetFrameRate == 0)
        {
            targetFrameRate = 60;
        }
        Application.targetFrameRate = targetFrameRate;
    }
}
