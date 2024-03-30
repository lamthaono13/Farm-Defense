using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using System;
//using System.Threading.Tasks;
using I2.Loc;
using HG.Rate;
//using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private bool isGameDesign;

    [SerializeField] private bool noTutorial;

    [SerializeField] private bool isHack;

    [SerializeField] private GameObject objWifi;

    [SerializeField] private Camera camera;

    [SerializeField] private SoundManager soundManager;

    [SerializeField] private VibrateManager vibrateManager;

    [SerializeField] private DataManager dataManager;

    [SerializeField] private LoadingManager loadingManager;

    [SerializeField] private UiGlobalManager uiGlobalManager;

    [SerializeField] private RateManager rateManager;

    public bool NoTutorial => noTutorial;

    public bool IsHack => isHack;

    public Camera Camera => camera;

    public SoundManager SoundManager => soundManager;

    public VibrateManager VibrateManager => vibrateManager;

    public DataManager DataManager => dataManager;

    public LoadingManager LoadingManager => loadingManager;

    public UiGlobalManager UiGlobalManager => uiGlobalManager;

    public RateManager RateManager => rateManager;

    private GameFSM gameFSM;

    private float currentTimeScale;

    private bool canSetTimeScale;

    public bool CanSetTimeScale => canSetTimeScale;

    //[SerializeField] private GameObject objNotEnoughGold;

    //[SerializeField] private GameObject objNotEnoughGem;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Application.targetFrameRate = 60;

        canSetTimeScale = true;

        if(Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        gameFSM = new GameFSM(this);

        int levelUnlock = dataManager.GetLevelMaxUnlock();

        if(levelUnlock >= 3)
        {
            Loading(TypeLoading.LoadingInitialGame, () => LoadLobby("LoadingLevelInititalGame"), "LoadingInititalGame");
        }
        else
        {
            Loading(TypeLoading.LoadingInitialGame, () => LoadLevel("LoadingLevelInititalGame"), "LoadingInititalGame");
        }



        // await UniTask.RunOnThreadPool(() =>
        // {
        //     InitAds();
        //     
        //     InitIap();
        //     

        // });

        Invoke("InitAds", 1.5f);

        Invoke("InitIap", 2.5f);

        currentTimeScale = 1;
    }

    public void InitAds()
    {
        AdsManager.Instance.Init();

        //Invoke("ShowBanner", 5);
    }

    private void ShowBanner()
    {
        AdsManager.Instance.ShowBanner();
    }

    private void InitIap()
    {
        HandleIAP.Instance.Initialize();
    }

    private void Start()
    {
        soundManager.Init();

        //Invoke("CheckInternet", 2);

        //Application.lowMemory += OnLowMemory;
    }

    //private void Update()
    //{
    //    if (Input.GetKey(KeyCode.K))
    //    {
    //        I2.Loc.LocalizationManager.CurrentLanguage = "Korean";
    //    }

    //    if (Input.GetKey(KeyCode.J))
    //    {
    //        I2.Loc.LocalizationManager.CurrentLanguage = "Japanese";
    //    }

    //    if (Input.GetKey(KeyCode.E))
    //    {
    //        I2.Loc.LocalizationManager.CurrentLanguage = "English";
    //    }
    //}

    //private void OnLowMemory()
    //{
    //    Resources.UnloadUnusedAssets();
    //}

    public void ChangeState(GameState gameState, string mess)
    {
        gameFSM.ChangeState(gameState);
    }

    // all loading will call in here

    public void Loading(TypeLoading typeLoading,Action actionWhenLoadingDone, string mess)
    {
        loadingManager.OnLoading(typeLoading, actionWhenLoadingDone);

        switch (typeLoading)
        {
            case TypeLoading.NotDeciced:
                break;
            case TypeLoading.LoadingInitialGame:
                break;
            case TypeLoading.LoadingToInGame:
                break;
            case TypeLoading.LoadingToLobby:
                break;
        }

        gameFSM.ChangeState(GameState.LOADING);
    }

    public void LoadScene(int index, TypeLoadScene typeLoadScene, string mess)
    {
        SetTimeScale(1);

        //if (SceneManager.sceneCount > 0)
        //{
        //    SceneManager.UnloadScene(SceneManager.GetActiveScene());
        //}

        Resources.UnloadUnusedAssets();

        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public void LoadLobby(string mess)
    {
        ChangeState(GameState.LOBBY, "");

        if (isGameDesign)
        {
            LoadScene(3, TypeLoadScene.Single, "");

            return;
        }

        LoadScene(1, TypeLoadScene.Single, "");
    }

    public void LoadLevel(string mess)
    {
        ChangeState(GameState.IN_GAME, "");

        LoadScene(2, TypeLoadScene.Single, "");
    }


    [Button]
    public void OnNextLevel(Action action ,string mess)
    {
        ChangeState(GameState.IN_GAME, "");

        dataManager.LevelUp();

        Loading(TypeLoading.LoadingToInGame, () => 
        {
            action?.Invoke();
            LoadLevel("LoadingLevelInititalGame");
        }, "LoadingInititalGame");
    }

    [Button]
    public void OnChooseLevel(int levelChoose)
    {
        ChangeState(GameState.LOBBY, "");

        dataManager.SetLevel(levelChoose);

        dataManager.SetLevelMaxUnlock(levelChoose);

        Loading(TypeLoading.LoadingToLobby, () => LoadLobby("LoadingLevelInititalGame"), "LoadingInititalGame");
    }


    public void OnReload()
    {
        LoadLobby("");
    }

    public void OnLoadingSceneDone(GameState gameState)
    {
        loadingManager.CloseUiHide();

        ChangeState(gameState, "");
    }

    public void OnNotEnoughGold(Vector3 positionNotEnough)
    {

    }

    public void OnNotEnoughGem()
    {

    }

    public void CheckInternet()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            objWifi.SetActive(false);

            SetTimeScale(1);
        }
        else
        {
            objWifi.SetActive(true);

            SetTimeScale(0);
        }

        Invoke("CheckInternet", 2);
    }

    public void SetTimeScale(float _timeScale)
    {
        canSetTimeScale = false;

        currentTimeScale = _timeScale;

        Time.timeScale = _timeScale;

        StartCoroutine(waitTimeScale());
    }

    IEnumerator waitTimeScale()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        canSetTimeScale = true;
    }

    public float GetTimeScale()
    {
        return currentTimeScale;
    }

    [Button]
    public void HackGold(long number)
    {
        dataManager.AddGold(number, "Hack");
    }

    [Button]
    public void HackGem(long number)
    {
        dataManager.AddGem(number, "Hack");
    }

    [Button]
    public void HackTicket(int number)
    {
        dataManager.AddTicket(number, "Hack");
    }

    public void CallRatePoppup()
    {
        rateManager.ShowPopup();
    }

    public void OnDoneTutorialLevel1()
    {
        HandleAppsflyer.Instance.LogEventTutorial("true", "0");

        HandleFireBase.Instance.LogEventWithString("tutorial_choose_ingame_level_1_done");
    }

    public void OnDoneTutorialUnlockLobbyLevel3()
    {
        HandleAppsflyer.Instance.LogEventTutorial("true", "2");

        HandleFireBase.Instance.LogEventWithString("tutorial_Unlock_level_3_done");
    }

    public void OnDoneTutorialUpgrade()
    {
        HandleAppsflyer.Instance.LogEventTutorial("true", "3");

        HandleFireBase.Instance.LogEventWithString("tutorial_Upgrade_lose_level_3_to_5_done");
    }

    public void OnDoneTutorialLevel6()
    {
        HandleAppsflyer.Instance.LogEventTutorial("true", "4");

        HandleFireBase.Instance.LogEventWithString("tutorial_Upgrade_level_6_done");
    }
}

public enum TypeLoadScene
{
    Single,
    Additive
}