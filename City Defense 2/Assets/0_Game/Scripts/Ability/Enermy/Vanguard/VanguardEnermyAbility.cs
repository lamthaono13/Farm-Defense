using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardEnermyAbility : AbilityBase
{
    public override void OnGetHit(TypeWeapon typeWeapon, float damage)
    {
        base.OnGetHit(typeWeapon, damage);

        switch (typeWeapon)
        {
            case TypeWeapon.Range:

                OnGetEffect(TypeEffectAttack.SpeedUp);

                break;
            case TypeWeapon.RangeCrit:

                OnGetEffect(TypeEffectAttack.SpeedUp);

                break;
        }
    }
}
