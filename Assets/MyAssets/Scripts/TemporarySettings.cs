using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporarySettings : MonoBehaviour
{
    public int maxFrameRate;
    void Awake()
    {
        if (maxFrameRate == 0)
        {
            maxFrameRate = 120;
        }
        Application.targetFrameRate = maxFrameRate;
    }
}
