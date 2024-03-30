using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

public class LevelManagerMainGame : LevelManager<LevelManagerMainGame>
{
    [SerializeField] protected float timeWaitLoadDataDone;

    [SerializeField] protected UiManagerMainGame _uiManagerMainGame;

    [SerializeField] protected CharManager _charManager;

    [SerializeField] protected EnergyManager _energyManager;

    [SerializeField] protected Pooling _pooling;

    [SerializeField] protected ObjectManager _objectManager;

    [SerializeField] protected NavMeshManager _navMeshManager;

    [SerializeField] protected HealthGamePlay _healthGamePlay;

    [SerializeField] protected QuestManager questManager;

    public UiManagerMainGame UiManagerMainGame => _uiManagerMainGame;

    public QuestManager QuestManager => questManager;

    private DataLevel dataLevel;

    private float timePlay;

    protected override void Awake()
    {
        InitSingleton(this);
        
        base.Awake();

        StartCoroutine(WaitForLoadingData());

        InitAll();
    }

    protected override void Start()
    {
        base.Start();

        GameManager.Instance.SoundManager.PlaySoundInGame(true);
    }

    IEnumerator WaitForLoadingData()
    {
        yield return new WaitForSecondsRealtime(timeWaitLoadDataDone);
        GameManager.Instance.OnLoadingSceneDone(GameState.IN_GAME);
    }

    protected override void InitSingleton(LevelManagerMainGame t)
    {
        Instance = this;
    }

    protected override void Update()
    {
        base.Update();

        timePlay += Time.deltaTime;
    }

    public void InitAll()
    {
        int currentLevel = GameManager.Instance.DataManager.GetLevel();

        int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        // Log

        if(levelUnlock == currentLevel)
        {
            HandleFireBase.Instance.LogEventStart(currentLevel);
        }
        else
        {
            HandleFireBase.Instance.LogEventWithParameter("Level_Start_Play_Again", new FirebaseParam[]{ new FirebaseParam("Level", currentLevel) });
        }

        // Init

        dataLevel = GameManager.Instance.DataManager.DataManagerMainGame.DataGame.DataLevels[currentLevel - 1];

        _pooling.Init();
        
        _energyManager.Init(dataLevel.Energy, dataLevel.isEnergySub);
        
        _objectManager.Init(dataLevel.DataMap);
        
        _charManager.SqawnSystem.Init(dataLevel, 0);

        _navMeshManager.Build((int)dataLevel.DataMap.TypeShapeMap);

        _healthGamePlay.Init(GameManager.Instance.DataManager.DataManagerMainGame.DataGame.HealthInGame);

        _uiManagerMainGame.Init();

        List<TypeQuestGame> typeQuestGames = new List<TypeQuestGame>();

        typeQuestGames.Add((TypeQuestGame)(dataLevel.IndexQuest_1));

        typeQuestGames.Add((TypeQuestGame)(dataLevel.IndexQuest_2));

        questManager.Init(typeQuestGames);
    }

    public int GetGoldLevel()
    {
        return (int)dataLevel.GoldDrop;
    }

    public int GetGemLevel()
    {
        return (int)dataLevel.gemDrop;
    }

    public override void OnRevive()
    {
        base.OnRevive();
    }

    public int GetTimePlay()
    {
        return (int)timePlay;
    }
}