using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiLobbyManager : MonoBehaviour
{
    [SerializeField] private UiTabLobbyManager uiTabLobbyManager;

    [SerializeField] private PopupLobbyManager popupLobbyManager;

    [SerializeField] private UiTutorialLobby uiTutorialLobby;

    [SerializeField] private UiCoin uiCoin;

    [SerializeField] private UiGem uiGem;

    [SerializeField] private UiTicket uiTicket;

    public UiTabLobbyManager UiTabLobbyManager => uiTabLobbyManager;

    public PopupLobbyManager PopupLobbyManager => popupLobbyManager;

    public UiTutorialLobby UiTutorialLobby => uiTutorialLobby;

    public UiCoin UiCoin => uiCoin;

    public UiGem UiGem => uiGem;

    public UiTicket UiTicket => uiTicket;

    private void Start()
    {
        //btnShop.onClick.AddListener(OnClickBtnShop);
    }

    private void OnClickBtnShop()
    {
        // UiShop uiShop = GameManager.Instance.GetUiShop(TypeCamera.Lobby);
        //
        // uiShop.Show(true);
    }

    public void Init()
    {
        uiTabLobbyManager.Init();
    }
}
