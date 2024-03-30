using Common.FSM;
using UnityEngine;

public class ReviveAction : FSMAction
{
    private readonly GameManager gameManager;
    private const float timeCountDown = 3;
    private float timeCountDownLeft;

    public ReviveAction(GameManager _gameManager, FSMState owner) : base(owner)
    {
        gameManager = _gameManager;
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