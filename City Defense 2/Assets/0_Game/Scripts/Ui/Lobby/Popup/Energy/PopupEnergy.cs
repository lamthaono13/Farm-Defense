using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupEnergy : UiCanvas
{
    [SerializeField] private int gemToPurchase;

    [SerializeField] private int numberEarnGem;

    [SerializeField] private int numberEarnReward;

    [SerializeField] private Button btnGem;

    [SerializeField] private Button btnReward;

    [SerializeField] private GameObject objHideGem;

    [SerializeField] private TextMeshProUGUI textGem;

    [SerializeField] private TextMeshProUGUI textEarnGem;

    [SerializeField] private TextMeshProUGUI textEarnReward;

    protected override void Start()
    {
        base.Start();

        btnGem.onClick.AddListener(OnClickBtnGem);

        btnReward.onClick.AddListener(OnClickBtnReward);
    }

    public override void Show(bool _isShow)
    {
        base.Show(_isShow);

        if (_isShow)
        {
            Init();
        }
    }

    public override void Init()
    {
        base.Init();

        long currentGem = GameManager.Instance.DataManager.GetGem();
        
        if(gemToPurchase <= currentGem)
        {
            objHideGem.SetActive(false);
        }
        else
        {
            objHideGem.SetActive(true);
        }

        textEarnGem.text = "X " + numberEarnGem.ToString();

        textEarnReward.text = "X " + numberEarnReward;

        textGem.text = gemToPurchase.ToString();
    }

    private void OnClickBtnGem()
    {
        GameManager.Instance.DataManager.AddTicket(numberEarnGem, "Poppup_Use_Gem_To_Buy_Ticket");

        GameManager.Instance.DataManager.AddGem(-gemToPurchase, "Poppup_Use_Gem_To_Buy_Ticket");

        Show(false);
    }

    private void OnClickBtnReward()
    {
        AdsManager.Instance.ShowRewarded(() => 
        {
            GameManager.Instance.DataManager.AddTicket(numberEarnReward, "Poppup_Use_Reward_To_Buy_Ticket");

            Show(false);

        }, "Reward Poppup Ticket");
    }
}
