using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporarySettings : MonoBehaviour
{
    public int maxFrameRate;

    public Material skyMaterial;
    public float rotationMultiplier;
    private float rotation;
    void Awake()
    {
        if (maxFrameRate == 0)
        {
            maxFrameRate = 120;
        }
        Application.targetFrameRate = maxFrameRate;
    }

    private void Update()
    {
        skyMaterial.SetFloat("_Rotation", rotation = rotation + rotationMultiplier * Time.deltaTime);
    }
}
