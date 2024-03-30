using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTutorial : MonoBehaviour
{
    private int idTutorialChoose;

    [SerializeField] private List<GameObject> listObjTutorialChoose;

    [SerializeField] private GameObject objTutorialNext;

    [SerializeField] private GameObject objTutorialHome;

    [SerializeField] private GameObject objTutorialReplay;

    [SerializeField] private GameObject objTutorialBoom;

    [SerializeField] private GameObject objTutorialHero;

    [SerializeField] private UiCanvas uiCanvasBoom;

    [SerializeField] private UiCanvas uiCanvasHero;

    [SerializeField] private GameObject objTutorialLv4;

    private bool isTutorialChoose;

    public bool isTutorialBoom;

    public int idTutLog;

    //[SerializeField] private GameObject objTextHero;

    //[SerializeField] private GameObject objRewardHero;

    private void Start()
    {
        idTutLog = 1;

        //int currentLevel = GameManager.Instance.DataManager.GetLevel();

        //if (currentLevel == 8 && !GameManager.Instance.NoTutorial)
        //{
        //    StartTutorialHero();
        //}
        //else
        //{

        //}
    }

    public void Init()
    {

    }

    public void StartTutorialNextUiWin()
    {
        objTutorialNext.gameObject.SetActive(true);
    }

    public void StartTutorialHomeUi()
    {
        objTutorialHome.gameObject.SetActive(true);
    }


    public void StartTutorialReplayUi()
    {
        objTutorialReplay.gameObject.SetActive(true);
    }

    public void StartTutorialChoose()
    {
        isTutorialChoose = true;

        idTutorialChoose = 0;

        HandleFireBase.Instance.LogEventWithString("Tut" + (idTutorialChoose + idTutLog).ToString());

        Time.timeScale = 0;

        for (int i = 0; i < listObjTutorialChoose.Count; i++) 
        {
            listObjTutorialChoose[i].gameObject.SetActive(false);
        }

        if (idTutorialChoose == 0 || idTutorialChoose == 2 || idTutorialChoose == 4)
        {
            EnergyManager.Instance.AddEnergy(1000000);
        }

        listObjTutorialChoose[idTutorialChoose].gameObject.SetActive(true);
    }

    public void StartTutorialBoom()
    {
        isTutorialBoom = true;

        GameManager.Instance.SetTimeScale(0);

        if (GameManager.Instance.DataManager.GetBoom() <= 0)
        {
            GameManager.Instance.DataManager.AddBoom(1);
        }

        HandleFireBase.Instance.LogEventWithString("Tut25");

        uiCanvasBoom.Show(true);

        objTutorialBoom.gameObject.SetActive(true);
    }

    public void StopTutorialBoom()
    {
        //isTutorialBoom = false;

        objTutorialBoom.gameObject.SetActive(false);
    }

    public void StartTutorialHero()
    {
        GameManager.Instance.SetTimeScale(0);

        //if (GameManager.Instance.DataManager.GetNumberHero() <= 0)
        //{
        //    GameManager.Instance.DataManager.AddNumberHero(1);
        //}

        uiCanvasHero.Show(true);

        objTutorialHero.gameObject.SetActive(true);
    }

    public void StopTutorialHero()
    {
        objTutorialHero.gameObject.SetActive(false);
    }

    public void ChangeActionChoose()
    {
        if (!isTutorialChoose)
        {
            return;
        }

        idTutorialChoose ++;

        for (int i = 0; i < listObjTutorialChoose.Count; i++)
        {
            listObjTutorialChoose[i].gameObject.SetActive(false);
        }

        if (idTutorialChoose >= listObjTutorialChoose.Count)
        {
            isTutorialChoose = false;

            GameManager.Instance.DataManager.SetHasTutorialChooseChar();

            GameManager.Instance.OnDoneTutorialLevel1();

            //Debug.Log("Dang yeu");

            Time.timeScale = 1;

            return;
        }

        HandleFireBase.Instance.LogEventWithString("Tut" + (idTutorialChoose + idTutLog).ToString());

        if (idTutorialChoose == 0 || idTutorialChoose == 2 || idTutorialChoose == 4)
        {
            EnergyManager.Instance.SetEnergy(1000000);
        }

        listObjTutorialChoose[idTutorialChoose].gameObject.SetActive(true);
    }

    public void ActiveTutorialLevel4(bool isShow)
    {
        objTutorialLv4.gameObject.SetActive(isShow);

        if (isShow)
        {
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("Dang yeu");
            Time.timeScale = 1;
        }
    }
}