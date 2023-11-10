using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;  // Needed for IEnumerator

[Serializable]
public class LevelDetail
{
    public string description;
    public int levelNumber;
    public float transitionSpeed = 1f;
    public GameObject nextLevelGO;
    public LevelCollisionChecker nextLevel;
    public GameObject currentLevelGO;
    public Transform playerSpawn;
    public int setLevelTo;
}

public class LevelManager : MonoBehaviour
{
    public RawImage sceneTransition;
    public int currentLevel;
    public int StartingID;
    public LevelDetail[] levelDetails;

    private GameObject playerGO;

    //---------------------------------------------------
    // UNITY FUNCTIONS
    //---------------------------------------------------

    private void Start()
    {
        GrabStartingReferences();
        EnableAndDisableLevelObjects();
        // Get the player's starting level (set by map screen) and Spawn them there
        StartingTeleport();
    }

    private void Update()
    {
        ScreenFadeWithTeleport();
    }

    //---------------------------------------------------
    // PUBLIC FUNCTIONS
    //---------------------------------------------------

    public void Teleport(Transform newPosition)
    {
        playerGO.SetActive(false);
        playerGO.transform.position = newPosition.position;
        playerGO.transform.rotation = newPosition.rotation;
        playerGO.SetActive(true);
    }

    //---------------------------------------------------
    // PRIVATE FUNCTIONS
    //---------------------------------------------------

    private void StartingTeleport()
    {
        currentLevel = PlayerPrefs.GetInt("SelectedLevel");
        StartingID = PlayerPrefs.GetInt("SelectedStartingPoint");

        StartingPoints startingPoints = levelDetails[currentLevel].currentLevelGO.GetComponent<StartingPoints>();

        foreach (SPData startData in startingPoints.startingPointArray)
        {
            if (currentLevel != startData.startingPointLevel || StartingID != startData.startingPointID)
            {
                continue;
            }
            else if(currentLevel == startData.startingPointLevel && StartingID == startData.startingPointID)
            {
                Debug.Log($"Moving Player to Level {startData.startingPointLevel} at start {startData.startingPointID}");
                Teleport(startData.startingPointTransform);
                break; // Found correct point, remain points don't need to be checked
            }
            else
            {
                Debug.Log("No points were found, defaulting to the first point!");
                StartingID = 1;
                Teleport(startData.startingPointTransform);
            }
        }
    }

    private void GrabStartingReferences()
    {
        playerGO = GameObject.FindWithTag("Player");
    }

    private void EnableAndDisableLevelObjects()
    {
        foreach (LevelDetail levelDetail in levelDetails)
        {
            if (currentLevel != levelDetail.levelNumber)
            {
                levelDetail.currentLevelGO.SetActive(false);
            }
        }
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
        // Move player to next level area
        playerGO.SetActive(false);
        playerGO.transform.position = detailsForTeleport.playerSpawn.localPosition;
        playerGO.SetActive(true);

        // Set the level number to the next level's ID
        currentLevel = detailsForTeleport.setLevelTo;

        // Disable current level, enable nect level
        detailsForTeleport.currentLevelGO.SetActive(false);
        detailsForTeleport.nextLevelGO.SetActive(true);

        // Player got teleported out of trigger, so it doesn't detect the player has exited, "manually" set the inTrigger state
        detailsForTeleport.nextLevel.inTrigger = false;
    }
}