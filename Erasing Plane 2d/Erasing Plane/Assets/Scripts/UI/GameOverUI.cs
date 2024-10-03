using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    GameData gameData;

    public event EventHandler onClick;

    public static GameOverUI Instance { get; private set; }
    
    public GameObject player;         
    public Vector2 startPosition;     
    public GameObject levelContainer; 

    public GameObject gameOverUI;  
    
    [SerializeField] private GameObject notEnoughtMoneyUI;

    public Button restartButton;    
    public Button mainMenuButton;     

    private void Awake()
    {
        Instance = this;
        
        restartButton.onClick.AddListener(() => {
            onClick?.Invoke(this, EventArgs.Empty);
            RestartLevel();            
        });
        mainMenuButton.onClick.AddListener(() => {
            onClick?.Invoke(this, EventArgs.Empty);
            GoToMainMenu();
        });
    }

    private void Update()
    {
        gameData = SaveSystem.Load();
    }

    public void RestartLevel()
    {       
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
       
        player.transform.position = startPosition;
       
        if (player.TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
       
        if (levelContainer != null)
        {
            Destroy(levelContainer);
        }

        if(gameData.totalCoins < 50)
        {
            notEnoughtMoneyUI.SetActive(true);
        }
        else
        {            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }       
    }    
    public void GoToMainMenu()
    {        
        Loader.Load(Loader.Scene.Menu);
    }
}
