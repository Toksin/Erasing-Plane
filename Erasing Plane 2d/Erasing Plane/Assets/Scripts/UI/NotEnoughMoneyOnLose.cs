using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NotEnoughMoneyOnLose : MonoBehaviour
{
    public static NotEnoughMoneyOnLose Instance { get; private set; }

    public event EventHandler onClick;

    [SerializeField] private Button backToMainMenuButton;

    private void Awake()
    {
        Instance = this;

        backToMainMenuButton.onClick.AddListener(() =>
        {
            onClick?.Invoke(this, EventArgs.Empty);
            Loader.Load(Loader.Scene.Menu);
        });
    }
}
