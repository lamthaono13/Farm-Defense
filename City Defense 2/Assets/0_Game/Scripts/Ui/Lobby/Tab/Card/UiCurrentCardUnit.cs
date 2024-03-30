using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using AssetKits.ParticleImage;

public class UiCurrentCardUnit : UiCardUnit
{
    [SerializeField] private TypeSlotEquip typeSlotEquip;

    [SerializeField] private Button btnProfile;

    [SerializeField] private ParticleImage particleImage;

    protected override void Start()
    {
        base.Start();

        if(particleImage != null)
        {
            particleImage.gameObject.SetActive(false);
        }

        btnProfile.onClick.AddListener(OnClickBtnProfile);

        GameManager.Instance.DataManager.OnChangeSlotEquip += OnChangeSlotEquip;

        GameManager.Instance.DataManager.OnChangeStar += Init;

        GameManager.Instance.DataManager.OnChangeLevelAlly += Init;

        GameManager.Instance.DataManager.OnDoneUnlockLv3 += OnDoneTutorialLv3;

        if (!GameManager.Instance.DataManager.GetHasTutorialLobbyLv3() && typeSlotEquip == TypeSlotEquip.Slot3 && !GameManager.Instance.IsHack)
        {
            if (!GameManager.Instance.DataManager.GetDoneTutEquidLv3())
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
        else
        {
            gameObject.SetActive(true);
        }

        Init();
    }

    private void OnDestroy()
    {
        GameManager.Instance.DataManager.OnChangeSlotEquip -= OnChangeSlotEquip;

        GameManager.Instance.DataManager.OnChangeStar -= Init;

        GameManager.Instance.DataManager.OnChangeLevelAlly -= Init;

        GameManager.Instance.DataManager.OnDoneUnlockLv3 -= OnDoneTutorialLv3;
    }

    public void OnChangeSlotEquip(TypeSlotEquip _typeSlotEquip, TypeGroup _typeGroup, TypeTier _typeTier, TypeId _typeId)
    {
        if(_typeSlotEquip != typeSlotEquip)
        {
            return;
        }

        Init(GameManager.Instance.DataManager.GetDataCard(typeSlotEquip));
    }

    protected override void OnClickBtnCard()
    {
        base.OnClickBtnCard();

        //GameManager.Instance.SoundManager.PlaySoundButton();

        LobbyManager.Instance.SetCurrentSwap(typeSlotEquip);

        LobbyManager.Instance.UiLobbyManager.PopupLobbyManager.ShowPopup(TypePopupLobby.SwapPopup, true);
    }

    public void Init()
    {
        if (GameManager.Instance.DataManager.GetUnlockBtnSwap())
        {
            btnCard.gameObject.SetActive(true);
        }
        else
        {
            btnCard.gameObject.SetActive(false);
        }

        Init(GameManager.Instance.DataManager.GetDataCard(typeSlotEquip));
    }

    public void OnClickBtnProfile()
    {
        GameManager.Instance.SoundManager.PlaySoundButton();

        LobbyManager.Instance.SetTypeEquipProfile(_dataCard.TypeGroup, _dataCard.TypeTier, _dataCard.TypeId);

        LobbyManager.Instance.UiLobbyManager.PopupLobbyManager.ShowPopup(TypePopupLobby.ProfileAllyPopup, true);
    }

    public void OnDoneTutorialLv3()
    {
        if(typeSlotEquip == TypeSlotEquip.Slot3)
        {
            gameObject.transform.localScale = Vector3.zero;

            gameObject.SetActive(true);

            gameObject.transform.DOScale(1, 0.2f).SetUpdate(true).OnComplete(() =>
            {
                if (particleImage != null)
                {
                    particleImage.gameObject.SetActive(true);

                    particleImage.Play();
                }
            });
        }
    }
}
