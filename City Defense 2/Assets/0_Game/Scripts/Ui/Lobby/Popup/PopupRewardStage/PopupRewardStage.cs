using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupRewardStage : UiCanvas
{
    [SerializeField] private List<DataRewardMap> dataRewardMaps;

    [SerializeField] private TextMeshProUGUI textStar;

    [SerializeField] private List<ElementQuestRewardStage> elementQuestRewardStages;

    [SerializeField] private UITabStageLobby uITabStageLobby;

    public override void Show(bool _isShow)
    {
        base.Show(_isShow);

        if (_isShow)
        {
            Init();
        }
        else
        {
            uITabStageLobby.InitChestWarning();
        }
    }

    public override void Init()
    {
        base.Init();

        int idMap = LobbyManager.Instance.GetCurrentIdMapStage();

        int currentStar = GameManager.Instance.DataManager.GetStarInMap(idMap);

        textStar.text = currentStar.ToString() + "/45";



        for(int i = 0; i < elementQuestRewardStages.Count; i++)
        {
            DataEachRewardStage dataEachRewardStage = dataRewardMaps[idMap - 1].DataEachRewardStages[i];

            bool a = GameManager.Instance.DataManager.CheckGetRewardInMap(dataEachRewardStage.StarNeed, idMap);

            if (currentStar < dataEachRewardStage.StarNeed)
            {
                elementQuestRewardStages[i].Init(dataEachRewardStage, this, TypeElementQuestRewardStage.Nomarl);
            }
            else
            {
                if (a)
                {
                    elementQuestRewardStages[i].Init(dataEachRewardStage, this, TypeElementQuestRewardStage.HasGet);
                }
                else
                {
                    elementQuestRewardStages[i].Init(dataEachRewardStage, this, TypeElementQuestRewardStage.CanGet);
                }
            }




        }
    }

    public void OnClickBtnClose()
    {
        int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        bool b = GameManager.Instance.DataManager.GetHasTutorialLobbyLv6();

        if (levelUnlock == 6 && !b)
        {
            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUpgradeUnit();
        }

        Show(false);
    }
}