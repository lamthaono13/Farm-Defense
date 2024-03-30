using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTabLobby : UiCanvas
{
    protected int idTab;

    protected bool isChoosing;

    public virtual void Init(int _idTab,bool _isDefault)
    {
        idTab = _idTab;

        SetChoosing(_isDefault);
    }

    public virtual void SetChoosing(bool isChoose)
    {
        isChoosing = isChoose;

        Show(isChoose);
    }

    public virtual void GoTo(int id)
    {

    }
}
