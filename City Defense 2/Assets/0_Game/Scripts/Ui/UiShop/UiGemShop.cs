using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiGemShop : UiCanvas
{
    [SerializeField] private List<ElementUiGemShop> elementUiGemShops;

    public override void Init()
    {
        base.Init();

        for(int i = 0; i < elementUiGemShops.Count; i++)
        {
            elementUiGemShops[i].Init(i);
        }
    }

    //public void OnRestorePurchases(string productName, bool onSuccess)
    //{
    //    for (int i = 0; i < elementUiGemShops.Count; i++)
    //    {
    //        elementUiGemShops[i].OnRestorePurchases(productName, onSuccess);
    //    }
    //}

    public void OnBuyCompleted(ShopProductNames productName)
    {
        for (int i = 0; i < elementUiGemShops.Count; i++)
        {
            elementUiGemShops[i].OnBuyCompleted(productName);
        }
    }
}
