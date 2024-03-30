using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(HealthBase))]

public class CharacterBase : ObjectBase
{
    [SerializeField] protected Rigidbody2D ri;

    [SerializeField] protected Movement movement;

    [SerializeField] protected Collider2D colli;

    [SerializeField] protected WeaponBase weapon;

    protected const float configTimeCheckUpdate = 0.1f;

    [SerializeField] protected ConfigBaseIndex configBaseIndex;
    
    [SerializeField] protected float radiusCheck;

    [SerializeField] protected float rangeAttack;
    
    [SerializeField] protected float speedAttack;

    [SerializeField] protected float timeWaitDie;

    protected int energy;

    //public HealthCharacter Health => health;

    public int Energy => energy;

    protected float currentTimeAnimation;

    protected float currentTimeCheckUpdate;

    protected CharFSM charFSM;

    protected IContactObject target;

    protected bool canCheck;

    protected StateChar state;

    public override void OnEnable()
    {
        CharManager.Instance.AddCharBase(this);
    }

    public override void OnDisable()
    {
        CharManager.Instance.RemoveCharBase(this);
    }

    public override void InitIndexConfig(DataSqawn dataSqawn)
    {
        transform.position = dataSqawn.PostionSqawn;

        float damage = configBaseIndex.dataConfigForTypeCharBase.Damage + configBaseIndex.dataConfigIndexGrow.AttackGrow * (dataSqawn.level - 1);

        float healthChar = configBaseIndex.dataConfigForTypeCharBase.HP + configBaseIndex.dataConfigIndexGrow.HPGrow * (dataSqawn.level - 1);

        //for (int i = 0; i < dataSqawn.level - 1; i++)
        //{
        //    damage = damage + configBaseIndex.dataConfigIndexGrow.AttackGrow;

        //    healthChar = healthChar + configBaseIndex.dataConfigIndexGrow.HPGrow;
        //}



        health.InitIndexConfig(healthChar);
        movement.InitIndexConfig(configBaseIndex.dataConfigForTypeCharBase.Speed, configBaseIndex.dataConfigForTypeCharBase.Speed);
        weapon.InitIndexConfig(this, damage, 0);

        speedAttack = configBaseIndex.dataConfigForTypeCharBase.AttackSpeed - configBaseIndex.dataConfigIndexGrow.AttackSpeedGrow;

        radiusCheck = configBaseIndex.dataConfigForTypeCharBase.RadiusCheck;

        rangeAttack = configBaseIndex.dataConfigForTypeCharBase.RangeToAttack;

        energy = configBaseIndex.dataConfigForTypeCharBase.Energy;
    }

    public virtual void OnStartLevel()
    {
        //Init();
    }

    public override void Init(TypeSlotEquip typeSlotEquip = TypeSlotEquip.Slot1)
    {
        base.Init(typeSlotEquip);

        charFSM = new CharFSM(this);

        colli.enabled = true;

        currentTimeCheckUpdate = configTimeCheckUpdate;

        health.Init();

        canCheck = true;
    }

    public virtual void OnEndGame(GameResult gameResult)
    {

    }

    public virtual void OnRevive()
    {

    }

    public override void Update()
    {
        charFSM.Update();
        
        //movement.FixPositionNavmesh();
    }

    public override void FixedUpdate()
    {
        charFSM.FixedUpdate();
    }

    public override void LateUpdate()
    {
        charFSM.LateUpdate();
    }

    public virtual void OnSqawn()
    {

    }

    public virtual void OnGetHit(float damage, bool isDie)
    {
        if (isDie)
        {
            ChangeState(StateChar.Die);
        }
        else
        {
            render.OnGetHit();
        }
    }

    public override void Hited(TypeWeapon typeWeapon, float damage)
    {
        base.Hited(typeWeapon, damage);
        
        GetAbility().OnGetHit(typeWeapon, damage);
        
        //GetHealth().SubHealth(typeWeapon, damage, "");
    }

    public override WeaponBase GetWeapon()
    {
        return weapon;
    }

    public virtual void OnGetSheild()
    {

    }

    public virtual void OnEnterState(StateChar stateChar)
    {
        switch (stateChar)
        {
            case StateChar.Idle:
                break;
            case StateChar.Run:

                break;
            case StateChar.FindTarget:
                break;
            case StateChar.Attack:

                //currentTimeAnimation = 0;

                break;
            case StateChar.Die:

                colli.enabled = false;

                movement.OnDie();

                render.Animation.Animate(TypeAnimation.Die, 1, false, null, null);

                render.OnDie();

                //StartWaitToDie();

                break;
            case StateChar.Revive:
                break;
            case StateChar.Stun:
                render.Animation.Animate(TypeAnimation.Idle, 1, true, null, null);

                movement.SetFreezeSpeed(true);
                break;
            case StateChar.Destroy:

                GetHealth().SubHealthToZero();

                colli.enabled = false;

                movement.OnDie();

                render.Animation.Animate(TypeAnimation.Die, 1, false, null, null);

                render.OnDie();

                break;
        }
    }

    public virtual void StartWaitToDie(Action action)
    {
        StartCoroutine(WaitToDie(action));
    }

    public virtual void AcitionBeforeDie()
    {

    }

    public virtual IEnumerator WaitToDie(Action action)
    {
        AcitionBeforeDie();

        yield return new WaitForSeconds(timeWaitDie);

        actionWhenDie?.Invoke();
        
        action?.Invoke();
    }

    public virtual void OnUpdateState(StateChar stateChar)
    {
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
            case StateChar.Stun:
                break;
        }
    }

    public virtual void OnExitState(StateChar stateChar)
    {
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
            case StateChar.Stun:
                movement.SetFreezeSpeed(false);
                break;
        }
    }

    protected virtual void ChangeState(StateChar stateChar)
    {
        if(charFSM.CurrentCharState == StateChar.Stun && stateChar != StateChar.Die)
        {
            return;
        }

        if(stateChar != StateChar.Stun)
        {
            this.state = stateChar;
        }



        charFSM.ChangeState(stateChar);
    }

    public override void OnStun(float timeStun)
    {

    }

    public virtual void OnSlow()
    {

    }

    protected void SetTarget(IContactObject _target)
    {
        target = _target;
    }

    protected virtual bool CheckTarget()
    {
        return false;
    }

    protected virtual bool CheckTargetOnGround()
    {
        return false;
    }

    protected virtual bool CheckTargetPriority(TypeGroup typeGroup)
    {
        return false;
    }
    
    protected virtual bool CheckAliveTarget()
    {
        if(target as object == null)
        {
            target = null;

            return false;
        }
        else
        {
            if (!target.GetBody())
            {
                target = null;

                return false;
            }

            if(target.GetHealth().GetHealth() > 0)
            {
                return true;
            }
            else
            {
                target = null;

                return false;
            }
        }
    }

    public override void OnTriggerStayChildDetect(Collider2D collider)
    {
        base.OnTriggerStayChildDetect(collider);
    }

    public override void OnTriggerExitChildDetect(Collider2D collider)
    {
        base.OnTriggerExitChildDetect(collider);
    }
}