using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialSelection : MonoBehaviour
{
    [SerializeField] private string newSortingLayer = "Default";
    public static MaterialSelection Instance { get; private set; }

    public event EventHandler OnPlayerNotEnoughMoney;

    public Button startButton;           
    public TextMeshProUGUI coinText;    
        
    public Button bricksButton;
    public Button woodButton;
    public Button concreteButton;
    public Button emptyButton;
    private MaterialType selectedMaterial = MaterialType.Empty; 
    
    public AirplaneController airplaneController;

    [SerializeField] private GameObject MaterialChooseUI;
    [SerializeField] private GameObject mainUI;   
       
    public enum MaterialType
    {
        Empty,     
        Bricks,    
        Wood,      
        Concrete   
    }

    [System.Serializable]
    public struct MaterialData
    {
        public MaterialType materialType;
        public int cost;          
        public float weight;      
    }

    public List<MaterialData> materials = new List<MaterialData>();  

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
       
        startButton.onClick.AddListener(OnStartButtonPressed);
        UpdateCoinText();      
        InitializeMaterialButtons();
        MaterialChooseUI.SetActive(true);
    }

    private void InitializeMaterialButtons()
    {       
        bricksButton.onClick.AddListener(() => SelectMaterial((int)MaterialType.Bricks));
        woodButton.onClick.AddListener(() => SelectMaterial((int)MaterialType.Wood));
        concreteButton.onClick.AddListener(() => SelectMaterial((int)MaterialType.Concrete));
        emptyButton.onClick.AddListener(() => SelectMaterial((int)MaterialType.Empty));
    }

    
    private void OnStartButtonPressed()
    {      
        
        GameStates.Instance.SetCurrentMaterial(selectedMaterial);
        MaterialData chosenMaterial = materials.Find(m => m.materialType == selectedMaterial);
        GameData data = SaveSystem.Load();

        if (data.totalCoins >= chosenMaterial.cost)
        {
            data.totalCoins -= chosenMaterial.cost;  
            UpdateCoinText();

            if (airplaneController != null)
            {
                airplaneController.SetMaterial(chosenMaterial);
            }
            MaterialChooseUI.SetActive(false);
            SaveSystem.Save(data); 

           
            if (mainUI != null)
            {
                var renderer = mainUI.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.sortingOrder = 0;  
                }

                var canvas = mainUI.GetComponent<Canvas>();
                if (canvas != null)
                {
                    canvas.sortingLayerName = newSortingLayer; 
                }
                else if (renderer != null)
                {
                    renderer.sortingLayerName = newSortingLayer;
                }
            }
        }
        else
        {
            OnPlayerNotEnoughMoney?.Invoke(this, EventArgs.Empty);
        }


    }
 
    private void UpdateCoinText()
    {
        GameData data = SaveSystem.Load();

        coinText.text = "Coins: " + data.totalCoins;
    }   
    public void SelectMaterial(int materialIndex)
    {
        selectedMaterial = (MaterialType)materialIndex;        
    }

    public MaterialSelection.MaterialType SelectedMaterial
    {
        get { return selectedMaterial; }
    }


}
