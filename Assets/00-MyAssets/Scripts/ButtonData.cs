using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonData : MonoBehaviour
{
    public GameObject currentView;

    [Header("Leave this variable as null to make the button load a level instead of switching the Menu")]
    public GameObject nextView;

    public AudioSource audioSource;
    public AudioClip buttonSound;

    public int sceneIndex, startingPointID;

    private void Start()
    {
        // SUGGESTION: Try replacing GetChild with something else
        audioSource = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void SwitchViews()
    {
        EnableNextView(currentView, nextView, sceneIndex, startingPointID);
    }

    private void EnableNextView(GameObject currentView, GameObject nextView, int nextScene, int startingPointID)
    {
        if(nextView == null)
        {
            PlayerPrefs.SetInt("SelectedLevel", nextScene);
            PlayerPrefs.SetInt("SelectedStartingPoint", startingPointID);

            SceneManager.LoadScene(nextScene);
        }
        else
        {
            nextView.SetActive(true);
            currentView.SetActive(false);
        }
    }
}
