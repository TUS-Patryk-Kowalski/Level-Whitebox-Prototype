using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonData : MonoBehaviour
{
    [Header("Leave as null to load a level instead of switching the Menu")]
    public AudioClip buttonSound;
    public int sceneIndex, startingPointID, newScreenID;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GameObject.FindWithTag("MainCamera").GetComponentInChildren<AudioSource>();
        if (audioSource == null)
            audioSource = GameObject.FindWithTag("MainCamera").AddComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if (buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }
    }

    public void SetNewScreenID()
    {
        CanvasInfo.currentScreenID = newScreenID;
    }

    public void LoadNewScene()
    {
        PlayerPrefs.SetInt("SelectedLevel", sceneIndex);
        PlayerPrefs.SetInt("SelectedStartingPoint", startingPointID);
        SceneManager.LoadScene(sceneIndex);
    }
}
