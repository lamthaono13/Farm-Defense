using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTicketShop : UiCanvas
{
    [SerializeField] private List<ElementUiTicketShop> elementUiTicketShops;

    public override void Init()
    {
        base.Init();

        for (int i = 0; i < elementUiTicketShops.Count; i++)
        {
            elementUiTicketShops[i].Init(i);
        }
    }
}
