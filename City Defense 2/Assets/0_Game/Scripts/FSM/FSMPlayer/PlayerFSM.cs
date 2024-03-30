//using Common.FSM;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerFSM : FSM
//{
//    public PlayerState CurrentPlayerState { get; private set; }

//    private FSMState playerLobbyState;
//    private PlayerLobbyAction playerLobbyAction;

//    public FSMState playerRunState;
//    private PlayerRunAction playerRunAction;

//    private FSMState playerAttackState;
//    private PlayerAttackAction playerAttackAction;

//    private FSMState playerEndGameState;
//    private PlayerEndGameAction playerEndGameAction;

//    public PlayerFSM(Player player) : base("Game FSM")
//    {
//        playerLobbyState = this.AddState((byte)PlayerState.Lobby);
//        playerRunState = this.AddState((byte)PlayerState.Run);
//        playerAttackState = this.AddState((byte)PlayerState.Attack);
//        playerEndGameState = this.AddState((byte)PlayerState.EndGame);

//        playerLobbyAction = new PlayerLobbyAction(player, playerLobbyState);
//        playerRunAction = new PlayerRunAction(player, playerRunState);
//        playerAttackAction = new PlayerAttackAction(player, playerAttackState);
//        playerEndGameAction = new PlayerEndGameAction(player, playerEndGameState);

//        playerLobbyState.AddAction(playerLobbyAction);
//        playerRunState.AddAction(playerRunAction);
//        playerAttackState.AddAction(playerAttackAction);
//        playerEndGameState.AddAction(playerEndGameAction);
//    }

//    public void ChangeState(PlayerState state)
//    {
//        switch (state)
//        {
//            case PlayerState.Lobby:
//                ChangeToState(playerLobbyState);
//                CurrentPlayerState = PlayerState.Lobby;
//                break;
//            case PlayerState.Run:
//                ChangeToState(playerRunState);
//                CurrentPlayerState = PlayerState.Run;
//                break;
//            case PlayerState.Attack:
//                ChangeToState(playerAttackState);
//                CurrentPlayerState = PlayerState.Attack;
//                break;
//            case PlayerState.EndGame:
//                ChangeToState(playerEndGameState);
//                CurrentPlayerState = PlayerState.EndGame;
//                break;
//            default:
//                break;
//        }
//    }
//}

//public enum PlayerState
//{
//    Lobby,
//    Run,
//    Attack,
//    EndGame
//}
