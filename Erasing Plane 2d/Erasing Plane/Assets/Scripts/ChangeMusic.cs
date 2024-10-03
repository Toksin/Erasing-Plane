using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMusic : MonoBehaviour
{
    public static ChangeMusic Instance { get; private set; }

    public TMP_Dropdown musicDropdown; 
    private GameData gameData;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        musicDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        gameData = SaveSystem.Load();
        UpdateMusicDropdown();
    }
    public void OnDropdownValueChanged(int index)
    {
        ChangeMusicTrack(index);
    }    
    public void UpdateMusicDropdown()
    {
        musicDropdown.options.Clear();      

        for (int i = 0; i < MusicManager.Instance.musicRefsSO.music.Length; i++)
        {
            if (gameData.musicUnlocked[i])
            {
                musicDropdown.options.Add(new TMP_Dropdown.OptionData(MusicManager.Instance.musicRefsSO.music[i].name));               
                Debug.Log($"Добавлен трек: {MusicManager.Instance.musicRefsSO.music[i].name}");
            }
            else
            {
                Debug.Log($"Трек {i} не разблокирован.");
            }
        } 
        int currentTrackIndex = MusicManager.Instance.GetCurrentTrackIndex();
        if (currentTrackIndex < musicDropdown.options.Count)
        {
            musicDropdown.value = currentTrackIndex;
        }
        else
        {
           
            musicDropdown.value = 0; 
        }
        musicDropdown.RefreshShownValue();    
    }   

    public void ChangeMusicTrack(int trackIndex)
    {       
        MusicManager.Instance.PlayMusicTrack(trackIndex);
    }
}
