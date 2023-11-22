using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject UICore;

    // Graphics settings
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    public TMP_InputField framerateInputField;

    public int resolutionWidth = 1920;
    public int resolutionHeight = 1080;
    public bool isFullScreen = true;

    // Audio settings
    public AudioMixer audioMixer;

    public Slider masterVolumeSlider;
    public Slider dialogueVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider ambienceVolumeSlider;

    public float masterVolume = 1.0f;
    public float dialogueVolume = 1.0f;
    public float musicVolume = 1.0f;
    public float sfxVolume = 1.0f;
    public float ambienceVolume = 1.0f;

    public float mouseSensitivity = 1.0f;
    public bool invertYAxis = false;

    private float scaleFactor;

    private void Start()
    {
        UICore = GameObject.FindWithTag("UI_Core");

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        framerateInputField.text = Application.targetFrameRate.ToString();

        UpdateUIResolution();
    }

    public void SetResolution()
    {
        Screen.SetResolution(resolutionWidth, resolutionHeight, isFullScreen);
        UpdateUIResolution();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        resolutionWidth = resolution.width;
        resolutionHeight = resolution.height;

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        UpdateUIResolution();
    }

    private void UpdateUIResolution()
    {
        if (resolutionHeight <= 0) resolutionHeight = 1080;

        scaleFactor = resolutionHeight / 1080f;
        UICore.GetComponent<CanvasScaler>().scaleFactor = scaleFactor;
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetFramerate()
    {
        int framerate;
        if (int.TryParse(framerateInputField.text, out framerate))
        {
            Application.targetFrameRate = framerate;
        }
        else
        {
            Debug.LogError("Invalid framerate input");
        }
    }
}