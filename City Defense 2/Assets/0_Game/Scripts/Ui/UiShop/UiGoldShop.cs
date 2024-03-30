using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiGoldShop : UiCanvas
{
    [SerializeField] private List<ElementUiGoldShop> elementUiGoldShops;

    public override void Init()
    {
        base.Init();

        for(int i = 0; i < elementUiGoldShops.Count; i++)
        {
            elementUiGoldShops[i].Init(i);
        }
    }
}
