using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance { get; private set; }
    public event EventHandler onClick;
    public event EventHandler onClickUse;

   public Button[] skinButtons;
   public Button[] backgroundButtons;     
   public Button[] musicButtons;
   public Button[] useSkinButtons;
   public Button[] useBackgroundButtons;          

    private GameData gameData;           

    private int[] skinPrices = { 100, 200, 800, 1200, 10000 };          
    private int[] backgroundPrices = { 50, 100, 350, 850, 1000 };    
    private int[] musicPrices = { 120, 220, 320, 420, 520 };         

    private void Awake()
    {        
        instance = this;

        gameData = SaveSystem.Load();
        UpdateShopUI();
     
    }
    void Start()
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            int index = i;
            skinButtons[i].onClick.AddListener(() => 
            {                
                BuySkin(index);               
            });
            useSkinButtons[i].onClick.AddListener(() => 
            {
                
                UseSkin(index);
            });
        }

        for (int i = 0; i < backgroundButtons.Length; i++)
        {
            int index = i;
            backgroundButtons[i].onClick.AddListener(() => 
            {                
                BuyBackground(index);                
            });
            useBackgroundButtons[i].onClick.AddListener(() => 
            {                
                UseBackground(index);
            });
        }

        for (int i = 0; i < musicButtons.Length; i++)
        {
            int index = i;
            musicButtons[i].onClick.AddListener(() => {
                
                BuyMusic(index);
            });
        }
    }
    public void UpdateShopUI()
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            // Если скин не разблокирован и хватает монет, активируем кнопку, иначе деактивируем
            skinButtons[i].gameObject.SetActive(!gameData.skinsUnlocked[i]);
            // Кнопка использования активна только если скин разблокирован
            useSkinButtons[i].gameObject.SetActive(gameData.skinsUnlocked[i]);
        }

        for (int i = 0; i < backgroundButtons.Length; i++)
        {
            // Если фон не разблокирован и хватает монет, активируем кнопку, иначе деактивируем
            backgroundButtons[i].gameObject.SetActive(!gameData.backgroundUnlocked[i]);
            // Кнопка использования фона активна только если фон разблокирован
            useBackgroundButtons[i].gameObject.SetActive(gameData.backgroundUnlocked[i]);
        }

        for (int i = 0; i < musicButtons.Length; i++)
        {
            // Если музыка не разблокирована и хватает монет, активируем кнопку, иначе деактивируем
            musicButtons[i].gameObject.SetActive(!gameData.musicUnlocked[i]);
        }
    }
    public void BuySkin(int index)
    {
        if (index < 0 || index >= skinPrices.Length) return;

        int price = skinPrices[index];

        if (gameData.totalCoins >= price && !gameData.skinsUnlocked[index])
        {
            gameData.totalCoins -= price;
            gameData.skinsUnlocked[index] = true;

            
            SaveSystem.Save(gameData);           
            UpdateShopUI();
            gameObject.SetActive(false);
        }
        Debug.Log("OnClick done");
        onClick?.Invoke(this, EventArgs.Empty);
        Debug.Log(gameData.totalCoins);       
    }
    public void BuyBackground(int index)
    {
        if (index < 0 || index >= backgroundPrices.Length) return;

        int price = backgroundPrices[index];

        if (gameData.totalCoins >= price && !gameData.backgroundUnlocked[index])
        {
            gameData.totalCoins -= price;
            gameData.backgroundUnlocked[index] = true;

          
            SaveSystem.Save(gameData);           
            UpdateShopUI();
            gameObject.SetActive(false);
        }

        onClick?.Invoke(this, EventArgs.Empty);        
    }
    public void BuyMusic(int index)
    {
        if (index < 0 || index >= musicPrices.Length) return;

        int price = musicPrices[index];

        if (gameData.totalCoins >= price && !gameData.musicUnlocked[index])
        {
            gameData.totalCoins -= price;
            gameData.musicUnlocked[index] = true;
           
            SaveSystem.Save(gameData);         
            UpdateShopUI();         
           
        }
        onClick?.Invoke(this, EventArgs.Empty);
        ChangeMusic.Instance.UpdateMusicDropdown();       
    }
    public void UseSkin(int index)
    {
        onClickUse?.Invoke(this, EventArgs.Empty);
        if (index < 0 || index >= gameData.skinsUnlocked.Length) return;

        if (gameData.skinsUnlocked[index])
        {
            if (onClickUse != null) // Проверка на null перед вызовом
            {
                onClickUse.Invoke(this, EventArgs.Empty);
                gameData.activeSkinIndex = index;
                SaveSystem.Save(gameData);
            }
            else
            {
                Debug.Log("onClickUse event is null.");
            }
        }
    }
    public void UseBackground(int index)
    {   
        if (index < 0 || index >= gameData.skinsUnlocked.Length) return;

        if (gameData.backgroundUnlocked[index])
        {
            gameData.activeBackgroundIndex = index;
            SaveSystem.Save(gameData);            
        }
        onClickUse?.Invoke(this, EventArgs.Empty);
    }
}
