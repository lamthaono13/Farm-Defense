using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabUnitManager : UiCanvas
{
    [SerializeField] private TabListCard tabListCard;

    [SerializeField] private GroupListCard groupListCard;

    protected override void Start()
    {
        base.Start();

        Show(true);
    }

    public override void Show(bool _isShow)
    {
        base.Show(_isShow);

        if (_isShow)
        {
            Init();
        }
    }

    public override void Init()
    {
        base.Init();

        tabListCard.Init(this, 0);

        groupListCard.Init(this, 0);
    }

    public void ChangeTab(int idTab)
    {
        tabListCard.ChangeTab(idTab);

        groupListCard.ChangeTab(idTab);
    }
}
