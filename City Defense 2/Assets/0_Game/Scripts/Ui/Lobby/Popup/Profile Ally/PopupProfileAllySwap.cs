using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupProfileAllySwap : UiCanvas
{
    [SerializeField] private List<UiCardUnit> uiCardUnits;

    public override void Show(bool _isShow)
    {
        base.Show(_isShow);

        if (_isShow)
        {
            Init();
        }
    }

    public void Init(TypeEquip typeEquip)
    {
        for(int i = 0; i < uiCardUnits.Count; i++)
        {
            DataCard dataCard = GameManager.Instance.DataManager.GetDataCard((TypeSlotEquip)i);

            bool a = LobbyManager.Instance.CheckCanSwap(typeEquip.TypeGroup, typeEquip.TypeTier, typeEquip.TypeId, (TypeSlotEquip)i);

            uiCardUnits[i].Init(dataCard, a);
        }
    }
}
