using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotEnoughResoursesUI : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject neiUI;

    private void Awake()
    {
        backButton.onClick.AddListener(() => {
            
            neiUI.SetActive(false);

        });
    }


}
