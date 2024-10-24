using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMenu : MonoBehaviour
{
    public static ChangeMenu Instance { get; private set; }
    public event EventHandler onClick;

    [SerializeField] private GameObject[] objects;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button islandButton;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        menuButton.onClick.AddListener(() =>
        {
            ActivateObject(0);           
        });

        shopButton.onClick.AddListener(() =>
        {
            ActivateObject(1);
        });

        islandButton.onClick.AddListener(() =>
        {
            ActivateObject(2);
        });
    }

    public void ActivateObject(int index)
    {        
        for (int i = 0; i < objects.Length; i++)
        {            
            objects[i].SetActive(i == index);
            onClick?.Invoke(this, EventArgs.Empty);
        }
    }


}
