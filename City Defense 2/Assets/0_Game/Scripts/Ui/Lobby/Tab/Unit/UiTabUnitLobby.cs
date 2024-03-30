using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTabUnitLobby : UiTabLobby
{
    //[SerializeField] private TabUnitManager tabUnitManager;

    //[SerializeField] private List<UiCurrentCardUnit> uiCurrentCardUnits;

    public override void Init(int _idTab, bool _isDefault)
    {
        base.Init(_idTab, _isDefault);

        //tabUnitManager.Init();

        //for(int i = 0; i < uiCurrentCardUnits.Count; i++)
        //{
        //    uiCurrentCardUnits[i].Init();
        //}
    }

    public override void SetChoosing(bool isChoose)
    {
        base.SetChoosing(isChoose);
    }
}
