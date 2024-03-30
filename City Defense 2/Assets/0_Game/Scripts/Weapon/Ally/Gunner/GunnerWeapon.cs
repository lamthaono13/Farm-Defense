using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerWeapon : WeaponBase
{
    [SerializeField] protected Transform postionHeadGun;

    [SerializeField] private ParticleSystem particleSystemShoot;

    public override void Attack(IContactObject iContactObject)
    {
        base.Attack(iContactObject);

        GameManager.Instance.SoundManager.PlaySoundThrow();

        Vector3 dir = iContactObject.GetBody().position - postionHeadGun.position;

        dir = dir.normalized;

        float ZAngle = DirectionToAngle2D.GetAngleFromDirection2D(dir);

        GameObject obj = Instantiate(bullet, postionHeadGun.position, Quaternion.Euler(0, 0, ZAngle));

        BulletBase bulletBase = obj.GetComponent<BulletBase>();



        particleSystemShoot.Play();

        bulletBase.OnShoot(baseDamage, iContactObject.GetBody().position, new Vector3(dir.x, dir.y, 0));
    }
}