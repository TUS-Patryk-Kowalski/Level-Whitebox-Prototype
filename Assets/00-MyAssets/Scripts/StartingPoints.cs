using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SPData
{
    public Transform startingPointTransform;
    public int startingPointLevel;
    public int startingPointID;
}

public class StartingPoints : MonoBehaviour
{
    public SPData[] startingPointArray;
}
