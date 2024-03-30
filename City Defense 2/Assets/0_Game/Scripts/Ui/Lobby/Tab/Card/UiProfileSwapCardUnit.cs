using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiProfileSwapCardUnit : UiCardUnit
{
    [SerializeField] private Image imgCantChoose;

    [SerializeField] private Image imgSwap;

    [SerializeField] private TypeSlotEquip typeSlotEquip;

    protected override void Start()
    {
        base.Start();

    }

    protected override void OnClickBtnCard()
    {
        base.OnClickBtnCard();

        GameManager.Instance.SoundManager.PlaySoundButton();

        TypeEquip typeEquip = LobbyManager.Instance.GetTypeEquipProfile();

        GameManager.Instance.DataManager.SetEquipAlly(typeSlotEquip, typeEquip.TypeGroup, typeEquip.TypeTier, typeEquip.TypeId);

        int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        bool a = GameManager.Instance.DataManager.GetHasTutorialLobbyLv3();

        if (levelUnlock == 3 && !a)
        {
            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUnlockUnit();
        }

        LobbyManager.Instance.UiLobbyManager.PopupLobbyManager.ShowPopup(TypePopupLobby.ProfileAllyPopup, false);
    }

    public override void Init(DataCard dataCard, bool canChoose)
    {
        base.Init(dataCard, canChoose);

        imgCantChoose.gameObject.SetActive(!canChoose);

        imgSwap.gameObject.SetActive(canChoose);

        if (canChoose)
        {
            btnCard.enabled = true;

            imgRender.color = new Color(255, 255, 255, 255);
        }
        else
        {
            btnCard.enabled = false;

            imgRender.color = new Color(103, 103, 103, 255);
        }
    }
}
