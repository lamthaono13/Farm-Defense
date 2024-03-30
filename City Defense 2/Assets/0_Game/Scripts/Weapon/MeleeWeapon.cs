using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    public override void Attack(IContactObject icContactObject)
    {
        base.Attack(icContactObject);

        if(icContactObject == null)
        {
            return;
        }

        //icContactObject.SubHealth(baseDamage, "meleeWeapon");
    }
}
