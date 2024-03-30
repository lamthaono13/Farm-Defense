using Common.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFSM : FSM
{
    public GameState CurrentGameState { get; private set; }

    private FSMState lobbyGameState;
    private LobbyAction lobbyGameAction;

    public FSMState loadingGameState;
    public LoadingAction loadingGameAction;

    public FSMState inGameState;
    private InGameAction inGameAction;

    private FSMState endGameState;
    private EndgameAction endGameAction;

    private FSMState reviveGameState;
    private ReviveAction reviveGameAction;

    private FSMState prestartGameState;
    private PreStartAction preStartAction;

    private FSMState preEndGameState;
    private PreEndGameAction preEndGameAction;

    public GameFSM(GameManager gameController) : base("Game FSM")
    {
        lobbyGameState = this.AddState((byte)GameState.LOBBY);
        loadingGameState = this.AddState((byte)GameState.LOADING);
        inGameState = this.AddState((byte)GameState.IN_GAME);
        endGameState = this.AddState((byte)GameState.END_GAME);
        reviveGameState = this.AddState((byte)GameState.REVIVE);
        prestartGameState = this.AddState((byte)GameState.PRESTART);
        preEndGameState = this.AddState((byte)GameState.PRE_ENDGAME);

        lobbyGameAction = new LobbyAction(gameController, lobbyGameState);
        loadingGameAction = new LoadingAction(gameController, loadingGameState);
        inGameAction = new InGameAction(gameController, inGameState);
        endGameAction = new EndgameAction(gameController, endGameState);
        reviveGameAction = new ReviveAction(gameController, reviveGameState);
        preStartAction = new PreStartAction(gameController, prestartGameState);
        preEndGameAction = new PreEndGameAction(gameController, preEndGameState);

        lobbyGameState.AddAction(lobbyGameAction);
        loadingGameState.AddAction(loadingGameAction);
        inGameState.AddAction(inGameAction);
        endGameState.AddAction(endGameAction);
        reviveGameState.AddAction(reviveGameAction);
        prestartGameState.AddAction(preStartAction);
        preEndGameState.AddAction(preEndGameAction);
    }

    public void ChangeState(GameState state)
    {
        //Crashlytics.Log($"Change from state {this.currentState} to {state}");
        switch (state)
        {
            case GameState.LOBBY:
                ChangeToState(lobbyGameState);
                CurrentGameState = GameState.LOBBY;
                break;
            case GameState.LOADING:
                ChangeToState(loadingGameState);
                CurrentGameState = GameState.LOADING;
                break;
            case GameState.IN_GAME:
                ChangeToState(inGameState);
                CurrentGameState = GameState.IN_GAME;
                break;
            case GameState.END_GAME:
                ChangeToState(endGameState);
                CurrentGameState = GameState.END_GAME;
                break;
            case GameState.REVIVE:
                ChangeToState(reviveGameState);
                CurrentGameState = GameState.REVIVE;
                break;
            case GameState.PRESTART:
                ChangeToState(prestartGameState);
                CurrentGameState = GameState.PRESTART;
                break;
            case GameState.PRE_ENDGAME:
                ChangeToState(preEndGameState);
                CurrentGameState = GameState.PRE_ENDGAME;
                break;
            default:
                break;
        }
    }
}

public enum GameState
{
    LOBBY,
    LOADING,
    PRESTART,
    IN_GAME,
    END_GAME,
    REVIVE,
    PRE_ENDGAME
}
