using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenUI : MonoBehaviour
{
   public static WinScreenUI Instance { get; private set; }

    public event EventHandler onClick;

    [SerializeField] private TextMeshProUGUI coinAmount;
    [SerializeField] private TextMeshProUGUI materialsAmount;

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button nextLevelButton;

    private void Awake()
    {
        Instance = this;

        mainMenuButton.onClick.AddListener(() =>
        {
            onClick?.Invoke(this, EventArgs.Empty);
            GameOverUI.Instance.GoToMainMenu();
        });

        nextLevelButton.onClick.AddListener(() =>
        {
            onClick?.Invoke(this, EventArgs.Empty);
            GameOverUI.Instance.RestartLevel();
            Debug.Log(GameStates.Instance.GetCollectedMaterialsInfo().ToString());
        });
    }
    private void Update()
    {
        coinAmount.text = GameStates.Instance.GetTemporaryCoins().ToString();
        materialsAmount.text = GameStates.Instance.GetCollectedMaterialsInfo().ToString();
        
    }
}
