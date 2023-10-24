using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;  // Needed for IEnumerator

[Serializable]
public class LevelDetail
{
    public float transitionSpeed = 1f;
    public GameObject nextLevelGO;
    public LevelCollisionChecker nextLevel;
    public GameObject previousLevelGO;
    public Transform playerSpawn;
    public int setLevelTo;
}

public class LevelManager : MonoBehaviour
{
    public RawImage sceneTransition;  // The shared RawImage for all levels
    public int currentLevel = 0;
    public LevelDetail[] levelDetails;

    private GameObject playerGO;

    // Cooldown mechanism variables
    private bool canTransition = true;
    private float cooldownTime = 2.0f; // 2 seconds as an example

    private void Start()
    {
        playerGO = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        LevelDetail detail = levelDetails[currentLevel];

        if (!detail.nextLevel.inTrigger)
        {
            // Make UI Image transparent over time
            sceneTransition.color = new Color(sceneTransition.color.r, sceneTransition.color.g, sceneTransition.color.b, Mathf.Clamp01(sceneTransition.color.a - detail.transitionSpeed * Time.deltaTime));
        }
        else if (canTransition)  // Only proceed with the transition if cooldown allows it
        {
            // Make UI Image opaque over time
            sceneTransition.color = new Color(sceneTransition.color.r, sceneTransition.color.g, sceneTransition.color.b, Mathf.Clamp01(sceneTransition.color.a + detail.transitionSpeed * Time.deltaTime));

            // Load next level, Unload current level
            if (sceneTransition.color.a >= 1.0f)
            {
                playerGO.SetActive(false);
                playerGO.transform.position = detail.playerSpawn.localPosition;
                playerGO.SetActive(true);

                currentLevel = detail.setLevelTo;
                detail.previousLevelGO.SetActive(false);
                detail.nextLevelGO.SetActive(true);

                detail.nextLevel.inTrigger = false;

                // Start the transition cooldown
                canTransition = false;
                StartCoroutine(TransitionCooldown());
            }
        }
    }

    // Cooldown coroutine
    IEnumerator TransitionCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        canTransition = true;
    }
}