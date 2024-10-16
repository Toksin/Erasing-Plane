using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    public static BackgroundChanger instance;

    public GameObject[] backgrounds;

    [SerializeField] private Color[] backgroundColors;
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {        
        int savedIndex = SaveSystem.Load().activeBackgroundIndex;
        SetActiveBackground(savedIndex >= 0 ? savedIndex : 0); 
    }

    public void SetActiveBackground(int index)
    {
        if (index < 0 || index >= backgrounds.Length)
        {           
            return;
        }
        
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(i == index); 
        }
       
    }
}
