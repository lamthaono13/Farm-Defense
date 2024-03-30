using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnermy : Enermy
{
    [SerializeField] private float timeCountToNextAttack;

    private float timeCurrentToNextAttack;

    public override void Start()
    {
        base.Start();

        currentTimeAnimation = 0;
    }

    public override void OnEnable()
    {
        base.OnEnable();

        CharManager.Instance.AddEnermy(this);
    }

    public override void OnDisable()
    {
        base.OnDisable();

        CharManager.Instance.RemoveEnermy(this);
    }

    public override void Init(TypeSlotEquip _typeSlotEquip = TypeSlotEquip.Slot1)
    {
        base.Init(_typeSlotEquip);
        timeCurrentToNextAttack = timeCountToNextAttack;
        ChangeState(StateChar.Run);
    }

    public override void OnEnterState(StateChar stateChar)
    {
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

                render.Animation.Animate(TypeAnimation.Attack, speedAttack / render.Animation.GetTimeAnimation(TypeAnimation.Attack, false), false, () => { weapon.Attack(target); }, () => { ChangeState(StateChar.Run); });

                break;

            case StateChar.Die:

                //PoolManager.Despawn(this.gameObject);

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

                if(currentTimeAnimation >= speedAttack)
                {
                    bool hasTarget = CheckTarget();

                    if (hasTarget)
                    {
                        currentTimeAnimation = 0;

                        ChangeState(StateChar.Attack);
                    }
                }
                else
                {
                    currentTimeAnimation += Time.deltaTime;
                }

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

    public override void OnTriggerEnterChildDetect(Collider2D collider)
    {
        base.OnTriggerEnterChildDetect(collider);
    }

    public override void OnTriggerStayChildDetect(Collider2D collider)
    {
        base.OnTriggerStayChildDetect(collider);

        if (collider.CompareTag("Player"))
        {
            if (target == null)
            {
                SetTarget(collider.gameObject.GetComponent<ChildImpactDetect>().ObjectBase.GetComponent<IContactObject>());

                ChangeState(StateChar.Attack);
            }
        }

        if (collider.CompareTag("Barel"))
        {
            if (target == null)
            {
                SetTarget(collider.gameObject.GetComponent<IContactObject>());

                ChangeState(StateChar.Attack);
            }
        }
    }
}
