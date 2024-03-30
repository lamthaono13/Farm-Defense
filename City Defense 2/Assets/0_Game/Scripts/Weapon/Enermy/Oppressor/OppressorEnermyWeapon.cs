using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppressorEnermyWeapon : WeaponBase
{
    public override void Attack(IContactObject iContactObject)
    {
        base.Attack(iContactObject);

        if (iContactObject == null)
        {
            return;
        }

        GameManager.Instance.SoundManager.PlaySoundPunch();

        iContactObject.Hited(TypeWeapon.Melee, baseDamage);
    }
}
