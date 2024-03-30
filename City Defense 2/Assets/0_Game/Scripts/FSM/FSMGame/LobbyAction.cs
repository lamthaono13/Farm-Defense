using Common.FSM;
using UnityEngine;

public class LobbyAction : FSMAction
{
    private readonly GameManager gameManager;

    public LobbyAction(GameManager _gameController, FSMState owner) : base(owner)
    {
        gameManager = _gameController;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}