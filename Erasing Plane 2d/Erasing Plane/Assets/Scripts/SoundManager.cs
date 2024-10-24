using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClipRefsSO AudioClipRefsSO;
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("SoundManager instance created.");
            SceneManager.sceneLoaded += OnSceneLoaded; // Подписка на событие загрузки сцены
        }
        else
        {
            Destroy(gameObject); // Уничтожаем дублирующий объект
            Debug.Log("Duplicate SoundManager destroyed.");
        }
    }

    private void Start()
    {
        SubscribeToEvents();
        UpdateMainCamera();
    }  


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SubscribeToEvents();
        UpdateMainCamera(); // Обновление ссылки на камеру при каждой загрузке новой сцены
    }

    private void UpdateMainCamera()
    {
        mainCamera = Camera.main; // Устанавливаем ссылку на главную камеру
        if (mainCamera == null)
        {
            Debug.LogWarning("Main camera not found!");
        }
    }

    private void SubscribeToEvents()
    {
        if (ShopManager.instance != null)
        {
            ShopManager.instance.onClickUse -= ShopManager_onClickUse;
            ShopManager.instance.onClick -= ShopManager_onClick;
            ShopManager.instance.onClickUse += ShopManager_onClickUse;
            ShopManager.instance.onClick += ShopManager_onClick;

        }

        if (GameOverUI.Instance != null)
            GameOverUI.Instance.onClick += GameOverUI_onClick;

        if (MainMenuUI.Instance != null)
            MainMenuUI.Instance.onClick += mainMenuUI_onClick;

        if (MaterialChooseUI.Instance != null)
            MaterialChooseUI.Instance.onClick += MaterialChooseUI_onClick;

        if (NotEnoughMoneyOnLose.Instance != null)
            NotEnoughMoneyOnLose.Instance.onClick += NotEnoughMoneyOnLose_onClick;

        if (OptionUI.Instance != null)
            OptionUI.Instance.onClick += OptionUI_onClick;

        if (WinScreenUI.Instance != null)
            WinScreenUI.Instance.onClick += WinScreenUI_onClick;

        if (AirplaneController.Instance != null)
            AirplaneController.Instance.onAirplanePush += AirplaneController_onAirplanePush;

        if (FortuneWheel.Instance != null)
            FortuneWheel.Instance.onClick += FortuneWheel_onClick;

        if (GameStates.Instance != null)
        {
            GameStates.Instance.onWin += GameStates_onWin;
            GameStates.Instance.onLoose += GameStates_onLoose;
        }

        if (ObstacleRemoval.Instance != null)
            ObstacleRemoval.Instance.onClick += ObstacleRemoval_onClick;

        if (IslandUpgradeManager.Instance != null)
            IslandUpgradeManager.Instance.onClick += IslandUpgradeManager_onClick;

        if (ChangeMenu.Instance != null)
            ChangeMenu.Instance.onClick += ChangeMenu_onClick;
    }

    private void ChangeMenu_onClick(object sender, System.EventArgs e)
    {
        PlaySound(AudioClipRefsSO.click, mainCamera.transform.position);
    }

    private void ShopManager_onClickUse(object sender, System.EventArgs e)
    {          
      PlaySound(AudioClipRefsSO.useClick, mainCamera.transform.position);            
    }
    private void ShopManager_onClick(object sender, System.EventArgs e)
    {
        PlaySound(AudioClipRefsSO.click, mainCamera.transform.position);
    }

    private void ObstacleRemoval_onClick(object sender, System.EventArgs e)
    {
        PlaySound(AudioClipRefsSO.click, mainCamera.transform.position);
    }

    private void IslandUpgradeManager_onClick(object sender, System.EventArgs e)
    {
        PlaySound(AudioClipRefsSO.click, mainCamera.transform.position);
    }

    private void GameStates_onLoose(object sender, System.EventArgs e)
    {
        PlaySound(AudioClipRefsSO.death, mainCamera.transform.position);
    }

    private void GameStates_onWin(object sender, System.EventArgs e)
    {
        PlaySound(AudioClipRefsSO.win, mainCamera.transform.position);
    }

    private void FortuneWheel_onClick(object sender, System.EventArgs e)
    {
        PlaySound(AudioClipRefsSO.click, mainCamera.transform.position);
    }

    private void AirplaneController_onAirplanePush(object sender, System.EventArgs e)
    {
        PlaySound(AudioClipRefsSO.shootPlayer, mainCamera.transform.position);
    }

    private void WinScreenUI_onClick(object sender, System.EventArgs e)
    {
        PlaySound(AudioClipRefsSO.click, mainCamera.transform.position);
    }

    private void OptionUI_onClick(object sender, System.EventArgs e)
    {
        PlaySound(AudioClipRefsSO.click, mainCamera.transform.position);
    }

    private void NotEnoughMoneyOnLose_onClick(object sender, System.EventArgs e)
    {
        PlaySound(AudioClipRefsSO.click, mainCamera.transform.position);
    }

    private void MaterialChooseUI_onClick(object sender, System.EventArgs e)
    {
        Debug.Log("Click");
        PlaySound(AudioClipRefsSO.click, mainCamera.transform.position);
    }

    private void mainMenuUI_onClick(object sender, System.EventArgs e)
    {


        PlaySound(AudioClipRefsSO.click, mainCamera.transform.position);
    }

    private void GameOverUI_onClick(object sender, System.EventArgs e)
    {
        PlaySound(AudioClipRefsSO.click, mainCamera.transform.position);
    }

    private float volume = 1f;

    private void PlaySound(AudioClip audioClip, Vector2 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector2 position, float volumeMultiplier = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier * volume);
    }
    public void ChangeVolume()
    {
        volume += .1f;
        if(volume > 1f)
        {
            volume = 0f;
        }
    }
    public float GetVolume()
    {
        return volume;
    }

    
}
