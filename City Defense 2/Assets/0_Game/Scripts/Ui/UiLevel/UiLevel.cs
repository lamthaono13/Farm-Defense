using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
//using I2.Loc;
using UnityEngine.UI;

public class UiLevel : UiCanvas
{
    [SerializeField] private TextMeshProUGUI textLevel;

    [SerializeField] private Image imageLevel;

    //[SerializeField] private LocalizationParamsManager localManager;

    private int currenLevel;

    public override void Show(bool _isShow)
    {
        base.Show(_isShow);
    }

    public override void Init()
    {
        base.Init();

        currenLevel = GameManager.Instance.DataManager.GetLevel();

        textLevel.text = "LEVEL " + currenLevel.ToString();

        StartCoroutine(WaitFadeLevel());

        //LocalizationManager.GetTranslation("Level");

        //textLevel.text = "LEVEL " + currenLevel.ToString();

        //LocalizationManager.GetTranslation("Level").Replace("{[Level]}", currenLevel.ToString());

        //textLevel.text = LocalizationManager.GetTranslation("Level").Replace("{Level}", currenLevel.ToString());

        //localManager.SetParameterValue("Level", GameManager.Instance.DataManager.GetLevel().ToString(), true);

        //textLevel.text = LocalizationManager.GetTranslation("Level", localManager.gameObject);

        //LocalizationManager.get

        //I2.Loc.LocalizationManager.CurrentLanguage = I2.Loc.LocalizationManager.CurrentLanguage;
    }

    IEnumerator WaitFadeLevel()
    {
        yield return new WaitForSeconds(2);

        textLevel.DOFade(0, 0.3f);

        imageLevel.DOFade(0, 0.3f);
    }

    //public void OnEnable()
    //{
    //    if (!LocalizationManager.ParamManagers.Contains(this))
    //    {

    //        LocalizationManager.ParamManagers.Add(this);

    //        LocalizationManager.LocalizeAll(true);
    //    }

    //}

    //public void OnDisable()
    //{
    //    LocalizationManager.ParamManagers.Remove(this);
    //}

    //public string GetParameterValue(string Param)
    //{
    //    if (Param == "Level")
    //    {
    //        return GameManager.Instance.DataManager.GetLevel().ToString();
    //    }

    //    return null;
    //}
}