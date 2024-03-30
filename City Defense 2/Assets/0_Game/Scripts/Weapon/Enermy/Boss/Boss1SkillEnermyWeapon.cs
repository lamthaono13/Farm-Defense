using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1SkillEnermyWeapon : WeaponBase
{
    [SerializeField] protected Transform postionHeadGun;

    //[SerializeField] private ParticleSystem particleSystemShoot;

    protected float rateCrit = 0;

    public override void Attack(IContactObject iContactObject)
    {
        base.Attack(iContactObject);

        Vector3 dir = iContactObject.GetBody().position - postionHeadGun.position;

        dir = dir.normalized;

        float ZAngle = DirectionToAngle2D.GetAngleFromDirection2D(dir);

        GameObject obj = Instantiate(bullet, iContactObject.GetBody());

        BulletBase bulletBase = obj.GetComponent<BulletBase>();



        //particleSystemShoot.Play();

        bulletBase.OnShoot(baseDamage, iContactObject.GetBody().position, new Vector3(dir.x, dir.y, 0));
    }
}
