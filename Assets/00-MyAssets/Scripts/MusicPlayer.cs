using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] tracks;
    private AudioSource audioSource;
    private int currentTrackIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayNextTrack();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    void PlayNextTrack()
    {
        if (tracks.Length == 0) return;

        audioSource.clip = tracks[currentTrackIndex];
        audioSource.Play();

        currentTrackIndex = (currentTrackIndex + 1) % tracks.Length;
    }
}