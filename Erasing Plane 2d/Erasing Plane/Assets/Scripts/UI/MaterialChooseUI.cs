using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialChooseUI : MonoBehaviour
{
    public static MaterialChooseUI Instance { get; private set; }

    public event EventHandler onClick;

    [SerializeField] private TextMeshProUGUI coinValue;
    [SerializeField] private Button NotEnoughMoneyButton;
    [SerializeField] private GameObject NotEnoughMoneyUI;

    [SerializeField] private GameObject[] Arrows;

    [SerializeField] private Button EmptyButton;
    [SerializeField] private Button BricksButton;
    [SerializeField] private Button ConcreteButton;
    [SerializeField] private Button WoodButton;

    private void Awake()
    {
        Instance = this;

        EmptyButton.onClick.AddListener(() =>
        {           
            onClick?.Invoke(this, EventArgs.Empty);
            ActivateArrow(0);            
        });

        BricksButton.onClick.AddListener(() =>
        {
            onClick?.Invoke(this, EventArgs.Empty);
            ActivateArrow(1);
        });

        ConcreteButton.onClick.AddListener(() =>
        {
            onClick?.Invoke(this, EventArgs.Empty);
            ActivateArrow(2);
        });

        WoodButton.onClick.AddListener(() =>
        {
            onClick?.Invoke(this, EventArgs.Empty);
            ActivateArrow(3);
        });
    }

    private void Start()
    {
        NotEnoughMoneyUI.SetActive(false);
        NotEnoughMoneyButton.onClick.AddListener(() => OnNotEnoughMoneyButtonClick());

        MaterialSelection.Instance.OnPlayerNotEnoughMoney += OnPlayerNotEnoughMoney;
    }

    private void OnPlayerNotEnoughMoney(object sender, System.EventArgs e)
    {
        NotEnoughMoneyUI.SetActive(true);
    }

    private void Update()
    {
        GameData data = SaveSystem.Load();
        coinValue.text = "Your coin: " + data.totalCoins.ToString();
    }

    private void OnNotEnoughMoneyButtonClick()
    {
        NotEnoughMoneyUI.SetActive(false);
    }

    private void ActivateArrow(int arrowIndex)
    {        

        for (int i = 0; i < Arrows.Length; i++)
        {
            if(i == arrowIndex)
            {
                Arrows[i].gameObject.SetActive(true);
            }
            else
            {
                Arrows[i].gameObject.SetActive(false);
            }
        }
    }
}
