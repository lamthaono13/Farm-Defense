using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using Game;

[RequireComponent(typeof(HealthObject))]
[RequireComponent(typeof(NavMeshObstacle))]

public class DynamicObject : ObjectBase
{
    private TypeSlotEquip typeSlotEquip;

    [SerializeField] protected ConfigBaseIndex configBaseIndex;

    [SerializeField] protected HealthObject healthObject;

    [SerializeField] protected Collider2D colli;

    [SerializeField] protected NavMeshObstacle navMeshObstacle;

    [SerializeField] protected float timeAnimationDie;

    [SerializeField] protected float timeWaitDie;

    private void OnEnable()
    {
        CharManager.Instance.AddAlly(this);
    }

    private void OnDisable()
    {
        CharManager.Instance.RemoveAlly(this);
    }

    public override void Hited(TypeWeapon typeWeapon, float damage)
    {
        base.Hited(typeWeapon, damage);

        GetAbility().OnGetHit(typeWeapon, damage);
    }

    public virtual void OnGetHit(float damage, bool isDie)
    {
        if (isDie)
        {
            colli.enabled = false;

            navMeshObstacle.enabled = false;

            render.OnDie();

            StartCoroutine(WaitDie());
        }
        else
        {
            render.OnGetHit();
        }
    }

    IEnumerator WaitDie()
    {
        yield return new WaitForSeconds(timeWaitDie);
        Pooling.Instance.PoolAlly.DeSqawn(typeSlotEquip, gameObject);
    }

    public override void InitIndexConfig(DataSqawn dataSqawn)
    {
        base.InitIndexConfig(dataSqawn);

        transform.position = dataSqawn.PostionSqawn;

        health.InitIndexConfig(configBaseIndex.dataConfigForTypeCharBase.HP + configBaseIndex.dataConfigIndexGrow.HPGrow * (dataSqawn.level - 1));

        //movement.InitIndexConfig(configBaseIndex.dataConfigForTypeCharBase.Speed, configBaseIndex.dataConfigForTypeCharBase.Speed);

        //weapon.InitIndexConfig(configBaseIndex.dataConfigForTypeCharBase.Damage * Mathf.Pow(configBaseIndex.dataConfigIndexGrow.AttackGrow, dataSqawn.level - 1), 0);

        //speedAttack = configBaseIndex.dataConfigForTypeCharBase.AttackSpeed * Mathf.Pow(configBaseIndex.dataConfigIndexGrow.AttackSpeedGrow, dataSqawn.level - 1);

        //radiusCheck = configBaseIndex.dataConfigForTypeCharBase.RadiusCheck;

        //rangeAttack = configBaseIndex.dataConfigForTypeCharBase.RangeToAttack;
    }

    public override void Init(TypeSlotEquip _typeSlotEquip = TypeSlotEquip.Slot1)
    {
        base.Init(_typeSlotEquip);

        typeSlotEquip = _typeSlotEquip;

        colli.enabled = true;

        navMeshObstacle.enabled = true;

        healthObject.Init();
    }
}
