using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    private GameData gameData;
    public static GameStates Instance { get; private set; }

    public event EventHandler onLoose;
    public event EventHandler onWin;

    public GameObject goal; 
    public GameObject player; 
    public GameObject gameOverUI; 
    public GameObject victoryUI; 
    public int finishReward = 100; 

    private bool hasWon = false;
    private bool hasLost = false;

    private int temporaryCoins = 0;  

    public MaterialSelection.MaterialData currentMaterial;


    [SerializeField] private string newSortingLayer = "NewLayer1";
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject levelHandler;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {        
        gameData = SaveSystem.Load();
        if (gameData == null)
        {
            gameData = new GameData();
        }
        
        if (gameOverUI != null)
            gameOverUI.SetActive(false);
        if (victoryUI != null)
            victoryUI.SetActive(false);
       
        if (MaterialSelection.Instance != null)
        {
            SetCurrentMaterial(MaterialSelection.Instance.SelectedMaterial);
        }
    }

    public void SetCurrentMaterial(MaterialSelection.MaterialType materialType)
    {
        currentMaterial.materialType = materialType;
    }

    void Update()
    {
        if (!hasWon && !hasLost)
        {           
            if (Vector2.Distance(player.transform.position, goal.transform.position) < 1.0f)
            {
                HandleWin();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (!hasWon && collision.gameObject == goal)
        {
            HandleWin();
        }
        
        if (collision.CompareTag("Obstacle") || collision.CompareTag("UnbrecableObstacle"))
        {
            HandleLoss();
        }
    }

    public void AddTemporaryCoins(int amount)
    {       
        temporaryCoins += amount;
    }

    private void HandleWin()
    {
        onWin?.Invoke(this, EventArgs.Empty);
        hasWon = true;
        
        if (player.TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
        
        if (gameData == null)
        {
            gameData = SaveSystem.Load();
            if (gameData == null)
            {
                gameData = new GameData();
            }
        }
        
        gameData.totalCoins += temporaryCoins + finishReward;
        
        if (currentMaterial.materialType == MaterialSelection.MaterialType.Bricks)
        {
            gameData.totalBricks += 1;
        }
        else if (currentMaterial.materialType == MaterialSelection.MaterialType.Wood)
        {
            gameData.totaledWood += 1;
        }
        else if (currentMaterial.materialType == MaterialSelection.MaterialType.Concrete)
        {
            gameData.totalConcrete += 1;
        }
        gameData.isTutorialEnd = true;
        SaveSystem.Save(gameData); 
       
        if (victoryUI != null)
        {
            victoryUI.SetActive(true);
            levelHandler.SetActive(false);
        }
    }

    private void HandleLoss()
    {
        onLoose?.Invoke(this, EventArgs.Empty);
        hasLost = true;
       
        if (player.TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
       
        temporaryCoins = 0;

        if (gameOverUI != null)
            gameOverUI.SetActive(true);


        if (mainUI != null)
        {        
            var renderer = mainUI.GetComponent<Renderer>();
            if (renderer != null)
            {                
                renderer.sortingLayerName = newSortingLayer; 
            }
            else
            {                
                var canvas = mainUI.GetComponent<Canvas>();
                if (canvas != null)
                {                   
                    canvas.sortingLayerName = newSortingLayer; 
                }
            }
        }
    }

    public int GetTemporaryCoins()
    {
        return temporaryCoins;
    }  
    public string GetCollectedMaterialsInfo()
    {
        string info;

        if (currentMaterial.materialType == MaterialSelection.MaterialType.Empty)
        {
            info = "Materials 0";
        }
        else
        {
            info = "Collected materials: +1";
        }
        return info;
    }
}
