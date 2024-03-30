using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using Cysharp.Threading.Tasks;

public abstract class LevelManager<T> : MonoBehaviour, IManager
{
    [SerializeField] protected TypeLevel typeLevel;

    public TypeLevel TypeLevel => typeLevel;

    public static T Instance;

    public GameResult gameResult;

    [SerializeField] private float timeWaitToLose;

    [SerializeField] private float timeWaitToWin;

    [SerializeField] private List<GameObject> singletons;
    
    #region Event

    public delegate void Pause();

    public delegate void Remuse();

    public delegate void StartLevel();

    public delegate void EndGame(GameResult gameResult);

    public delegate void Revive();

    public Pause PauseEvent;

    public Remuse RemuseEvent;

    public StartLevel StartLevelEvent;

    public EndGame EndGameEvent;

    public Revive ReviveEvent;

    #endregion

    protected abstract void InitSingleton(T t);

    protected async virtual void Awake()
    {
        gameResult = GameResult.NoDeciced;

        for (int i = 0; i < singletons.Count; i++)
        {
            singletons[i].GetComponent<ISingleton>().InitSingleton();
        }
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {

    }

    public void OnPause()
    {
        PauseEvent?.Invoke();
    }

    public void OnRemuse()
    {
        RemuseEvent?.Invoke();
    }

    public void OnStartLevel()
    {
        StartLevelEvent?.Invoke();
    }

    public void OnEndGame(GameResult _gameResult)
    {
        if(gameResult != GameResult.NoDeciced)
        {
            return;
        }

        switch (_gameResult)
        {
            case GameResult.Lose:

                OnFreezeGame(true);

                DOTween.To((x) => { }, 0, 10, timeWaitToLose).SetUpdate(true).OnComplete(() => 
                {
                    EndGameEvent?.Invoke(_gameResult);
                });

                break;
            case GameResult.Win:
                DOTween.To((x) => { }, 0, 10, timeWaitToWin).SetUpdate(true).OnComplete(() =>
                {
                    EndGameEvent?.Invoke(_gameResult);
                });
                break;
        }



        gameResult = _gameResult;

        GameManager.Instance.ChangeState(GameState.END_GAME, gameResult.ToString());
    }

    IEnumerator WaitToWin()
    {
        yield return new WaitForSeconds(timeWaitToWin);
    }

    IEnumerator WaitToLose()
    {
        yield return new WaitForSeconds(timeWaitToLose);
    }

    public virtual void OnRevive()
    {
        gameResult = GameResult.NoDeciced;

        OnFreezeGame(false);

        ReviveEvent?.Invoke();
    }

    public void OnFreezeGame(bool isFreeze)
    {
        Time.timeScale = isFreeze ? 0 : 1;
    }
}

public enum TypeLevel
{
    MainGame,
    ModeSurvival,
    BonusGame
}

public enum GameResult
{
    NoDeciced,
    Lose,
    Win
}
