using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardAlly : Ally
{
   public override void Init(TypeSlotEquip _typeSlotEquip = TypeSlotEquip.Slot1)
    {
        base.Init(_typeSlotEquip);
        currentTimeAnimation = speedAttack;
    }

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

                bool b = CheckAliveTarget();

                if (!b)
                {
                    ChangeState(StateChar.FindTarget);

                    //currentTimeAnimation = 0;

                    return;
                }

                movement.StopMove();

                render.Animation.Animate(TypeAnimation.Idle, 1, true, null, null);

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

                Debug.Log(c);

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

                CheckTargetOnGround();

                movement.StartMove(TypeMove.Destination, target.GetBody().position);

                currentTimeAnimation += Time.deltaTime;

                break;
            case StateChar.FindTarget:

                if(currentTimeCheckUpdate >= configTimeCheckUpdate)
                {
                    bool a = CheckTargetOnGround();

                    if (a)
                    {
                        ChangeState(StateChar.Run);
                    }
                }
                else
                {
                    currentTimeCheckUpdate += Time.deltaTime;
                }

                currentTimeAnimation += Time.deltaTime;

                break;
            case StateChar.Attack:

                bool b = CheckAliveTarget();

                if (!b)
                {
                    ChangeState(StateChar.FindTarget);

                    //currentTimeAnimation = 0;

                    return;
                }

                if (Vector3.Distance(target.GetBody().position, GetBody().position) > rangeAttack)
                {
                    ChangeState(StateChar.FindTarget);

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



                //if(currentTimeAnimation >= timeAttack && !isAttacking)
                //{
                //    isAttacking = true;
                //}

                if (currentTimeAnimation >= speedAttack)
                {
                    currentTimeAnimation = 0;

                    render.Animation.Animate(TypeAnimation.Attack, speedAttack / render.Animation.GetTimeAnimation(TypeAnimation.Attack, false), false, () => { weapon.Attack(target); }, () => { ChangeState(StateChar.Attack); });
                    
                    //ChangeState(StateChar.Attack);

                    //ChangeState(StateChar.Attack);
                }
                else
                {
                    currentTimeAnimation += Time.deltaTime;
                }

                break;
            case StateChar.Die:
                break;
            case StateChar.Revive:
                break;
        }
    }

    public override void OnTriggerStayChildDetect(Collider2D collider)
    {
        base.OnTriggerStayChildDetect(collider);

        if (collider.CompareTag("Enermy"))
        {
            if(state != StateChar.Attack)
            {
                IContactObject iContactObject = collider.gameObject.GetComponent<ChildImpactDetect>().ObjectBase.GetComponent<IContactObject>();

                if (iContactObject.GetTypePosition() != TypePosition.Ground)
                {
                    return;
                }

                HealthBase healthBase = iContactObject.GetHealth();

                if(healthBase.GetHealth() > 0)
                {
                    SetTarget(iContactObject);

                    //currentTimeAnimation = speedAttack;

                    ChangeState(StateChar.Attack);
                }
            }
        }
    }
}
