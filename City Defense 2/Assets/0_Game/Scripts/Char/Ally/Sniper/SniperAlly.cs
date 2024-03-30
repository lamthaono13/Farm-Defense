using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAlly : Ally
{
    //[SerializeField] private float speedAttack;

    //private bool isShooting;

    public override void OnEnterState(StateChar stateChar)
    {
        base.OnEnterState(stateChar);

        switch (stateChar)
        {
            case StateChar.Idle:
                break;
            case StateChar.Run:

                render.Animation.Animate(TypeAnimation.Run, 1, true, null, null);
                
                break;
            case StateChar.FindTarget:

                currentTimeCheckUpdate = Random.Range(0, configTimeCheckUpdate);

                movement.StopMove();

                render.Animation.Animate(TypeAnimation.Idle, 1, true, null, null);

                break;
            case StateChar.Attack:

                render.Animation.Animate(TypeAnimation.Idle, 1, true, null, null);

                movement.StopMove();

                break;
            case StateChar.Die:

                break;
            case StateChar.Revive:
                break;
        }
    }

    public override void OnExitState(StateChar stateChar)
    {
        base.OnExitState(stateChar);

        switch (stateChar)
        {
            case StateChar.Idle:
                break;
            case StateChar.Run:
                break;
            case StateChar.FindTarget:
                break;
            case StateChar.Attack:
                break;
            case StateChar.Die:
                break;
            case StateChar.Revive:
                break;
        }
    }

    public override void OnUpdateState(StateChar stateChar)
    {
        base.OnUpdateState(stateChar);

        switch (stateChar)
        {
            case StateChar.Idle:
                break;
            case StateChar.Run:
                
                bool c = CheckAliveTarget();

                if (!c)
                {
                    ChangeState(StateChar.FindTarget);

                    //currentTimeAnimation = 0;

                    return;
                }

                if (target.GetBody().position.x < transform.position.x)
                {
                    render.SetFlip(true);
                }
                else
                {
                    render.SetFlip(false);
                }

                movement.StartMove(TypeMove.Destination, target.GetBody().position);

                currentTimeAnimation += Time.deltaTime;
                
                if(Vector3.Distance(transform.position, target.GetBody().position) <= rangeAttack)
                {
                    ChangeState(StateChar.Attack);

                    //currentTimeAnimation = 0;
                            
                }
                
                break;
            case StateChar.FindTarget:

                if(currentTimeCheckUpdate >= configTimeCheckUpdate)
                {
                    bool a = CheckTargetPriority(TypeGroup.Gunner);

                    currentTimeAnimation += Time.deltaTime;

                    if (a)
                    {
                        if(Vector3.Distance(transform.position, target.GetBody().position) > rangeAttack)
                        {
                            ChangeState(StateChar.Run);

                            //currentTimeAnimation = 0;
                            
                        }
                        else
                        {
                            ChangeState(StateChar.Attack);
                        }
                    }
                }
                else
                {
                    currentTimeCheckUpdate += Time.deltaTime;
                }



                break;
            case StateChar.Attack:

                bool b = CheckAliveTarget();

                if (!b)
                {
                    ChangeState(StateChar.FindTarget);
                    
                    return;
                }

                if(Vector3.Distance(transform.position, target.GetBody().position) > rangeAttack)
                {
                    ChangeState(StateChar.FindTarget);

                    //currentTimeAnimation = 0;

                    return;
                }
                

                if(currentTimeAnimation >= speedAttack)
                {
                    currentTimeAnimation = 0;

                    render.Animation.Animate(TypeAnimation.Attack, speedAttack / render.Animation.GetTimeAnimation(TypeAnimation.Attack, false), false, () => { weapon.Attack(target); }, () => { ChangeState(StateChar.Attack); });

                    
                }
                else
                {
                    currentTimeAnimation += Time.deltaTime;
                }

                if (target.GetBody().position.x < transform.position.x)
                {
                    render.SetFlip(true);
                }
                else
                {
                    render.SetFlip(false);
                }

                break;
            case StateChar.Die:
                break;
            case StateChar.Revive:
                break;
        }
    }
}
