using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1EnermyAbility : AbilityBase
{
    public override void OnGetHit(TypeWeapon typeWeapon, float damage)
    {
        if(typeWeapon == TypeWeapon.NuclearBoom)
        {
            iContactObject.GetHealth().SubHealth(typeWeapon, iContactObject.GetHealth().GetHealthMax() * 0.3f, "");

            return;
        }

        if(typeWeapon == TypeWeapon.OneShot)
        {
            iContactObject.GetHealth().SubHealth(typeWeapon, iContactObject.GetHealth().GetHealthMax() * 0.1f, "");

            return;
        }

        base.OnGetHit(typeWeapon, damage);
    }
}
