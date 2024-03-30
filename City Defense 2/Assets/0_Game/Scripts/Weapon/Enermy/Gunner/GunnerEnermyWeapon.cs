using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerEnermyWeapon : WeaponBase
{
    public override void Attack()
    {
        base.Attack();

        GameObject obj = Instantiate(bullet, contactObject.GetBody().position, Quaternion.Euler(0, 0, 0));

        BulletBase bulletBase = obj.GetComponent<BulletBase>();

        bulletBase.OnShoot(baseDamage, contactObject.GetBody().position, new Vector3(0, 0, 0));

        Debug.Log(1);
    }
}
