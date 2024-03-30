using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ElementUiTicketShop : MonoBehaviour
{
    [SerializeField] private DataGetTicket dataGetTicket;

    [SerializeField] private Button btnGem;

    [SerializeField] private Button btnGold;

    [SerializeField] private Button btnReward;

    [SerializeField] private Button btnFree;

    [SerializeField] private TextMeshProUGUI textNumber;

    [SerializeField] private TextMeshProUGUI textGem;

    [SerializeField] private TextMeshProUGUI textCoin;

    [SerializeField] private UiNotEnough uiNotEnoughGem;

    [SerializeField] private UiNotEnough uiNotEnoughGold;

    [SerializeField] private Image imgHide;

    [SerializeField] private Image imgWarnningShop;

    private int id;

    private Buy buy;

    private int numberGold;

    private Tween tweenWarning;

    private void Start()
    {
        btnGem.onClick.AddListener(OnClickBtnGem);
        btnGold.onClick.AddListener(OnClickBtnCoin);
        btnReward.onClick.AddListener(OnClickBtnReward);
        btnFree.onClick.AddListener(OnClickBtnFree);
    }

    public void Init(int _id)
    {
        id = _id;

        int currentMap = GameManager.Instance.DataManager.GetMap();

        numberGold = (int)dataGetTicket.ElementGetTickets[id].IndexEarn;

        textNumber.text = NumberToString.ChangeNumberToString(numberGold);

        buy = dataGetTicket.ElementGetTickets[id].Buy;

        switch (buy.TypeBuy)
        {
            case TypeBuy.Money:
                break;
            case TypeBuy.Gem:

                textGem.text = buy.Index.ToString();

                btnGem.gameObject.SetActive(true);

                btnGold.gameObject.SetActive(false);

                btnReward.gameObject.SetActive(false);

                btnFree.gameObject.SetActive(false);

                break;
            case TypeBuy.Gold:

                textCoin.text = buy.Index.ToString();

                btnGem.gameObject.SetActive(false);

                btnGold.gameObject.SetActive(true);

                btnReward.gameObject.SetActive(false);

                btnFree.gameObject.SetActive(false);

                break;
            case TypeBuy.Reward:

                btnGem.gameObject.SetActive(false);

                btnGold.gameObject.SetActive(false);

                if (GameManager.Instance.DataManager.GetFreeTicketShop())
                {
                    btnFree.gameObject.SetActive(true);

                    btnReward.gameObject.SetActive(false);

                    TweenWarning();
                }
                else
                {
                    if (GameManager.Instance.DataManager.CheckCanEardRewardTicketShop())
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
    }

    private void TweenWarning()
    {
        if (tweenWarning != null)
        {
            tweenWarning.Kill();
        }

        imgWarnningShop.transform.localScale = Vector3.one;

        imgWarnningShop.gameObject.SetActive(true);

        imgWarnningShop.transform.DOScale(Vector3.one * 1.2f, 0.2f).SetUpdate(true).SetEase(DG.Tweening.Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnClickBtnCoin()
    {
        long currentCoin = GameManager.Instance.DataManager.GetGold();

        if (currentCoin >= buy.Index)
        {
            GameManager.Instance.DataManager.AddGold(-(int)buy.Index, "Use_Gold_To_Buy_Ticket_Shop");

            GameManager.Instance.DataManager.AddTicket(numberGold, "Use_Gold_To_Buy_Ticket_Shop", true);

            LobbyManager.Instance.UiLobbyManager.UiTicket.OnGet(btnFree.gameObject.GetComponent<RectTransform>());

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
            GameManager.Instance.DataManager.AddGem(-(int)buy.Index, "Use_Gem_To_Buy_Ticket_Shop");

            GameManager.Instance.DataManager.AddTicket(numberGold, "Use_Gem_To_Buy_Ticket_Shop", true);

            LobbyManager.Instance.UiLobbyManager.UiTicket.OnGet(btnFree.gameObject.GetComponent<RectTransform>());
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
            GameManager.Instance.DataManager.AddTicket(numberGold, "Use_Reward_To_Buy_Ticket_Shop", true);

            //HandleFireBase.Instance.LogEventReward("GoldInShop");

            GameManager.Instance.DataManager.SetEarnRewardTicketShop();

            //HandleFireBase.Instance.LogEventBuyGold(numberGold);

            Init(id);

            LobbyManager.Instance.UiLobbyManager.UiTicket.OnGet(btnFree.gameObject.GetComponent<RectTransform>());

        }, "GoldInShop");
    }

    private void OnClickBtnFree()
    {
        GameManager.Instance.DataManager.AddTicket(numberGold, "Use_Free_To_Buy_Ticket_Shop", true);

        GameManager.Instance.DataManager.SetFreeTicketShop();

        Init(id);

        LobbyManager.Instance.UiLobbyManager.UiTicket.OnGet(btnFree.gameObject.GetComponent<RectTransform>());

        //int currentLevel = GameManager.Instance.DataManager.GetLevel();

        //if (currentLevel == 9 && !GameManager.Instance.NoTutorial && GameManager.Instance.DataManager.GetTutorialShop())
        //{
        //    GameManager.Instance.GetUiShop(TypeCamera.Lobby).OnTutorialGem();
        //}
    }
}