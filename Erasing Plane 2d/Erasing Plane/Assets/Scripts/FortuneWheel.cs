using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FortuneWheel : MonoBehaviour
{
    public event EventHandler OnWheelTurn;
    public static FortuneWheel Instance { get; private set; }

    public event EventHandler onClick;
    private GameData gameData; 
    private int numbersOfTurn;
    private int whatWin;
    private float speed;

    private bool canWeTurn;

    [SerializeField] private Button speenButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject fortuneWheelUI;
    [SerializeField] private GameObject fortuneWheel;
      
    private void Awake()
    {
        Instance = this;
        gameData = SaveSystem.Load();

        speenButton.onClick.AddListener(() =>
        {
            if (canWeTurn && gameData.totalCoins < 50) 
            {
                onClick?.Invoke(this, EventArgs.Empty);
                StartCoroutine(TurnTheWheel());
            }
            else
            {
                fortuneWheelUI.SetActive(false);
            }
        });

        backButton.onClick.AddListener(() =>
        {
            onClick?.Invoke(this, EventArgs.Empty);
            fortuneWheelUI.SetActive(false);
        });
    }

    private void Start()
    {
        canWeTurn = true;
        if (gameData == null) 
        {
            gameData = new GameData();
        }
    }

    private IEnumerator TurnTheWheel()
    {
        canWeTurn = false;

        numbersOfTurn = UnityEngine.Random.Range(5, 10);
        speed = 0.1f;

        for (int i = 0; i < numbersOfTurn; i++)
        {
            fortuneWheel.transform.Rotate(0, 0, 22.5f);

            if (i > Mathf.RoundToInt(numbersOfTurn * 0.5f))
            {
                speed = 0.2f;
            }
            if (i > Mathf.RoundToInt(numbersOfTurn * 0.7f))
            {
                speed = 0.3f;
            }
            if (i > Mathf.RoundToInt(numbersOfTurn * 0.9f))
            {
                speed = 0.4f;
            }

            yield return new WaitForSeconds(speed);
        }
        
        float currentAngle = fortuneWheel.transform.eulerAngles.z;
        
        if (currentAngle >= 0 && currentAngle < 45)
        {
            gameData.totalCoins += 40;          
        }
        else if (currentAngle >= 45 && currentAngle < 90)
        {
            gameData.totalCoins += 60;            
        }
        else if (currentAngle >= 90 && currentAngle < 135)
        {
            gameData.totalCoins += 80;           
        }
        else if (currentAngle >= 135 && currentAngle < 180)
        {
            gameData.totalCoins += 100;            
        }
        else if (currentAngle >= 180 && currentAngle < 225)
        {
            gameData.totalCoins += 120;            
        }
        else if (currentAngle >= 225 && currentAngle < 270)
        {
            gameData.totalCoins += 140;           
        }
        else if (currentAngle >= 270 && currentAngle < 315)
        {
            gameData.totalCoins += 160;            
        }
        else if (currentAngle >= 315 && currentAngle < 360)
        {
            gameData.totalCoins += 200;           
        }
        
        SaveSystem.Save(gameData);
        canWeTurn = true;
        OnWheelTurn?.Invoke(this, EventArgs.Empty);
    }
}
