using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class UiShop : UiTabLobby, IIAPListerner
{
    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] private Canvas canvasShop;

    [SerializeField] private GameObject objTutorialGold;

    [SerializeField] private GameObject objTutorialGem;

    [SerializeField] private UiEarnShop uiEarnShop;

    [SerializeField] private UiEarnShop uiEarnShop1;

    [SerializeField] private List<UiPackShop> uiPackShops;

    [SerializeField] private UiNuclearShop uiNuclearShop;

    [SerializeField] private UiGoldShop uiGoldShop;

    [SerializeField] private UiGemShop uiGemShop;

    [SerializeField] private UiTicketShop uiTicketShop;

    [SerializeField] private Transform transformScroll;

    [SerializeField] private List<float> positionScrollTap;

    public UiEarnShop UiEarnShop => uiEarnShop;

    public UiEarnShop UiEarnShop1 => uiEarnShop1;

    public Canvas CanvasShop => canvasShop;

    public override void Show(bool _isShow)
    {
        SetPosition();

        base.Show(_isShow);

        if (_isShow)
        {
            //Time.timeScale = 0;

            Init();
        }
        else
        {
            //Time.timeScale = 1;

            if (LobbyManager.Instance != null)
            {
                //LobbyManager.Instance.UiLobbyManager.InitBarrack();
            }
        }
    }

    public override void SetChoosing(bool isChoose)
    {
        base.SetChoosing(isChoose);

        Show(isChoose);
    }

    protected override void Start()
    {
        base.Start();

        HandleIAP.Instance.Register(this);
    }

    private void OnDestroy()
    {
        HandleIAP.Instance.UnRegister(this);
    }

    public override void Init()
    {
        base.Init();

        //for (int i = 0; i < uiPackShops.Count; i++)
        //{
        //    uiPackShops[i].Init(i);
        //}

        uiNuclearShop.Init();

        uiGoldShop.Init();

        uiGemShop.Init();

        uiTicketShop.Init();

        int currentLevel = GameManager.Instance.DataManager.GetLevel();

        //if(currentLevel == 9 && !GameManager.Instance.NoTutorial && GameManager.Instance.DataManager.GetTutorialShop())
        //{
        //    OnTutorial();

        //    OnTutorialGold();
        //}
    }

    [Button]
    public void ScrollTo(TypeGoShop typeGoShop)
    {
        transformScroll.DOLocalMoveY(positionScrollTap[(int)typeGoShop], 0.2f).SetUpdate(true);
    }

    public override void GoTo(int id)
    {
        base.GoTo(id);

        ScrollTo((TypeGoShop)id);
    }

    public void SetPosition()
    {
        transformScroll.localPosition = new Vector3(transformScroll.localPosition.x, positionScrollTap[0], transformScroll.localPosition.z);
    }

    //public void OnRestorePurchases(string productName, bool onSuccess)
    //{
    //    if (!onSuccess)
    //    {
    //        return;
    //    }

    //    if (productName == "DeadCoolPack")
    //    {
    //        uiPackShops[0].OnRestorePurchases(productName, onSuccess);

    //        return;
    //    }

    //    if (productName == "IronArmorPack")
    //    {
    //        uiPackShops[1].OnRestorePurchases(productName, onSuccess);

    //        return;
    //    }

    //    uiGemShop.OnRestorePurchases(productName, onSuccess);
    //}

    //public void OnBuyCompleted(string productName, bool onSuccess)
    //{
      
    //    //for(int i = 0; i < uiPackShops.Count; i++)
    //    //{
    //    //    uiPackShops[i].OnBuyCompleted(productName, onSuccess);
    //    //}

       
    //}

    public void OnTutorial()
    {
        transformScroll.localPosition = new Vector3(transformScroll.localPosition.x, positionScrollTap[positionScrollTap.Count - 1], transformScroll.localPosition.z);
    }

    public void OnTutorialGold()
    {
        scrollRect.enabled = false;

        objTutorialGold.transform.parent = canvasShop.transform;

        objTutorialGold.transform.SetAsLastSibling();

        objTutorialGold.gameObject.SetActive(true);
    }

    public void OnTutorialGem()
    {
        objTutorialGem.transform.parent = canvasShop.transform;

        objTutorialGem.transform.SetAsLastSibling();

        objTutorialGold.gameObject.SetActive(false);

        objTutorialGem.gameObject.SetActive(true);
    }

    public void OnDoneTutorial()
    {
        objTutorialGem.gameObject.SetActive(false);

        //GameManager.Instance.DataManager.SetTutorialShop();

        //LobbyManager.Instance.UiLobbyManager.UiMainLobby.OnTutorialShopDone();

        scrollRect.enabled = true;
    }

    public void OnBuyCompleted(ShopProductNames productName)
    {
        uiGemShop.OnBuyCompleted(productName);
    }
}
