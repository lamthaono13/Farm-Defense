using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : WeaponBase
{
    [SerializeField] protected float radius;

    [SerializeField] protected Transform postionHeadGun;

    [SerializeField] protected GameObject bullet;

    public override void Attack(IContactObject icContactObject)
    {
        base.Attack(icContactObject);

        GameManager.Instance.SoundManager.PlaySoundGun();
    }
}
