using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManagerMainGame : MonoBehaviour
{
    [SerializeField] private UiChooseChar uiChooseChar;



    [SerializeField] private UiLose uiLose;

    [SerializeField] private UiWin uiWin;

    [SerializeField] private UiRevive uiRevive;

    [SerializeField] private UiReviveResume uiReviveResume;

    [SerializeField] private UiTutorial uiTutorial;

    [SerializeField] private UiHealthBoss uiHealthBoss;

    [SerializeField] private UiLevel uiLevel;

    [SerializeField] private Canvas canvasHide;

    [SerializeField] private UiEnergy uiEnergy;

    [SerializeField] private GameObject btnSetting;

    public UiLose UiLose => uiLose;

    public UiWin UiWin => uiWin;

    public UiRevive UiRevive => uiRevive;

    public UiReviveResume UiReviveResume => uiReviveResume;

    public UiTutorial UiTutorial => uiTutorial;

    public UiHealthBoss UiHealthBoss => uiHealthBoss;

    private bool hasEnterRevive;

    private bool isShowUiEnergy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Init()
    {
        uiChooseChar.Init();

        uiLevel.Init();

        isShowUiEnergy = true;

        int currentLevel = GameManager.Instance.DataManager.GetLevel();

        LevelManagerMainGame.Instance.EndGameEvent += (a) =>
        {
            if (!hasEnterRevive)
            {
                hasEnterRevive = true;
                uiRevive.OnEndgame(a);
            }
            else
            {
                uiLose.OnEndgame(a);
            }

            canvasHide.enabled = false;
        };

        LevelManagerMainGame.Instance.EndGameEvent += uiWin.OnEndgame;

        LevelManagerMainGame.Instance.ReviveEvent += () => { canvasHide.enabled = true; };


        if (currentLevel == 1 && !GameManager.Instance.NoTutorial && !GameManager.Instance.DataManager.GetHasTutorialChooseChar())
        {
            uiTutorial.StartTutorialChoose();
        }
    }

    private void LogEndGame(GameResult gameResult)
    {
        int currentLevel = GameManager.Instance.DataManager.GetLevel();

        if(currentLevel == 1)
        {
            HandleAppsflyer.Instance.LogEventTutorial("Tutorial_Level_1", "0");
        }

        switch (gameResult)
        {
            case GameResult.Lose:


                break;
            case GameResult.Win:

                //  HandleFireBase.Instance.LogEventComplete(currentLevel, (int)LevelManagerMainGame.Instance.GetTimePlay());

                break;
        }
    }

    public void TurnUi()
    {
        canvasHide.enabled = !canvasHide.enabled;
    }

    public void TurnBtnSetting()
    {
        btnSetting.gameObject.SetActive(!btnSetting.gameObject.activeSelf);
    }

    public void TurnUiEnergy()
    {
        isShowUiEnergy = !isShowUiEnergy;

        uiEnergy.Show(isShowUiEnergy);
    }
}