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
    public RawImage sceneTransition;
    public int currentLevel = 0;
    public LevelDetail[] levelDetails;

    private GameObject playerGO;

    //---------------------------------------------------
    // UNITY FUNCTIONS
    //---------------------------------------------------

    private void Start()
    {
        GrabStartingReferences();
    }

    private void Update()
    {
        ScreenFadeWithTeleport();
    }

    //---------------------------------------------------
    // PRIVATE FUNCTIONS
    //---------------------------------------------------

    private void GrabStartingReferences()
    {
        playerGO = GameObject.FindWithTag("Player");
    }
    private void ScreenFadeWithTeleport()
    {
        LevelDetail detail = levelDetails[currentLevel];

        if (!detail.nextLevel.inTrigger)
        {
            sceneTransition.color = new Color(sceneTransition.color.r, sceneTransition.color.g, sceneTransition.color.b, Mathf.Clamp01(sceneTransition.color.a - detail.transitionSpeed * Time.deltaTime));
        }
        else
        {
            sceneTransition.color = new Color(sceneTransition.color.r, sceneTransition.color.g, sceneTransition.color.b, Mathf.Clamp01(sceneTransition.color.a + detail.transitionSpeed * Time.deltaTime));

            if (sceneTransition.color.a >= 1.0f)
            {
                Teleport(detail);
            }
        }
    }

    private void Teleport(LevelDetail detailsForTeleport)
    {
        playerGO.SetActive(false);
        playerGO.transform.position = detailsForTeleport.playerSpawn.localPosition;
        playerGO.SetActive(true);

        currentLevel = detailsForTeleport.setLevelTo;
        detailsForTeleport.previousLevelGO.SetActive(false);
        detailsForTeleport.nextLevelGO.SetActive(true);

        detailsForTeleport.nextLevel.inTrigger = false;
    }
}