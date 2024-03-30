using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AssetKits.ParticleImage;
using DG.Tweening;

public class UiGem : UiCanvas
{
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private Button btnAdd;

    [SerializeField] private ParticleImage particleImage;

    private long current;

    private Tween tween;

    protected override void Start()
    {
        base.Start();

        current = GameManager.Instance.DataManager.GetGem();

        string a = NumberToString.ChangeNumberToString(current);

        text.text = a;

        btnAdd.onClick.AddListener(OnClickBtnAdd);

        GameManager.Instance.DataManager.OnChangeGem += OnChange;

        if (btnAdd != null)
        {
            //int currentLevel = GameManager.Instance.DataManager.GetLevel();

            //if (currentLevel < 9)
            //{
            //    btnAdd.gameObject.SetActive(false);
            //}
        }
    }

    public void OnChange(bool hasAction)
    {
        if (hasAction)
        {
            //current = GameManager.Instance.DataManager.GetGem();

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

        }, coppy, GameManager.Instance.DataManager.GetGem(), 1).SetEase(DG.Tweening.Ease.Linear).SetUpdate(true).OnComplete(() =>
        {
            tween = null;
        });
    }

    private void OnDestroy()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.DataManager.OnChangeGem -= OnChange;
        }
    }

    private void OnClickBtnAdd()
    {
        // UiShop uiShop = GameManager.Instance.GetUiShop(TypeCamera.Lobby);
        //
        // uiShop.Show(true);
        //
        // uiShop.ScrollTo(TypeGoShop.Gem);

        LobbyManager.Instance.UiLobbyManager.UiTabLobbyManager.SetTab((int)TypeTabLobby.Shop);

        LobbyManager.Instance.UiLobbyManager.UiTabLobbyManager.GetTab((int)TypeTabLobby.Shop).GoTo((int)TypeGoShop.Gem);
    }
}
