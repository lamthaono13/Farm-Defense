using Common.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharStateFSM;

public class CharFSM : FSM
{
    public StateChar CurrentCharState { get; private set; }

    private FSMState idleCharState;

    private IdleAction idleAction;

    private FSMState runCharState;

    private RunAction runAction;

    private FSMState findTargetCharState;

    private FindTargetAction findTargetAction;

    private FSMState attackCharState;

    private AttackAction attackAction;

    private FSMState flyCharState;

    private FlyAction flyAction;

    private FSMState skillCharState;

    private SkillAction skillAction;

    private FSMState dieState;

    private DieAction dieAction;

    private FSMState reviveState;

    private ReviveCharAction reviveAction;

    private FSMState stunState;

    private StunAction stunAction;

    protected FSMState destroyState;

    protected DestroyAction destroyAction;

    public CharFSM(CharacterBase _characterBase) : base("Char FSM")
    {
        idleCharState = this.AddState((byte)StateChar.Idle);
        runCharState = this.AddState((byte)StateChar.Run);
        findTargetCharState = this.AddState((byte)StateChar.FindTarget);
        attackCharState = this.AddState((byte)StateChar.Attack);
        flyCharState = this.AddState((byte)StateChar.Fly);
        skillCharState = this.AddState((byte)StateChar.Skill);
        dieState = this.AddState((byte)StateChar.Die);
        reviveState = this.AddState((byte)StateChar.Revive);
        stunState = this.AddState((byte)StateChar.Stun);
        destroyState = this.AddState((byte)StateChar.Destroy);

        idleAction = new IdleAction(_characterBase, idleCharState);
        runAction = new RunAction(_characterBase, runCharState);
        findTargetAction = new FindTargetAction(_characterBase, findTargetCharState);
        attackAction = new AttackAction(_characterBase, attackCharState);
        flyAction = new FlyAction(_characterBase, flyCharState);
        skillAction = new SkillAction(_characterBase, skillCharState);
        dieAction = new DieAction(_characterBase, dieState);
        reviveAction = new ReviveCharAction(_characterBase, reviveState);
        stunAction = new StunAction(_characterBase, stunState);
        destroyAction = new DestroyAction(_characterBase, destroyState);

        idleCharState.AddAction(idleAction);
        runCharState.AddAction(runAction);
        findTargetCharState.AddAction(findTargetAction);
        attackCharState.AddAction(attackAction);
        flyCharState.AddAction(flyAction);
        skillCharState.AddAction(skillAction);
        dieState.AddAction(dieAction);
        reviveState.AddAction(reviveAction);
        stunState.AddAction(stunAction);
        destroyState.AddAction(destroyAction);
    }

    public void ChangeState(StateChar state)
    {
        CurrentCharState = state;

        switch (state)
        {
            case StateChar.Idle:
                ChangeToState(idleCharState);
                break;
            case StateChar.Run:
                ChangeToState(runCharState);
                break;
            case StateChar.FindTarget:
                ChangeToState(findTargetCharState);
                break;
            case StateChar.Attack:
                ChangeToState(attackCharState);
                break;
            case StateChar.Fly:
                ChangeToState(flyCharState);
                break;
            case StateChar.Skill:
                ChangeToState(skillCharState);
                break;
            case StateChar.Die:
                ChangeToState(dieState);
                break;
            case StateChar.Revive:
                ChangeToState(reviveState);
                break;
            case StateChar.Stun:
                ChangeToState(stunState);
                break;
            case StateChar.Destroy:
                ChangeToState(destroyState);
                break;
            default:
                break;
        }
    }
}

public enum StateChar
{
    Idle,
    Run,
    FindTarget,
    Attack,
    Fly,
    Die,
    Skill,
    Revive,
    Stun,
    Destroy
}
