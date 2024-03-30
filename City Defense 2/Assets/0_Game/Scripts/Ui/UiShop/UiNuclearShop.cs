using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiNuclearShop : UiCanvas
{
    [SerializeField] private List<ElementUiNuclearShop> elementUiNuclearShops;

    public override void Init()
    {
        base.Init();

        for(int i = 0; i < elementUiNuclearShops.Count; i++)
        {
            elementUiNuclearShops[i].Init(i);
        }
    }
}
