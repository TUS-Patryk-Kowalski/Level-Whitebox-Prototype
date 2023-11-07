using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCollisionChecker : MonoBehaviour
{
    public bool inTrigger;

    //---------------------------------------------------
    // CORE UNITY FUNCTIONS
    //---------------------------------------------------

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetTriggerStateTo(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetTriggerStateTo(false);
        }
    }

    //---------------------------------------------------
    // CUSTOM FUNCTIONS
    //---------------------------------------------------

    private void SetTriggerStateTo(bool status)
    {
        inTrigger |= status;
    }
}
