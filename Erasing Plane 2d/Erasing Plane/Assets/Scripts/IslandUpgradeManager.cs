using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IslandUpgradeManager : MonoBehaviour
{    
    public static IslandUpgradeManager Instance;
    public event EventHandler onClick;
    private GameData gameData;

    
    [SerializeField] private TextMeshProUGUI islandLevelText;  
    [SerializeField] private TextMeshProUGUI upgradeCostText;   
    [SerializeField] private Button upgradeButton;
    [SerializeField] private GameObject neiUI;
    [SerializeField] private Animator incomeAnimator; 
    [SerializeField] private TextMeshProUGUI incomeAnimatedText;  

   
    private int passiveIncome = 1;           
    private float incomeInterval = 5.0f;    


    private void Awake()
    {
        Instance = this;
        FortuneWheel.Instance.OnWheelTurn += FortuneWheel_OnWheelTurn;
    }

    private void FortuneWheel_OnWheelTurn(object sender, System.EventArgs e)
    {
        gameData = SaveSystem.Load();
    }

    private void Update()
    {       
        gameData = SaveSystem.Load();
    }

    private void Start()
    {     
       
        if (gameData == null)
        {
            gameData = new GameData();
        }
        
        UpdateUI();
        StartCoroutine(GeneratePassiveIncome());
        upgradeButton.onClick.AddListener(() => {

            onClick?.Invoke(this, EventArgs.Empty);
            UpgradeIsland();
        });
    }

    private void UpdateUI()
    {        
        islandLevelText.text = "Island level: " + gameData.islandLevel;
        upgradeCostText.text = "Need to upgrade:\n" +
            ": " + gameData.requiredBricks + "\n" +
            ": " + gameData.requiredWood + "\n" +
            ": " + gameData.requiredConcrete;
    }

    private IEnumerator GeneratePassiveIncome()
    {
        while (true)
        {
            yield return new WaitForSeconds(incomeInterval);
            
            gameData.totalCoins += passiveIncome;

            SaveSystem.Save(gameData);             
            ShowIncomeText(passiveIncome);
        }
    }

    private void ShowIncomeText(int income)
    {
        incomeAnimatedText.text = "+ " + income;
        incomeAnimator.SetTrigger("PlayIncome");  
        Debug.Log("Пассивный доход: " + income);
    }

    public void UpgradeIsland()
    {        
        if (gameData.totalBricks >= gameData.requiredBricks &&
            gameData.totaledWood >= gameData.requiredWood &&
            gameData.totalConcrete >= gameData.requiredConcrete)
        {           
            gameData.totalBricks -= gameData.requiredBricks;
            gameData.totaledWood -= gameData.requiredWood;
            gameData.totalConcrete -= gameData.requiredConcrete;
            
            gameData.islandLevel++;
            passiveIncome += 1;  
            gameData.requiredBricks += 5;  
            gameData.requiredWood += 3;
            gameData.requiredConcrete += 2;

            
            SaveSystem.Save(gameData);
            UpdateUI();              
        }
        else
        {
            neiUI.SetActive(true);            
        }
    }

}
