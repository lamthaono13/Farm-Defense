using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UiEnergy : UiCanvas
{
    [SerializeField] private Image imageFill;

    [SerializeField] private TextMeshProUGUI textEnergy;

    [SerializeField] private GameObject objTutorial;

    private Tween tweenFill;

    private Tween tweenScale;

    private bool hasTutorial;

    protected override void Start()
    {
        base.Start();
    }

    public override void Show(bool _isShow)
    {
        base.Show(_isShow);
    }

    public override void Init()
    {
        base.Init();
        Show(true);

        EnergyManager.Instance.OnChangeEnergy += FillBar;
    }

    public void FillBar(float current, float max, bool isTutorial)
    {
        //imageFill.fillAmount = current / max;

        if(tweenFill != null)
        {
            tweenFill.Kill();
        }

        tweenFill = imageFill.DOFillAmount(current / max, 0.1f).SetUpdate(true).OnComplete(() => { tweenFill = null; });

        ChangeText(current, max);

        if (isTutorial && GameManager.Instance.DataManager.GetHasTutorialChooseChar())
        {
            objTutorial.gameObject.SetActive(true);
            StartCoroutine(WaitTutorial());
        }


    }

    IEnumerator WaitTutorial()
    {
        yield return new WaitForSeconds(3);
        objTutorial.gameObject.SetActive(false);
    }

    private void ChangeText(float current, float max)
    {
        textEnergy.text = current.ToString() + " / " + max.ToString();
    }

    public void OnEndgame(GameResult gameResult)
    {
        Show(false);
    }
}
