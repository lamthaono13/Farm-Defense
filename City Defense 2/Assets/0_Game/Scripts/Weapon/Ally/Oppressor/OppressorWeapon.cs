using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppressorWeapon : WeaponBase
{
    [SerializeField] protected Transform postionHeadGun;

    [SerializeField] private ParticleSystem particleSystemShoot;

    protected float rateCrit = 0;

    public override void Attack(IContactObject iContactObject)
    {
        base.Attack(iContactObject);

        GameManager.Instance.SoundManager.PlaySoundGun();

        Vector3 dir = iContactObject.GetBody().position - postionHeadGun.position;

        dir = dir.normalized;

        float ZAngle = DirectionToAngle2D.GetAngleFromDirection2D(dir);

        GameObject obj = Instantiate(bullet, postionHeadGun.position, Quaternion.Euler(0, 0, ZAngle));

        BulletBase bulletBase = obj.GetComponent<BulletBase>();



        particleSystemShoot.Play();

        int a = Random.Range(1, 101);

        if (a <= rateCrit)
        {
            bulletBase.OnShoot(baseDamage * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Opperssor_Ally_B_Index_Crit_Damage), iContactObject.GetBody().position, new Vector3(dir.x, dir.y, 0));
        }
        else
        {
            bulletBase.OnShoot(baseDamage, iContactObject.GetBody().position, new Vector3(dir.x, dir.y, 0));
        }
    }
}
