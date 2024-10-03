using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI Instance { get; private set; }

    public event EventHandler onClick;

    private GameData gameData;
    public Button playButton;
    public Button settingsButton;

    [SerializeField] private GameObject setting;
    [SerializeField] private GameObject fortuneWheelUI;

    private void Awake()
    {       
        Instance = this;       
        playButton.onClick.AddListener(OnPlayButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        onClick?.Invoke(this, EventArgs.Empty);
        StartGame();
    }

    private void OnSettingsButtonClicked()
    {
        onClick?.Invoke(this, EventArgs.Empty);
        ActivateSetting();
    }

    public void StartGame()
    {
        gameData = SaveSystem.Load();
        if (gameData.totalCoins < 50)
        {
            fortuneWheelUI.SetActive(true);
        }
        else
        {
            Loader.Load(Loader.Scene.SampleScene);
        }
    }

    private void ActivateSetting()
    {
        setting.SetActive(true);
    }
}
