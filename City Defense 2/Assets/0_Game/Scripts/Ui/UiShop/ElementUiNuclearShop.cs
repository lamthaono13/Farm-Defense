using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElementUiNuclearShop : MonoBehaviour
{
    [SerializeField] private DataSpecialWeapon dataSpecialWeapon;

    [SerializeField] private TypeSpecialWeapon typeSpecialWeapon;

    [SerializeField] private Button btnGem;

    [SerializeField] private Button btnGold;

    [SerializeField] private Button btnReward;

    [SerializeField] private TextMeshProUGUI textNumberSpecialWeapon;

    [SerializeField] private TextMeshProUGUI textGem;

    [SerializeField] private TextMeshProUGUI textCoin;

    [SerializeField] private Image imageRender;

    [SerializeField] private Sprite spriteNuclear;

    [SerializeField] private Sprite spriteHero;

    [SerializeField] private UiNotEnough uiNotEnoughGem;

    [SerializeField] private UiNotEnough uiNotEnoughGold;

    private int id;

    private Buy buy;

    private int numberSpecialWeapon;

    private void Start()
    {
        btnGem.onClick.AddListener(OnClickBtnGem);
        btnGold.onClick.AddListener(OnClickBtnCoin);
        btnReward.onClick.AddListener(OnClickBtnReward);
    }

    public void Init(int _id)
    {
        id = _id;

        switch (typeSpecialWeapon)
        {
            case TypeSpecialWeapon.Hero:
                imageRender.sprite = spriteHero;
                break;
            case TypeSpecialWeapon.Boom:
                imageRender.sprite = spriteNuclear;
                break;
        }

        numberSpecialWeapon = (int)dataSpecialWeapon.ElementGetSpecials[id].IndexEarn;

        textNumberSpecialWeapon.text = numberSpecialWeapon.ToString();

        buy = dataSpecialWeapon.ElementGetSpecials[id].Buy;

        switch (buy.TypeBuy)
        {
            case TypeBuy.Money:

                break;
            case TypeBuy.Gem:

                textGem.text = buy.Index.ToString();

                btnGem.gameObject.SetActive(true);

                btnGold.gameObject.SetActive(false);

                btnReward.gameObject.SetActive(false);

                break;
            case TypeBuy.Gold:

                textCoin.text = buy.Index.ToString();

                btnGem.gameObject.SetActive(false);

                btnGold.gameObject.SetActive(true);

                btnReward.gameObject.SetActive(false);

                break;
            case TypeBuy.Reward:

                btnGem.gameObject.SetActive(false);

                btnGold.gameObject.SetActive(false);

                btnReward.gameObject.SetActive(true);

                break;
        }
    }

    private void OnClickBtnCoin()
    {
        long currentCoin = GameManager.Instance.DataManager.GetGold();

        switch (typeSpecialWeapon)
        {
            case TypeSpecialWeapon.Hero:

                if (currentCoin >= buy.Index)
                {
                    GameManager.Instance.DataManager.AddGold(-(int)buy.Index, "Use_Gold_To_Buy_Hero");

                    //GameManager.Instance.DataManager.AddNumberHero(numberSpecialWeapon);

                }
                else
                {
                    uiNotEnoughGold.Play();
                }

                break;
            case TypeSpecialWeapon.Boom:

                if (currentCoin >= buy.Index)
                {
                    GameManager.Instance.DataManager.AddGold(-(int)buy.Index, "Use_Gold_To_Buy_Boom");

                    GameManager.Instance.DataManager.AddBoom(numberSpecialWeapon);

                }
                else
                {
                    uiNotEnoughGold.Play();
                }

                break;
        }
    }

    private void OnClickBtnGem()
    {
        long currentGem = GameManager.Instance.DataManager.GetGem();

        switch (typeSpecialWeapon)
        {
            case TypeSpecialWeapon.Hero:

                if (currentGem >= buy.Index)
                {
                    GameManager.Instance.DataManager.AddGem(-(int)buy.Index, "Use_Gem_To_Buy_Hero");

                    //GameManager.Instance.DataManager.AddNumberHero(numberSpecialWeapon);
                }
                else
                {
                    uiNotEnoughGem.Play();
                }

                break;
            case TypeSpecialWeapon.Boom:

                if (currentGem >= buy.Index)
                {
                    GameManager.Instance.DataManager.AddGem(-(int)buy.Index, "Use_Gem_To_Buy_Boom");

                    GameManager.Instance.DataManager.AddBoom(numberSpecialWeapon);
                }
                else
                {
                    uiNotEnoughGem.Play();
                }

                break;
        }
    }

    private void OnClickBtnReward()
    {
        switch (typeSpecialWeapon)
        {
            case TypeSpecialWeapon.Hero:

                AdsManager.Instance.ShowRewarded(() =>
                {
                    //GameManager.Instance.DataManager.AddNumberHero(numberSpecialWeapon);
                }, "Buy_Hero_Shop");

                //HandleFireBase.Instance.LogEventReward("Buy_Hero_Shop");

                break;
            case TypeSpecialWeapon.Boom:

                AdsManager.Instance.ShowRewarded(() =>
                {
                    GameManager.Instance.DataManager.AddBoom(numberSpecialWeapon);
                }, "Buy_Nuclear_Shop");

                //HandleFireBase.Instance.LogEventReward("Buy_Nuclear_Shop");

                break;
        }



    }
}