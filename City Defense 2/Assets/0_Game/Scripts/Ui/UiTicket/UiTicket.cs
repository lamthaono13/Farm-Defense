using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiTicket : UiCanvas
{
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private TextMeshProUGUI textTime;

    [SerializeField] private Button btnAdd;

    [SerializeField] private GameObject objTime;

    private bool checkCanTime;

    private float timeCount;

    [SerializeField] private ParticleImage particleImage;

    private int current;

    private Tween tween;

    protected override void Start()
    {
        base.Start();

        current = GameManager.Instance.DataManager.GetTicket();

        string a = NumberToString.ChangeNumberToString(current);

        text.text = a + "/" + GameManager.Instance.DataManager.DataManagerMainGame.DataGame.MaxTicket.ToString();

        checkCanTime = GameManager.Instance.DataManager.GetCanReborn();

        if (checkCanTime)
        {
            //timeCount = GameManager.Instance.DataManager.GetTimeCountRebornTicket();

            objTime.gameObject.SetActive(true);
        }
        else
        {
            objTime.gameObject.SetActive(false);
        }

        btnAdd.onClick.AddListener(OnClickBtnAdd);

        GameManager.Instance.DataManager.OnChangeTicket += OnChange;

        if (btnAdd != null)
        {
            //int currentLevel = GameManager.Instance.DataManager.GetLevel();

            //if (currentLevel < 9)
            //{
            //    btnAdd.gameObject.SetActive(false);
            //}
        }
    }

    private void Update()
    {
        //if (checkCanTime)
        //{
        //    if(timeCount < 0)
        //    {
        //        timeCount = GameManager.Instance.DataManager.DataManagerMainGame.DataGame.MaxTicket;

        //        textTime.text = (int)timeCount + "s";

        //        checkCanTime = GameManager.Instance.DataManager.InitRebornTicket();

        //        if (!checkCanTime)
        //        {
        //            textTime.gameObject.SetActive(false);
        //        }
        //    }
        //    else
        //    {
        //        timeCount -= Time.deltaTime;

        //        textTime.text = (int)timeCount + "s";
        //    }
        //}

        if (GameManager.Instance.DataManager.GetCanReborn())
        {
            textTime.text = (int)GameManager.Instance.DataManager.GetTimeCountTicket() + "s";
        }
        else
        {
            textTime.gameObject.SetActive(false);
        }
    }

    public void OnChange(bool hasAction)
    {
        if (hasAction)
        {
            //current = GameManager.Instance.DataManager.GetTicket();

            //string a = NumberToString.ChangeNumberToString(current);

            //text.text = a + "/" + GameManager.Instance.DataManager.DataManagerMainGame.DataGame.MaxTicket.ToString();
        }
        else
        {
            ActionTween();
        }

        checkCanTime = GameManager.Instance.DataManager.GetCanReborn();

        if (checkCanTime)
        {
            //timeCount = GameManager.Instance.DataManager.GetTimeCountRebornTicket();

            objTime.gameObject.SetActive(true);
        }
        else
        {
            objTime.gameObject.SetActive(false);
        }
    }

    public void OnGet(RectTransform rectTransform)
    {
        particleImage.rectTransform.position = rectTransform.position;

        particleImage.Play();
    }

    public void ActionTween()
    {
        if (tween != null)
        {
            tween.Kill();
        }

        int coppy = current;

        tween = DOTween.To((x) =>
        {
            string a = NumberToString.ChangeNumberToString((long)x);

            text.text = a + "/" + GameManager.Instance.DataManager.DataManagerMainGame.DataGame.MaxTicket.ToString();

            current = (int)x;

        }, coppy, GameManager.Instance.DataManager.GetTicket(), 1).SetEase(DG.Tweening.Ease.Linear).SetUpdate(true).OnComplete(() =>
        {
            tween = null;
        });
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.DataManager.OnChangeTicket -= OnChange;
        }
    }

    private void OnClickBtnAdd()
    {
        //UiShop uiShop = GameManager.Instance.GetUiShop(TypeCamera.Lobby);
        //
        // uiShop.Show(true);
        //
        // uiShop.ScrollTo(TypeGoShop.Gold);

        LobbyManager.Instance.UiLobbyManager.UiTabLobbyManager.SetTab((int)TypeTabLobby.Shop);

        LobbyManager.Instance.UiLobbyManager.UiTabLobbyManager.GetTab((int)TypeTabLobby.Shop).GoTo((int)TypeGoShop.Ticket);
    }
}
