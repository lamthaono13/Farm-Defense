using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerEnermy : Enermy
{
    protected override void OnDestroy()
    {
        //base.OnDestroy();
    }

    public override void OnEnterState(StateChar stateChar)
    {
        switch (stateChar)
        {
            case StateChar.Die:

                Pooling.Instance.PoolEffect.SqawnEnergyEffect(GetBody().position);

                //EnergyManager.Instance.AddEnergy(energyErn);

                StartWaitToDie(() => Destroy(gameObject));

                colli.enabled = false;

                health.SubHealthToZero();

                movement.OnDie();

                CharManager.Instance.RemoveEnermy(this);

                render.Animation.Animate(TypeAnimation.Skill, 1, false, () =>
                {
                    weapon.Attack();
                }, () => { });

                return;

        }

        base.OnEnterState(stateChar);

        switch (stateChar)
        {
            case StateChar.Idle:

                break;

            case StateChar.Run:

                break;

            case StateChar.FindTarget:

                break;

            case StateChar.Attack:

                bool b = CheckAliveTarget();

                if (!b)
                {
                    ChangeState(StateChar.Run);

                    //currentTimeAnimation = 0;

                    return;
                }

                render.Animation.Animate(TypeAnimation.Idle, 1, true, null, null);

                Debug.Log(currentTimeAnimation);

                break;

            case StateChar.Die:

                //PoolManager.Despawn(this.gameObject);

                break;

            case StateChar.Revive:

                break;
            case StateChar.Skill:

                colli.enabled = false;

                movement.OnDie();

                health.SubHealthToZero();

                CharManager.Instance.RemoveEnermy(this);

                render.Animation.Animate(TypeAnimation.Skill, 1, false, () =>
                {
                    weapon.Attack();
                    //Pooling.Instance.PoolEffect.SqawnEnergyEffect(GetBody().position);

                    //EnergyManager.Instance.AddEnergy(energyErn);



                    //render.Animation.Animate(TypeAnimation.Die, 1, false, null, null);

                    //render.OnDie();

                    StartWaitToDie(() => Destroy(gameObject));
                }, () => { });

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

                currentTimeAnimation += Time.deltaTime;

                break;

            case StateChar.FindTarget:

                break;

            case StateChar.Attack:

                bool a = CheckAliveTarget();

                if (!a)
                {
                    ChangeState(StateChar.Run);

                    //currentTimeAnimation = 0;

                    return;
                }

                if (Vector3.Distance(target.GetBody().position, GetBody().position) > radiusCheck)
                {
                    ChangeState(StateChar.Run);

                    return;
                }

                if (currentTimeAnimation >= speedAttack)
                {
                    currentTimeAnimation = 0;

                    render.Animation.Animate(TypeAnimation.Attack, speedAttack / render.Animation.GetTimeAnimation(TypeAnimation.Attack, false), true, () => { weapon.Attack(target); }, () => { });

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
            case StateChar.Skill:
                break;
        }
    }

    public override void AcitionBeforeDie()
    {
        base.AcitionBeforeDie();



        //render.Animation.Animate(TypeAnimation.Die, 1, false, () => 
        //{ 
        //    if(health.GetHealth() > 0)
        //    {
        //        health.SubHealthToZero();
        //        weapon.Attack();
        //    }
        //}, () => { });
    }

    public override void OnTriggerStayChildDetect(Collider2D collider)
    {
        base.OnTriggerStayChildDetect(collider);

        if (collider.CompareTag("Player"))
        {
            if (target == null && charFSM.CurrentCharState != StateChar.Skill)
            {
                SetTarget(collider.gameObject.GetComponent<ChildImpactDetect>().ObjectBase.GetComponent<IContactObject>());

                //currentTimeAnimation = speedAttack;

                ChangeState(StateChar.Skill);
            }
        }

        if (collider.CompareTag("Barel"))
        {
            if (target == null && charFSM.CurrentCharState != StateChar.Skill)
            {
                SetTarget(collider.gameObject.GetComponent<IContactObject>());

                //currentTimeAnimation = speedAttack;

                ChangeState(StateChar.Skill);
            }
        }
    }
}