using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElementUiEnermyStage : MonoBehaviour
{
    [SerializeField] private Button btnEnermy;

    [SerializeField] private Image imgRender;

    [SerializeField] private Image imgGroup;

    private TypeEquip typeEquip;

    // Start is called before the first frame update
    void Start()
    {
        btnEnermy.onClick.AddListener(OnClickBtn);
    }

    public void Init(TypeEquip _typeEquip)
    {
        typeEquip = _typeEquip;

        imgGroup.sprite = GameManager.Instance.DataManager.DataManagerMainGame.DataSprite.GetSpriteGroup(typeEquip.TypeGroup);

        imgRender.sprite = GameManager.Instance.DataManager.DataManagerMainGame.DataSprite.GetSpriteIconEnermy(typeEquip.TypeGroup, typeEquip.TypeTier, typeEquip.TypeId);
    }

    private void OnClickBtn()
    {
        LobbyManager.Instance.SetTypeEquipProfileEnermy(typeEquip);

        LobbyManager.Instance.UiLobbyManager.PopupLobbyManager.ShowPopup(TypePopupLobby.ProfileEnermyPopup, true);
    }
}
