using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiSetting : UiCanvas
{
    [SerializeField] private Button btnClose;

    [SerializeField] private Button btnHome;

    [SerializeField] private Button btnReplay;

    [SerializeField] private List<ElementUiSetting> elementUiSettings;

    protected override void Start()
    {
        base.Start();

        btnClose.onClick.AddListener(OnClickBtnClose);

        int currentLevel = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        if(btnHome != null)
        {
            if (currentLevel < 3)
            {
                btnHome.gameObject.SetActive(false);
            }

            btnHome.onClick.AddListener(OnClickBtnHome);
        }

        if(btnReplay != null)
        {
            if (currentLevel < 3)
            {
                btnReplay.gameObject.SetActive(false);
            }

            btnReplay.onClick.AddListener(OnClickBtnReplay);
        }
    }

    public override void Show(bool _isShow)
    {
        base.Show(_isShow);

        if (_isShow)
        {
            foreach(var item in elementUiSettings)
            {
                item.Init();
            }
        }

        if (LevelManagerMainGame.Instance != null)
        {
            LevelManagerMainGame.Instance.OnFreezeGame(_isShow);
        }
    }

    public override void Init()
    {
        base.Init();
    }

    public void OnClickBtnClose()
    {
        Show(false);
    }

    private void OnClickBtnHome()
    {
        btnHome.enabled = false;

        GameManager.Instance.Loading(TypeLoading.LoadingToLobby, () => GameManager.Instance.LoadLobby("LoadingLevelInititalGame"), "LoadingInititalGame");
    }

    private void OnClickBtnReplay()
    {
        //AdsManager.Instance.ShowInterstitial();

        btnReplay.enabled = false;

        GameManager.Instance.Loading(TypeLoading.LoadingToInGame, () => GameManager.Instance.LoadLevel("LoadingLevelInititalGame"), "LoadingInititalGame");
    }
}