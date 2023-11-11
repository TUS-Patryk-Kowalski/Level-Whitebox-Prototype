using UnityEngine;
using UnityEngine.Audio; // For audio management

public class Settings : MonoBehaviour
{
    // Graphics settings
    public int resolutionWidth = 1920;
    public int resolutionHeight = 1080;
    public bool isFullScreen = true;
    public int textureQuality = 0; // Lower number for higher quality
    public int antiAliasing = 0; // Options might be 0, 2, 4, 8 etc.

    // Audio settings
    public AudioMixer audioMixer; // Assign in inspector
    public float masterVolume = 1.0f;
    public float musicVolume = 1.0f;
    public float sfxVolume = 1.0f;

    // Control settings
    public float mouseSensitivity = 1.0f;
    public bool invertYAxis = false;

    // Example method to change resolution
    public void SetResolution(int width, int height, bool fullScreen)
    {
        Screen.SetResolution(width, height, fullScreen);
        GameObject.FindWithTag("UI_Core");
        // Set scale factor of UI to adjust for resolution, UI is scaled to 1920x1080 by default
    }

    // Example method to change master volume
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
}