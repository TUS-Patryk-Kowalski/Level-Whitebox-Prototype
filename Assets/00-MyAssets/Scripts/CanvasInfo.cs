using UnityEngine;

public class CanvasInfo : MonoBehaviour
{
    public GameObject[] screens;
    public static int currentScreenID;

    private void Start()
    {
        ScreenDisplayUpdate();
    }

    public void ScreenDisplayUpdate()
    {
        screens[currentScreenID].SetActive(true);

        for (int i = 0; i < screens.Length; i++)
        {
            if (i != currentScreenID)
            {
                screens[i].SetActive(false); // Disable all other screens
            }
        }
    }
}
