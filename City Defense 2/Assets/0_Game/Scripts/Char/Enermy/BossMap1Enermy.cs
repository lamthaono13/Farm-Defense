using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMap1Enermy : Enermy
{
    //private TypeAttackBoss1 typeAttackBoss1;

    [SerializeField] private float coolDownSkill;

    [SerializeField] private WeaponBase specialWeapon;

    [SerializeField] private ParticleSystem particleSystemDie;

    //[SerializeField] private float radiusToMove;

    private float countCDSkill;

    private bool isSkill;

    public override void Start()
    {
        base.Start();

        currentTimeAnimation = 0;
    }

    public override void InitIndexConfig(DataSqawn dataSqawn)
    {
        base.InitIndexConfig(dataSqawn);

        specialWeapon.InitIndexConfig(this, weapon.GetDamage(), 0);

        postionFinish = new Vector3(transform.position.x +0.1f, ObjectManager.Instance.GetPositionFinish().y, ObjectManager.Instance.GetPositionFinish().z);
    }

    public override void Init(TypeSlotEquip _typeSlotEquip = TypeSlotEquip.Slot1)
    {
        base.Init(_typeSlotEquip);

        
    }

    public override void OnEnable()
    {
        base.OnEnable();

        transform.position = new Vector3(0.1f, 16, 0);

        //LevelManagerMainGame.Instance.CharManager.AddEnermy(this);

        LevelManagerMainGame.Instance.UiManagerMainGame.UiHealthBoss.Show(true);
    }

    protected override void OnChangeHealth(float currentHealth, float number, float healthMax)
    {
        base.OnChangeHealth(currentHealth, number, healthMax);

        LevelManagerMainGame.Instance.UiManagerMainGame.UiHealthBoss.OnChangeHealth(number, currentHealth, healthMax);
    }

    public override void OnDisable()
    {
        base.OnDisable();

        //LevelManagerMainGame.Instance.CharManager.RemoveEnermy(this);

        if (LevelManagerMainGame.Instance.UiManagerMainGame.UiHealthBoss != null)
        {
            LevelManagerMainGame.Instance.UiManagerMainGame.UiHealthBoss.Show(false);
        }
    }

    public override void Update()
    {
        base.Update();

        if(countCDSkill >= coolDownSkill)
        {
            //ChangeState(StateChar.Skill);

            isSkill = true;

            countCDSkill = 0;
        }
        else
        {
            countCDSkill += Time.deltaTime;
        }
    }

    private void MoveBoss()
    {
        //Vector3 u = new Vector3(postionFinish.x, postionFinish.y, transform.position.z);

        ////if (postionFinish.x == transform.position.x)
        ////{
        ////    u.x += 0.01f;
        ////}

        //if (u.x < transform.position.x)
        //{
        //    render.SetFlip(true);
        //}
        //else
        //{
        //    render.SetFlip(false);
        //}

        movement.StartMove(TypeMove.Move, new Vector3(0, -100000, 0));

        //if (CheckImpactObstacle())
        //{
        //    movement.StartMove(TypeMove.Destination, new Vector3(0, -100000, 0));
        //}
        //else
        //{
        //    movement.StartMove(TypeMove.Move, new Vector3(0, -100000, 0));
        //}
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

            case StateChar.Skill:

                target = null;

                movement.StopMove();

                render.Animation.Animate(TypeAnimation.Skill, 1, false, () => { specialWeapon.Attack(this); }, () => { ChangeState(StateChar.Run); });

                break;

            case StateChar.Die:

                particleSystemDie.Play();

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

                if (isSkill)
                {
                    ChangeState(StateChar.Skill);

                    return;
                }

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

                    //render.Animation.Animate(TypeAnimation.Attack, speedAttack / render.Animation.GetTimeAnimation(TypeAnimation.Attack, false), true, () => { weapon.Attack(target); }, () => { });


                    if (target.GetBody().position.x < transform.position.x)
                    {
                        render.Animation.Animate(TypeAnimation.Attack_L, speedAttack / render.Animation.GetTimeAnimation(TypeAnimation.Attack_L, false), true, () => { weapon.Attack(target); }, () => { if (isSkill) { ChangeState(StateChar.Skill);  } });
                    }
                    else
                    {
                        render.Animation.Animate(TypeAnimation.Attack_R, speedAttack / render.Animation.GetTimeAnimation(TypeAnimation.Attack_R, false), true, () => { weapon.Attack(target); }, () => { if (isSkill) { ChangeState(StateChar.Skill); } });
                    }

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

    public override void OnExitState(StateChar stateChar)
    {
        base.OnExitState(stateChar);

        switch (stateChar)
        {
            case StateChar.Skill:
                isSkill = false;
                break;
        }
    }

    private void OnSkill()
    {

    }

    public override void OnTriggerEnterChildDetect(Collider2D collider)
    {
        base.OnTriggerEnterChildDetect(collider);
    }

    public override void OnTriggerStayChildDetect(Collider2D collider)
    {
        base.OnTriggerStayChildDetect(collider);

        //if (collider.CompareTag("Player"))
        //{
        //    if (target == null)
        //    {
        //        SetTarget(collider.gameObject.GetComponent<ChildImpactDetect>().ObjectBase.GetComponent<HealthBase>());

        //        isEat = true;

        //        ChangeState(StateChar.Attack);
        //    }
        //}

        if (collider.CompareTag("Player"))
        {
            if (target == null && charFSM.CurrentCharState != StateChar.Skill)
            {
                SetTarget(collider.gameObject.GetComponent<ChildImpactDetect>().ObjectBase.GetComponent<IContactObject>());

                //currentTimeAnimation = speedAttack;

                ChangeState(StateChar.Attack);
            }
        }

        if (collider.CompareTag("Barel"))
        {
            if (target == null && charFSM.CurrentCharState != StateChar.Skill)
            {
                SetTarget(collider.gameObject.GetComponent<IContactObject>());

                //currentTimeAnimation = speedAttack;

                ChangeState(StateChar.Attack);
            }
        }
    }

    public override void OnFinish()
    {
        //base.OnFinish();

        HealthGamePlay.Instance.SubAllHealth();

        ChangeState(StateChar.Destroy);
    }
}

public enum TypeAttackBoss1
{
    Normal,
    OneShot,
    AttackPercentHealth,
}