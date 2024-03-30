using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class WeaponBase : MonoBehaviour
{
    protected float baseDamage;

    protected float specialDamage;

    [SerializeField] protected bool hasBullet;

    [ShowIf("hasBullet")] [SerializeField] protected GameObject bullet;

    protected IContactObject contactObject;

    public void InitIndexConfig(IContactObject _contactObject, float _baseDamage, float _specialDamage)
    {
        baseDamage = _baseDamage;
        specialDamage = _specialDamage;

        contactObject = _contactObject;
    }

    public void InscreaseDamage(float index)
    {
        baseDamage += index;
    }
    
    public void InscreasePercentDamage(float percent)
    {
        baseDamage += baseDamage * percent;
    }

    public void SetDamage(float index)
    {
        baseDamage = index;
    }
    
    public float GetDamage()
    {
        return baseDamage;
    }

    public virtual void Attack(IContactObject iContactObject)
    {
        if(iContactObject == null || iContactObject.GetHealth().GetHealth() <= 0)
        {
            return;
        }
    }

    public virtual void Attack()
    {

    }

    public virtual void AttackLevel(int Level)
    {

    }

    public virtual void StopAttack()
    {

    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }
}