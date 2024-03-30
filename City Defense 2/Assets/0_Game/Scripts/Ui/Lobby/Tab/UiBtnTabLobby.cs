using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UiBtnTabLobby : MonoBehaviour
{
    [SerializeField] private Button btnChoose;

    [SerializeField] private Sprite spriteBgChoose;

    [SerializeField] private Sprite spriteBgUnChoose;

    [SerializeField] private Image imgRenderTab;

    [SerializeField] private Image imgBg;

    [SerializeField] private GameObject objScaleBg;

    [SerializeField] private GameObject objRender;

    [SerializeField] private GameObject objHide;

    [SerializeField] private Image imgWarning;

    private int idTab;

    private bool isChoosing;

    private Tween tweenScaleBg;

    private Tween tweenScaleRender;

    private Tween tweenMoveRender;

    private Tween tweenWarning;

    private void Start()
    {
        btnChoose.onClick.AddListener(OnClickBtnChoose);
    }

    public void Init(int _idTab, bool isDefaul)
    {
        idTab = _idTab;

        SetChoosing(isDefaul);
    }

    private void TweenWarning()
    {
        if (tweenWarning != null)
        {
            tweenWarning.Kill();
        }

        imgWarning.transform.localScale = Vector3.one;

        imgWarning.gameObject.SetActive(true);

        imgWarning.transform.DOScale(Vector3.one * 1.2f, 0.2f).SetUpdate(true).SetEase(DG.Tweening.Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    public void SetChoosing(bool isChoose)
    {
        isChoosing = isChoose;

        imgBg.sprite = isChoose ? spriteBgChoose : spriteBgUnChoose;

        float timeTween = 0.15f;

        //bg

        if (tweenScaleBg != null)
        {
            tweenScaleBg.Kill();
        }

        if (isChoose)
        {
            tweenScaleBg = objScaleBg.transform.DOScaleY(1.15f, timeTween).SetUpdate(true).OnComplete(() => { tweenScaleBg = null; });
        }
        else
        {
            tweenScaleBg = objScaleBg.transform.DOScaleY(1, timeTween).SetUpdate(true).OnComplete(() => { tweenScaleBg = null; });
        }

        // warning

        if((TypeTabLobby)idTab == TypeTabLobby.Shop)
        {
            if (GameManager.Instance.DataManager.CheckCanEarnShop())
            {
                TweenWarning();
            }
            else
            {
                imgWarning.gameObject.SetActive(false);
            }
        }

        //render

        imgRenderTab.sprite = GameManager.Instance.DataManager.DataManagerMainGame.DataSprite.GetSpriteTabLobby((TypeTabLobby)idTab);

        imgRenderTab.SetNativeSize();

        //sccale

        if(tweenScaleRender != null)
        {
            tweenScaleRender.Kill();
        }

        if (isChoose)
        {
            tweenScaleRender = objRender.transform.DOScale(1.5f, timeTween).SetUpdate(true).OnComplete(() => { tweenScaleRender = null; });
        }
        else
        {
            tweenScaleRender = objRender.transform.DOScale(1, timeTween).SetUpdate(true).OnComplete(() => { tweenScaleRender = null; });
        }

        //move

        if(tweenMoveRender != null)
        {
            tweenMoveRender.Kill();
        }

        if (isChoose)
        {
            tweenMoveRender = objRender.transform.DOLocalMoveY(84, timeTween).SetUpdate(true).OnComplete(() => { tweenMoveRender = null; });
        }
        else
        {
            tweenMoveRender = objRender.transform.DOLocalMoveY(0, timeTween).SetUpdate(true).OnComplete(() => { tweenMoveRender = null; });
        }
    }

    private void OnClickBtnChoose()
    {


        if (!isChoosing)
        {
            LobbyManager.Instance.UiLobbyManager.UiTabLobbyManager.SetTab(idTab);

            int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

            bool a = GameManager.Instance.DataManager.GetHasTutorialLobbyLv3();

            if(levelUnlock == 3 && !a)
            {
                LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUnlockUnit();
            }

            bool b = GameManager.Instance.DataManager.GetHasTutorialLobbyLv6();

            if (levelUnlock == 6 && !b)
            {
                LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUpgradeUnit();
            }

            if (levelUnlock < 6 && levelUnlock > 3 && GameManager.Instance.DataManager.GetIsTutorialUpgradeLobby() && !GameManager.Instance.DataManager.GetHasTutorialUpgrade())
            {
                LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUpgradeLose();
            }
        }
    }

    public void ActiveBtnTab(bool isActive)
    {
        objHide.gameObject.SetActive(!isActive);
    }
}
