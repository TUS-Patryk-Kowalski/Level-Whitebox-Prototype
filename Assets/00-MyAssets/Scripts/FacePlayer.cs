using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        // Set transform X to be negative to have the text face the player properly
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void FixedUpdate()
    {
        if(target == null && GameManager.instance.playerGO != null)
        {
            target = GameManager.instance.playerGO.transform.parent.GetComponentInChildren<Camera>().gameObject.transform;
        }
    }

    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);
        }
    }
}
