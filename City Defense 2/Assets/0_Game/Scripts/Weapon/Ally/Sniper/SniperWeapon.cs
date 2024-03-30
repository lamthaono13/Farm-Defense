using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SniperWeapon : WeaponBase
{
    [SerializeField] protected Transform postionHeadGun;

    [SerializeField] private ParticleSystem particleSystemShoot;

    //[SerializeField] private GameObject objSystemShoot;

    protected float rateCrit = 10;

    private Vector3 positionVfx;

    private Quaternion quaternionVfx;

    private Vector3 scaleVfx;

    public void OnEnable()
    {
        base.Start();

        positionVfx = particleSystemShoot.transform.localPosition;

        quaternionVfx = particleSystemShoot.transform.localRotation;

        scaleVfx = particleSystemShoot.transform.localScale;
    }

    public override void Attack(IContactObject iContactObject)
    {
        base.Attack(iContactObject);

        GameManager.Instance.SoundManager.PlaySoundGun2();

        rateCrit = GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Sniper_Ally_C_Percent_Crit);

        Vector3 dir = iContactObject.GetBody().position - postionHeadGun.position;

        dir = dir.normalized;

        float ZAngle = DirectionToAngle2D.GetAngleFromDirection2D(dir);

        GameObject obj = Instantiate(bullet, postionHeadGun.position, Quaternion.Euler(0, 0, ZAngle));

        BulletBase bulletBase = obj.GetComponent<BulletBase>();


        particleSystemShoot.transform.SetParent(postionHeadGun);

        particleSystemShoot.transform.localPosition = positionVfx;

        particleSystemShoot.transform.localRotation = quaternionVfx;

        particleSystemShoot.transform.localScale = scaleVfx;

        particleSystemShoot.transform.SetParent(contactObject.GetBody());

        if (particleSystemShoot != null)
        {
            particleSystemShoot.Play();
        }

        



        int a = Random.Range(1, 101);

        if (a <= rateCrit)
        {
            bulletBase.Init(new DataBullet() 
            { 
                TypeWeapon = TypeWeapon.RangeCrit, 
                Damage = baseDamage * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Sniper_Ally_C_Index_Damage_Crit)
                
            });

            bulletBase.OnShoot(baseDamage * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Sniper_Ally_C_Index_Damage_Crit), iContactObject.GetBody().position, new Vector3(dir.x, dir.y, 0));
        }
        else
        {
            bulletBase.Init(new DataBullet()
            {
                TypeWeapon = TypeWeapon.Range,
                Damage = baseDamage * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Sniper_Ally_C_Index_Damage_Crit)

            });

            bulletBase.OnShoot(baseDamage, iContactObject.GetBody().position, new Vector3(dir.x, dir.y, 0));
        }
    }
}
