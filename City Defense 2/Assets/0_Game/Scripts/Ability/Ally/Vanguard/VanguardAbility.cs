using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardAbility : AbilityBase
{
    public override void Init(IContactObject _iContactObject)
    {
        base.Init(_iContactObject);
    }

    public override void OnGetEffect(TypeEffectAttack typeEffectAttack)
    {
        //base.OnGetEffect(typeEffectAttack);

        return;
    }

    public override void OnGetHit(TypeWeapon typeWeapon, float damage)
    {
        //base.OnGetHit(typeWeapon, damage);

        if (typeWeapon == TypeWeapon.Range)
        {
            iContactObject.GetHealth().SubHealth(typeWeapon, damage * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Vanguard_Ally_C_ReduceDamage), "");
        }
        else
        {
            iContactObject.GetHealth().SubHealth(typeWeapon, damage, "");
        }
    }
}
