using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialInGame : MonoBehaviour
{
    public static TutorialInGame Instance { get; private set; }

    GameData gameData;

    [SerializeField] private GameObject TutorialUI;
    [SerializeField] private Button ButtonStart;

    public bool isTutorialEnd = false;

    private void Awake()
    {
        Instance = this;
        gameData = SaveSystem.Load();
    }

    private void Start()
    {     

        if(gameData.isTutorialEnd)
        {
            EndTutorial();
        }
        else
        {
            ButtonStart.onClick.AddListener(() =>
            {

                MaterialStartClick();
                EndTutorial();
            });
        }      
    }

    private void MaterialStartClick()
    {
        TutorialUI.SetActive(false);        
    }

    private void EndTutorial()
    {
        
        gameData.isTutorialEnd = true;        
        SaveSystem.Save(gameData);        
        Debug.Log("Current TutInfo- " + gameData.isTutorialEnd);
        TutorialUI.SetActive(false);
    }
}
