using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class LevelDetail
{
    public float transitionSpeed = 1f;
    public GameObject nextLevelGO;
    public LevelCollisionChecker nextLevel;
    public GameObject previousLevelGO;
    public Transform playerSpawn;
}

public class LevelManager : MonoBehaviour
{
    public RawImage sceneTransition;  // The shared RawImage for all levels
    public int currentLevel = 0;
    public LevelDetail[] levelDetails;

    private GameObject playerGO;

    private void Start()
    {
        playerGO = GameObject.FindWithTag("Player");
        UpdateActiveLevel();
    }

    private void Update()
    {
        LevelDetail detail = levelDetails[currentLevel];

        if (!detail.nextLevel.inTrigger)
        {
            // Make UI Image transparent over time
            sceneTransition.color = new Color(sceneTransition.color.r, sceneTransition.color.g, sceneTransition.color.b, Mathf.Clamp01(sceneTransition.color.a - detail.transitionSpeed * Time.deltaTime));
        }
        else
        {
            // Make UI Image opaque over time
            sceneTransition.color = new Color(sceneTransition.color.r, sceneTransition.color.g, sceneTransition.color.b, Mathf.Clamp01(sceneTransition.color.a + detail.transitionSpeed * Time.deltaTime));

            // Load next level, Unload current level
            if (sceneTransition.color.a >= 1.0f)
            {
                playerGO.SetActive(false);
                playerGO.transform.position = detail.playerSpawn.position;
                playerGO.SetActive(true);

                detail.previousLevelGO.SetActive(false);
                detail.nextLevelGO.SetActive(true);
                
                currentLevel++;
                UpdateActiveLevel();
            }
        }
    }

    void UpdateActiveLevel()
    {
        // Ensure currentLevel stays within bounds
        currentLevel = Mathf.Clamp(currentLevel, 0, levelDetails.Length - 1);
    }
}