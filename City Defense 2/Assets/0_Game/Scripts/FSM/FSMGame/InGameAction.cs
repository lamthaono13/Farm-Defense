using Common.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameAction : FSMAction
{
    private readonly GameManager gameManager;

    public InGameAction(GameManager _gameController, FSMState owner) : base(owner)
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
