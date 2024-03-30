using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiProfileCardUnit : UiCardUnit
{
    [SerializeField] private GameObject objLock;

    [SerializeField] private Color color;

    [SerializeField] private Image imgBgLock;

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnClickBtnCard()
    {
        base.OnClickBtnCard();

        GameManager.Instance.SoundManager.PlaySoundButton();

        LobbyManager.Instance.SetTypeEquipProfile(_dataCard.TypeGroup, _dataCard.TypeTier, _dataCard.TypeId);

        LobbyManager.Instance.UiLobbyManager.PopupLobbyManager.ShowPopup(TypePopupLobby.ProfileAllyPopup, true);

        int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        bool a = GameManager.Instance.DataManager.GetHasTutorialLobbyLv3();

        if (levelUnlock == 3 && !a)
        {
            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUnlockUnit();
        }

        bool b = GameManager.Instance.DataManager.GetHasTutorialLobbyLv6();

        if (levelUnlock == 6 && !b)
        {
            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUpgradeUnit();
        }

        if (levelUnlock < 6 && levelUnlock > 3 && GameManager.Instance.DataManager.GetIsTutorialUpgradeLobby() && !GameManager.Instance.DataManager.GetHasTutorialUpgrade())
        {
            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUpgradeLose();
        }
    }

    protected override void Init(DataCard dataCard)
    {
        base.Init(dataCard);

        bool a = GameManager.Instance.DataManager.GetUnlockAlly(dataCard.TypeGroup, dataCard.TypeTier, dataCard.TypeId);



        bool b = GameManager.Instance.DataManager.GetHasTutorialLobbyLv3();

        if (b)
        {
            Debug.Log("Khangggggg");

            imgBgLock.color = color;
        }
        else
        {
            imgBgLock.color = new Color(0, 0, 0, 0);
        }

        objLock.gameObject.SetActive(!a);

        if (a)
        {
            imgRender.color = new Color(255, 255, 255, 255);
        }
        else
        {
            imgRender.color = new Color(103, 103, 103, 255);
        }
    }
}
