using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTutorialLobby : MonoBehaviour
{
    [SerializeField] private List<GameObject> objTutorialUpgradeUnit;

    [SerializeField] private List<GameObject> objTutorialFreeShop;

    [SerializeField] private List<GameObject> objTutorialUnlockUnit;

    [SerializeField] private List<GameObject> objTutorialRewardStage;

    [SerializeField] private List<GameObject> objTutorialUpgradeLoseUnit;

    [SerializeField] private GroupListCard groupListCard;

    [SerializeField] private RectTransform objParentTutorial;

    private int countTutorialUpgradeUnit;

    private int countTutorialFreeShop;

    private int countTutorialUnlockUnit;

    private int countTutorialRewardStage;

    private int countTutorialUpgradeLoseUnit;

    private float positionScrollUnlock;

    public int idTutLog;

    // Start is called before the first frame update
    void Start()
    {
        positionScrollUnlock = -254;

        int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        bool a = GameManager.Instance.DataManager.GetHasTutorialLobbyLv3();

        if (levelUnlock == 3 && !a)
        {
            StartActionTutorialUnlockUnit();
        }

        bool b = GameManager.Instance.DataManager.GetHasTutorialLobbyLv6();

        if(levelUnlock == 6 && !b)
        {
            StartActionTutorialUpgradeUnit();
        }

        if(levelUnlock < 6 && levelUnlock > 3 && GameManager.Instance.DataManager.GetIsTutorialUpgradeLobby() && !GameManager.Instance.DataManager.GetHasTutorialUpgrade())
        {
            StartActionTutorialUpgradeLose();
        }
    }

    public void StartActionTutorialUpgradeUnit()
    {
        if (!GameManager.Instance.DataManager.GetHasTutorialUpgrade())
        {
            countTutorialUpgradeUnit = 0;

            idTutLog = 13;
        }
        else
        {
            countTutorialUpgradeUnit = 9;

            idTutLog = 22;
        }

        HandleFireBase.Instance.LogEventWithString("Tut" + (countTutorialUpgradeUnit + idTutLog).ToString());

        if (countTutorialUpgradeUnit == 1)
        {
            objTutorialUpgradeUnit[countTutorialUpgradeUnit].transform.parent = objParentTutorial;
        }

        if (countTutorialUpgradeUnit == 9 || countTutorialUpgradeUnit == 10)
        {
            objTutorialUpgradeUnit[countTutorialUpgradeUnit].transform.parent = objParentTutorial;
        }

        objTutorialUpgradeUnit[countTutorialUpgradeUnit].gameObject.SetActive(true);
    }

    public void OnChangeTutorialUpgradeUnit()
    {
        countTutorialUpgradeUnit++;



        if (countTutorialUpgradeUnit == 1)
        {
            objTutorialUpgradeUnit[countTutorialUpgradeUnit].transform.parent = objParentTutorial;
        }

        if (countTutorialUpgradeUnit == 9 || countTutorialUpgradeUnit == 10)
        {
            objTutorialUpgradeUnit[countTutorialUpgradeUnit].transform.parent = objParentTutorial;
        }

        if (countTutorialUpgradeUnit >= objTutorialUpgradeUnit.Count)
        {
            GameManager.Instance.DataManager.SetHasTutorialLobbyLv6();

            GameManager.Instance.OnDoneTutorialLevel6();

            for (int i = 0; i < objTutorialUpgradeUnit.Count; i++)
            {
                if (i == countTutorialUpgradeUnit)
                {
                    objTutorialUpgradeUnit[i].gameObject.SetActive(true);
                }
                else
                {
                    objTutorialUpgradeUnit[i].gameObject.SetActive(false);
                }
            }

            return;
        }

        HandleFireBase.Instance.LogEventWithString("Tut" + (countTutorialUpgradeUnit + idTutLog).ToString());

        for (int i = 0; i < objTutorialUpgradeUnit.Count; i++)
        {
            if(i == countTutorialUpgradeUnit)
            {
                objTutorialUpgradeUnit[i].gameObject.SetActive(true);
            }
            else
            {
                objTutorialUpgradeUnit[i].gameObject.SetActive(false);
            }
        }
    }

    public void StartActionTutorialFreeShop()
    {
        countTutorialFreeShop = 0;

        objTutorialFreeShop[countTutorialFreeShop].gameObject.SetActive(true);
    }

    public void OnChangeTutorialFreeShop()
    {
        countTutorialFreeShop++;

        for (int i = 0; i < objTutorialFreeShop.Count; i++)
        {
            if (i == countTutorialFreeShop)
            {
                objTutorialFreeShop[i].gameObject.SetActive(true);
            }
            else
            {
                objTutorialFreeShop[i].gameObject.SetActive(false);
            }
        }
    }

    public void StartActionTutorialUpgradeLose()
    {
        countTutorialUpgradeLoseUnit = 0;

        objTutorialUpgradeLoseUnit[countTutorialFreeShop].gameObject.SetActive(true);
    }

    public void OnChangeTutorialUpgradeLose()
    {
        countTutorialUpgradeLoseUnit++;

        if (countTutorialUpgradeLoseUnit >= objTutorialUpgradeLoseUnit.Count)
        {
            GameManager.Instance.DataManager.SetHasTutorialUpgrade();

            GameManager.Instance.OnDoneTutorialUpgrade();
        }

        if (countTutorialUpgradeLoseUnit == 1 || countTutorialUpgradeLoseUnit == 9 || countTutorialUpgradeLoseUnit == 10)
        {
            objTutorialUpgradeLoseUnit[countTutorialUpgradeLoseUnit].transform.parent = objParentTutorial;
        }

        for (int i = 0; i < objTutorialUpgradeLoseUnit.Count; i++)
        {
            if (i == countTutorialUpgradeLoseUnit)
            {
                objTutorialUpgradeLoseUnit[i].gameObject.SetActive(true);
            }
            else
            {
                objTutorialUpgradeLoseUnit[i].gameObject.SetActive(false);
            }
        }
    }

    //public void StartActionTutorialRewardStage()
    //{
    //    countTutorialRewardStage = 0;

    //    objTutorialRewardStage[countTutorialRewardStage].gameObject.SetActive(true);
    //}

    //public void OnChangeTutorialRewardStage()
    //{
    //    countTutorialUpgradeLoseUnit++;

    //    if (countTutorialUpgradeLoseUnit >= objTutorialUpgradeLoseUnit.Count)
    //    {
    //        GameManager.Instance.DataManager.SetHasTutorialUpgrade();
    //    }

    //    if (countTutorialUpgradeLoseUnit == 1 || countTutorialUpgradeLoseUnit == 9 || countTutorialUpgradeLoseUnit == 10)
    //    {
    //        objTutorialUpgradeLoseUnit[countTutorialUpgradeLoseUnit].transform.parent = objParentTutorial;
    //    }

    //    for (int i = 0; i < objTutorialUpgradeLoseUnit.Count; i++)
    //    {
    //        if (i == countTutorialUpgradeLoseUnit)
    //        {
    //            objTutorialUpgradeLoseUnit[i].gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            objTutorialUpgradeLoseUnit[i].gameObject.SetActive(false);
    //        }
    //    }
    //}

    public void StartActionTutorialUnlockUnit()
    {
        countTutorialUnlockUnit = 0;

        idTutLog = 5;

        HandleFireBase.Instance.LogEventWithString("Tut" + (countTutorialUnlockUnit + idTutLog).ToString());

        objTutorialUnlockUnit[countTutorialUnlockUnit].gameObject.SetActive(true);
    }

    public void OnChangeTutorialUnlockUnit()
    {
        countTutorialUnlockUnit++;



        if (countTutorialUnlockUnit == 1)
        {
            //groupListCard.OnTutorial(positionScrollUnlock);

            objTutorialUnlockUnit[countTutorialUnlockUnit].transform.parent = objParentTutorial;

            //objTutorialUnlockUnit[countTutorialUnlockUnit].transform.position = groupListCard.GetTransformTutorialLevel3().position;
        }

        if(countTutorialUnlockUnit == 2)
        {
            //groupListCard.OnTutorialDone();
        }

        if (countTutorialUnlockUnit == 4)
        {
            GameManager.Instance.DataManager.SetEquidTutLv3();

            StartCoroutine(WaitCongratulation());
        }

        if (countTutorialUnlockUnit == 6 || countTutorialUnlockUnit == 7 || countTutorialUnlockUnit == 4)
        {
            objTutorialUnlockUnit[countTutorialUnlockUnit].transform.SetParent(objParentTutorial);

            //objTutorialUnlockUnit[countTutorialUnlockUnit].GetComponent<RectTransform>().anchoredPosition = groupListCard.GetTransformTutorialLevel3().GetComponent<RectTransform>().anchoredPosition;
        }

        if(countTutorialUnlockUnit >= objTutorialUnlockUnit.Count)
        {
            GameManager.Instance.DataManager.SetHasTutorialLobbyLv3();

            GameManager.Instance.OnDoneTutorialUnlockLobbyLevel3();

            for (int i = 0; i < objTutorialUnlockUnit.Count; i++)
            {
                if (i == countTutorialUnlockUnit)
                {
                    objTutorialUnlockUnit[i].gameObject.SetActive(true);
                }
                else
                {
                    objTutorialUnlockUnit[i].gameObject.SetActive(false);
                }
            }

            return;
        }

        HandleFireBase.Instance.LogEventWithString("Tut" + (countTutorialUnlockUnit + idTutLog).ToString());

        for (int i = 0; i < objTutorialUnlockUnit.Count; i++)
        {
            if (i == countTutorialUnlockUnit)
            {
                objTutorialUnlockUnit[i].gameObject.SetActive(true);
            }
            else
            {
                objTutorialUnlockUnit[i].gameObject.SetActive(false);
            }
        }
    }

    IEnumerator WaitCongratulation()
    {
        yield return new WaitForSeconds(1);
        OnChangeTutorialUnlockUnit();
    }
}
