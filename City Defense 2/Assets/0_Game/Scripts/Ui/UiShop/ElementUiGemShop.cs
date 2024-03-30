using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ElementUiGemShop : MonoBehaviour, IIAPListerner
{
    [SerializeField] private int idProduct;

    [SerializeField] private DataGetGem dataGetGem;

    [SerializeField] private Button btnGem;

    [SerializeField] private Button btnGold;

    [SerializeField] private Button btnReward;

    [SerializeField] private Button btnMoney;

    [SerializeField] private Button btnFree;

    [SerializeField] private TextMeshProUGUI textNumber;

    [SerializeField] private TextMeshProUGUI textGem;

    [SerializeField] private TextMeshProUGUI textCoin;

    [SerializeField] private Text textMoney;

    [SerializeField] private UiNotEnough uiNotEnoughGem;

    [SerializeField] private UiNotEnough uiNotEnoughGold;

    [SerializeField] private Image imgHide;

    [SerializeField] private Image imgWarnningShop;

    private int id;

    private Buy buy;

    private int numberGem;

    private Tween tweenWarning;

    private void Start()
    {
        btnGem.onClick.AddListener(OnClickBtnGem);
        btnGold.onClick.AddListener(OnClickBtnCoin);
        btnReward.onClick.AddListener(OnClickBtnReward);
        btnMoney.onClick.AddListener(OnClickBtnMoney);
        btnFree.onClick.AddListener(OnClickBtnFree);
    }

    void InitLocalPrice()
    {
        for (int i = 0; i < HandleIAP.Instance.ShopProducts.Count; i++)
        {
            //if(HandleIAP.Instance.ShopProducts[i].productName == "adasda")
            //{
            ////.localizedPriceString;

            //}

            if (HandleIAP.Instance.ShopProducts[i].productName == ((ShopProductNames)idProduct).ToString() && id != 0)
            {
                textMoney.text = HandleIAP.Instance.ShopProducts[i].localizedPriceString;

                return;
            }
        }


    }

    public void Init(int _id)
    {
        id = _id;

        numberGem = (int)dataGetGem.ElementGetGems[id].IndexEarn;

        textNumber.text = NumberToString.ChangeNumberToString(numberGem);

        buy = dataGetGem.ElementGetGems[id].Buy;

        switch (buy.TypeBuy)
        {
            case TypeBuy.Money:

                textMoney.text = buy.Index.ToString();

                btnGem.gameObject.SetActive(false);

                btnGold.gameObject.SetActive(false);

                btnReward.gameObject.SetActive(false);

                btnMoney.gameObject.SetActive(true);

                btnFree.gameObject.SetActive(false);

                break;
            case TypeBuy.Gem:

                textGem.text = buy.Index.ToString();

                btnGem.gameObject.SetActive(true);

                btnGold.gameObject.SetActive(false);

                btnReward.gameObject.SetActive(false);

                btnMoney.gameObject.SetActive(false);

                btnFree.gameObject.SetActive(false);

                break;
            case TypeBuy.Gold:

                textCoin.text = buy.Index.ToString();

                btnGem.gameObject.SetActive(false);

                btnGold.gameObject.SetActive(true);

                btnReward.gameObject.SetActive(false);

                btnMoney.gameObject.SetActive(false);

                btnFree.gameObject.SetActive(false);

                break;
            case TypeBuy.Reward:

                btnGem.gameObject.SetActive(false);

                btnGold.gameObject.SetActive(false);

                btnMoney.gameObject.SetActive(false);

                if (GameManager.Instance.DataManager.GetFreeGemShop())
                {
                    btnFree.gameObject.SetActive(true);

                    btnReward.gameObject.SetActive(false);

                    TweenWarning();
                }
                else
                {
                    if (GameManager.Instance.DataManager.CheckCanEarnRewardGemShop())
                    {
                        btnReward.gameObject.SetActive(true);

                        btnFree.gameObject.SetActive(false);

                        TweenWarning();
                    }
                    else
                    {
                        imgHide.gameObject.SetActive(true);

                        btnReward.gameObject.SetActive(true);

                        btnFree.gameObject.SetActive(false);

                        imgWarnningShop.gameObject.SetActive(false);
                    }
                }

                break;
        }

        InitLocalPrice();
    }

    private void TweenWarning()
    {
        if(tweenWarning != null)
        {
            tweenWarning.Kill();
        }

        imgWarnningShop.transform.localScale = Vector3.one;

        imgWarnningShop.gameObject.SetActive(true);

        tweenWarning = imgWarnningShop.transform.DOScale(Vector3.one * 1.2f, 0.2f).SetUpdate(true).SetEase(DG.Tweening.Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnClickBtnCoin()
    {
        long currentCoin = GameManager.Instance.DataManager.GetGold();

        if (currentCoin >= buy.Index)
        {
            GameManager.Instance.DataManager.AddGold(-(int)buy.Index, "");

            GameManager.Instance.DataManager.AddGem(numberGem, "", true);

            LobbyManager.Instance.UiLobbyManager.UiGem.OnGet(btnFree.gameObject.GetComponent<RectTransform>());

        }
        else
        {
            uiNotEnoughGold.Play();
        }
    }

    private void OnClickBtnGem()
    {
        long currentGem = GameManager.Instance.DataManager.GetGem();

        if (currentGem >= buy.Index)
        {
            GameManager.Instance.DataManager.AddGem(-(int)buy.Index, "");

            GameManager.Instance.DataManager.AddGem(numberGem, "", true);

            LobbyManager.Instance.UiLobbyManager.UiGem.OnGet(btnFree.gameObject.GetComponent<RectTransform>());
        }
        else
        {
            uiNotEnoughGem.Play();
        }
    }

    private void OnClickBtnReward()
    {
        AdsManager.Instance.ShowRewarded(() => 
        {
            GameManager.Instance.DataManager.AddGem(numberGem, "Use_Reward_To_Buy_Gem_Shop", true);

            GameManager.Instance.DataManager.SetEarnRewardGemShop();

            Init(id);

            LobbyManager.Instance.UiLobbyManager.UiGem.OnGet(btnFree.gameObject.GetComponent<RectTransform>());

            //HandleFireBase.Instance.LogEventReward("Buy_Gem_Shop");
        }, "Buy_Gem_Shop");
    }

    private void OnClickBtnFree()
    {
        GameManager.Instance.DataManager.AddGem(numberGem, "Use_Free_To_Buy_Gem_Shop", true);

        GameManager.Instance.DataManager.SetFreeGemShop();

        Init(id);

        //int currentLevel = GameManager.Instance.DataManager.GetLevel();

        LobbyManager.Instance.UiLobbyManager.UiGem.OnGet(btnFree.gameObject.GetComponent<RectTransform>());

        //if (currentLevel == 9 && !GameManager.Instance.NoTutorial && GameManager.Instance.DataManager.GetTutorialShop())
        //{
        //    GameManager.Instance.GetUiShop(TypeCamera.Lobby).OnDoneTutorial();
        //}
    }

    private void OnClickBtnMoney()
    {
        Buy();
    }

    void Buy()
    {
        //bool sads = HandleIAP.Instance.IsExpired();
        //HandleIAP.Instance.RestorePurchases();
        HandleIAP.Instance.BuyProduct((ShopProductNames)idProduct);
    }

    //public void OnRestorePurchases(string productName, bool onSuccess)
    //{

    //}

    //public void OnBuyCompleted(string productName)
    //{
    //    //if (!onSuccess) return;



     
    //}

    public void OnBuyCompleted(ShopProductNames productName)
    {
        if (productName == (ShopProductNames)idProduct && id != 0)
        {
            GameManager.Instance.DataManager.AddGem(numberGem, "Use_Money_To_Buy_Gem_Shop", true);

            //AdsManager.Instance.SetIsRemoveAds(true);

            return;
        }

        Debug.Log(((ShopProductNames)idProduct).ToString());
    }
}
