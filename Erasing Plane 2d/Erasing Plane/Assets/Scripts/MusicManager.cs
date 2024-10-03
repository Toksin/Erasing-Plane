using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager Instance { get; private set; }
    
    private AudioSource audioSource;
    private float volume;
    public MusicRefsSO musicRefsSO;
    private int currentTrackIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusicTrack(int trackIndex)
    {
        if (trackIndex < 0 || trackIndex >= musicRefsSO.music.Length)
        {           
            return;
        }       

        currentTrackIndex = trackIndex;
        audioSource.clip = musicRefsSO.music[trackIndex];
        audioSource.Play();
        
        AudioClip selectedClip = musicRefsSO.music[trackIndex];        
    }

    public int GetCurrentTrackIndex()
    {
        return currentTrackIndex;
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return volume;
    }
}
