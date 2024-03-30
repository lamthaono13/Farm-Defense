using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSwapCardUnit : UiCardUnit
{
    [SerializeField] private Image imgCantChoose;

    protected override void Start()
    {
        base.Start();

    }

    protected override void OnClickBtnCard()
    {
        base.OnClickBtnCard();

        GameManager.Instance.SoundManager.PlaySoundButton();

        GameManager.Instance.DataManager.SetEquipAlly(LobbyManager.Instance.GetCurrentSwap(), _dataCard.TypeGroup, _dataCard.TypeTier, _dataCard.TypeId);

        LobbyManager.Instance.UiLobbyManager.PopupLobbyManager.ShowPopup(TypePopupLobby.SwapPopup, false);
    }

    public override void Init(DataCard dataCard, bool canChoose)
    {
        base.Init(dataCard, canChoose);

        imgCantChoose.gameObject.SetActive(!canChoose);

        if (canChoose)
        {
            imgRender.color = new Color(255, 255, 255, 255);
        }
        else
        {
            imgRender.color = new Color(103, 103, 103, 255);
        }
    }
}
