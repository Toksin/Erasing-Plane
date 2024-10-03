using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    public static bool isTutorialEnd = false;

    [SerializeField] private GameObject TotorialSwipeHandStart;
    [SerializeField] private GameObject TotorialSwipeHandShop;
    [SerializeField] private GameObject TotorialSwipeLeftHandIsland;
    [SerializeField] private GameObject TotorialSwipeLeftHandShop;
    [SerializeField] private GameObject TotorialClickHandMain;

    [SerializeField] private GameObject TotorialSwipeDownHandShop;
    [SerializeField] private GameObject TotorialClickHandShop;
    [SerializeField] private GameObject TotorialClickHandShopSecond;
    [SerializeField] private GameObject TotorialClickHandIsland;
    [SerializeField] private GameObject TutorialUI;

    [SerializeField] private Button ButtonForTutorialShopSkin;
    [SerializeField] private Button ButtonForTutorialShopBackground;
    [SerializeField] private Button ButtonForTutorialIsland;
    [SerializeField] private Button ButtonForEndTutorialMenu;

    private GameData gameData;   

    private void Awake()
    {
        Instance = this;
        gameData = SaveSystem.Load();
    }

    private void OnEnable()
    { 
        gameData = SaveSystem.Load();
       
    }

    void Start()
    {
        Debug.Log("Current TutInfo- " + gameData.isTutorialEnd);

        // Работаем напрямую с gameData.isTutorialEnd
        if (gameData.isTutorialEnd)
        {
            EndTutorial();
        }
        else
        {
            // Подписываемся на события, если туториал еще не завершен
            ButtonForTutorialShopSkin.onClick.AddListener(TutorialOnSkinClick);
            ButtonForTutorialShopBackground.onClick.AddListener(TutorialShopBackgroundClick);
            ButtonForTutorialIsland.onClick.AddListener(TutorialIslandClick);
            ButtonForEndTutorialMenu.onClick.AddListener(EndTutorial);
        }
    }

    private void Update()
    {
        if (isTutorialEnd)
        {
            gameData.isTutorialEnd = true;
            gameData = SaveSystem.Load();
        }
    }

    private void EndTutorial()
    {       
        DisableTutorial();       
        TutorialUI.SetActive(false);      
        
    }

    private void DisableTutorial()
    {
        TutorialUI.SetActive(false);
        TotorialSwipeLeftHandIsland.SetActive(false);
        TotorialSwipeLeftHandShop.SetActive(false);
        TotorialClickHandMain.SetActive(false);
        TotorialSwipeHandStart.SetActive(false);
        TotorialSwipeDownHandShop.SetActive(false);
        TotorialClickHandShop.SetActive(false);
        TotorialClickHandShopSecond.SetActive(false);
        TotorialClickHandIsland.SetActive(false);
        TotorialSwipeHandShop.SetActive(false);
        ButtonForTutorialShopSkin.gameObject.SetActive(false);
        ButtonForTutorialShopBackground.gameObject.SetActive(false);
        ButtonForTutorialIsland.gameObject.SetActive(false);
    }

    private void TutorialOnSkinClick()
    {
        TotorialSwipeHandStart.SetActive(false);
        TotorialClickHandShop.SetActive(false);
        TotorialSwipeDownHandShop.SetActive(true);
        ButtonForTutorialShopSkin.gameObject.SetActive(false);
        TotorialClickHandShopSecond.SetActive(true);
    }

    private void TutorialShopBackgroundClick()
    {
        TotorialClickHandShopSecond.SetActive(false);
        ButtonForTutorialShopBackground.gameObject.SetActive(false);
        TotorialSwipeDownHandShop.SetActive(false);
        TotorialClickHandIsland.SetActive(true);
        TotorialSwipeHandShop.SetActive(true);
    }

    private void TutorialIslandClick()
    {
        TotorialClickHandIsland.SetActive(false);
        ButtonForTutorialIsland.gameObject.SetActive(false);
        TotorialSwipeHandShop.SetActive(false);
        TotorialSwipeLeftHandIsland.SetActive(true);
        TotorialSwipeLeftHandShop.SetActive(true);
        TotorialClickHandMain.SetActive(true);
    }

}
