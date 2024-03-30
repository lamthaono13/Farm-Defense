using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupProfileEnermy : UiCanvas
{
    private TypeEquip typeEquip;

    [SerializeField] private TextMeshProUGUI textName;

    [SerializeField] private TextMeshProUGUI textDescrition;

    private GameObject objLoad;

    public override void Show(bool _isShow)
    {
        base.Show(_isShow);

        if (_isShow)
        {
            Init();
        }
        else
        {
            if(objLoad != null)
            {
                Destroy(objLoad);
            }
        }
    }

    public override void Init()
    {
        base.Init();

        typeEquip = LobbyManager.Instance.GetTypeEquipProfileEnermy();

        ConfigBaseIndex configBaseIndex = GameManager.Instance.DataManager.DataManagerMainGame.GetConfigBaseIndexEnermy(typeEquip.TypeGroup, typeEquip.TypeTier, typeEquip.TypeId);

        textName.text = configBaseIndex.dataConfigForTypeCharBase.Name;

        textDescrition.text = configBaseIndex.dataConfigForTypeCharBase.Description;

        string stringLoad = "Ui/EnermyUi/" + typeEquip.TypeGroup.ToString() + "/" + typeEquip.TypeTier.ToString() + " " + typeEquip.TypeId.ToString();

        var a = Resources.Load<GameObject>(stringLoad);

        objLoad = Instantiate(a, transform);
    }
}
