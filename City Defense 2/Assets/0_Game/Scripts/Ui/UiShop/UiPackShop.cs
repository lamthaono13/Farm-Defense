using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiPackShop : MonoBehaviour, IIAPListerner
{
    [SerializeField] private UiShop uiShop;

    [SerializeField] private DataGetPack dataGetPack;

    [SerializeField] private Button btnUnlock;

    [SerializeField] private TextMeshProUGUI textPrice;

    [SerializeField] private Image imgLock;

    [SerializeField] private Text textPrize;

    private int id;

    // Start is called before the first frame update
    void Start()
    {
        //HandleIAP.Instance.Delegate(this);
        btnUnlock.onClick.AddListener(OnClickBtnUnlock);
    }

    void InitLocalPrice()
    {
        for (int i = 0; i < HandleIAP.Instance.ShopProducts.Count; i++)
        {
            //if(HandleIAP.Instance.ShopProducts[i].productName == "adasda")
            //{
            ////.localizedPriceString;

            //}

            switch (id)
            {
                case 0:

                    if (HandleIAP.Instance.ShopProducts[i].productName == "DeadCoolPack")
                    {
                        Debug.Log(HandleIAP.Instance.ShopProducts[i].localizedPriceString);

                        textPrize.text = HandleIAP.Instance.ShopProducts[i].localizedPriceString;

                        return;
                    }

                    break;
                case 1:

                    if (HandleIAP.Instance.ShopProducts[i].productName == "IronArmorPack")
                    {
                        Debug.Log(HandleIAP.Instance.ShopProducts[i].localizedPriceString);

                        textPrize.text = HandleIAP.Instance.ShopProducts[i].localizedPriceString;

                        return;
                    }

                    break;
            }
        }


    }
    void Buy()
    {
        //bool sads = HandleIAP.Instance.IsExpired();
        //HandleIAP.Instance.RestorePurchases();
        //HandleIAP.Instance.BuyProduct(ShopProductNames.TerminatorPack);
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void Init(int _id)
    {
        id = _id;

        bool isUnlock = GameManager.Instance.DataManager.GetUnlockPackShop(id);

        textPrice.text = dataGetPack.DataPack[id].Index.ToString();

        ActivePack(!isUnlock);

        InitLocalPrice();
    }

    private void ActivePack(bool isActive)
    {
        if (isActive)
        {
            btnUnlock.interactable = true;

            btnUnlock.GetComponent<Image>().color = new Color(255, 255, 255);

            textPrice.color = new Color(255, 255, 255);

            if (imgLock != null)
            {
                imgLock.gameObject.SetActive(false);
            }
        }
        else
        {
            btnUnlock.interactable = false;

            btnUnlock.GetComponent<Image>().color = new Color(174, 174, 174);

            textPrice.color = new Color(174, 174, 174);

            if (imgLock != null)
            {
                imgLock.gameObject.SetActive(true);
            }
        }
    }

    private void OnClickBtnUnlock()
    {
        //switch (id)
        //{
        //    case 0:
        //        HandleIAP.Instance.BuyProduct(ShopProductNames.DeadCoolPack);
        //        break;
        //    case 1:
        //        HandleIAP.Instance.BuyProduct(ShopProductNames.IronArmorPack);
        //        break;
        //    case 2:
        //        break;
        //}

        //Buy();

        //OnUnlock();
    }

    public void OnUnlock()
    {
        //ActivePack(false);

        //GameManager.Instance.DataManager.SetUnlockPackShop(id);

        //AdsManager.Instance.SetIsRemoveAds(true);

        //AdsManager.Instance.HideBanner();

        //GameManager.Instance.DataManager.AddGem(50);

        ////GameManager.Instance.DataManager.SetUnlockPack(TypeAlly.Barel, 0, 2);

        //GameManager.Instance.DataManager.SetUnlockPack(TypeAlly.MeleeAlly, 0, 4);

        ////GameManager.Instance.DataManager.SetEquidAlly(TypeAlly.Barel, 0, 2);

        //GameManager.Instance.DataManager.SetEquidAlly(TypeAlly.MeleeAlly, 0, 4);

        //uiShop.UiEarnShop.Show(true);

        //if (LobbyManager.Instance != null)
        //{
        //    LobbyManager.Instance.UiLobbyManager.InitBarrack();
        //}
    }

    public void OnUnlock1()
    {
        //ActivePack(false);

        //GameManager.Instance.DataManager.SetUnlockPackShop(id);

        //AdsManager.Instance.SetIsRemoveAds(true);

        //AdsManager.Instance.HideBanner();

        //GameManager.Instance.DataManager.AddGem(50);

        ////GameManager.Instance.DataManager.SetUnlockPack(TypeAlly.Barel, 0, 2);

        //GameManager.Instance.DataManager.SetUnlockPack(TypeAlly.RangeAlly, 0, 4);

        ////GameManager.Instance.DataManager.SetEquidAlly(TypeAlly.Barel, 0, 2);

        //GameManager.Instance.DataManager.SetEquidAlly(TypeAlly.RangeAlly, 0, 4);

        //uiShop.UiEarnShop1.Show(true);

        //if (LobbyManager.Instance != null)
        //{
        //    LobbyManager.Instance.UiLobbyManager.InitBarrack();
        //}
    }

    public void OnRestorePurchases(string productName, bool onSuccess)
    {

    }



    public void OnBuyCompleted(ShopProductNames productName)
    {

        //switch (id)
        //{
        //    case 0:

        //        if (productName == "DeadCoolPack")
        //        {
        //            //Debug.Log(1);

        //            OnUnlock();

        //            return;
        //        }

        //        break;
        //    case 1:

        //        if (productName == "IronArmorPack")
        //        {
        //            //Debug.Log(1);

        //            OnUnlock1();

        //            return;
        //        }

        //        break;
        //}

    }
}