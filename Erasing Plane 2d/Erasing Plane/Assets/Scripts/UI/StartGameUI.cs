using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartGameUI : MonoBehaviour
{
    GameData gameData;
    
    [SerializeField] private TextMeshProUGUI coinValue;
    [SerializeField] private TextMeshProUGUI bricksValue;
    [SerializeField] private TextMeshProUGUI woodValue;
    [SerializeField] private TextMeshProUGUI concreteValue;

   
    
    private void Update()
    {
        gameData = SaveSystem.Load();
        UpdateUI();
    }

    private void UpdateUI()
    {
        coinValue.text = gameData.totalCoins.ToString();
        bricksValue.text = gameData.totalBricks.ToString();
        woodValue.text = gameData.totaledWood.ToString();
        concreteValue.text = gameData.totalConcrete.ToString();
    }
}
