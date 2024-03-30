using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using AssetKits.ParticleImage;
using DG.Tweening;

public class UiCoin : UiCanvas
{
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private Button btnAdd;

    [SerializeField] private ParticleImage particleImage;

    private long current;

    private Tween tween;

    protected override void Start()
    {
        base.Start();

        current = GameManager.Instance.DataManager.GetGold();

        string a = NumberToString.ChangeNumberToString(current);

        text.text = a;

        btnAdd.onClick.AddListener(OnClickBtnAdd);

        GameManager.Instance.DataManager.OnChangeGold += OnChange;

        if(btnAdd != null)
        {
            //int currentLevel = GameManager.Instance.DataManager.GetLevel();

            //if(currentLevel < 9)
            //{
            //    btnAdd.gameObject.SetActive(false);
            //}
        }
    }

    public void OnChange(bool hasAction)
    {
        if (hasAction)
        {
            //current = GameManager.Instance.DataManager.GetGold();

            //string a = NumberToString.ChangeNumberToString(current);

            //text.text = a;
        }
        else
        {
            ActionTween();
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

        long coppy = current;

        tween = DOTween.To((x) =>
        {
            string a = NumberToString.ChangeNumberToString((long)x);

            text.text = a;

            current = (long)x;

        }, coppy, GameManager.Instance.DataManager.GetGold(), 1).SetEase(DG.Tweening.Ease.Linear).SetUpdate(true).OnComplete(() =>
        {
            tween = null;
        });
    }

    private void OnDestroy()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.DataManager.OnChangeGold -= OnChange;
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

        LobbyManager.Instance.UiLobbyManager.UiTabLobbyManager.GetTab((int)TypeTabLobby.Shop).GoTo((int)TypeGoShop.Gold);
    }
}
