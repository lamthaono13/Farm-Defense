using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardWeapon_B : VanguardWeapon
{
    public override void Attack(IContactObject iContactObject)
    {
        //base.Attack(iContactObject);

        if (iContactObject == null)
        {
            return;
        }

        iContactObject.Hited(TypeWeapon.Melee, baseDamage / 2);
    }
}
