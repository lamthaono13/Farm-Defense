using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppressorEnermyAbility_B : OppressorEnermyAbility
{
    public override void OnGetEffect(TypeEffectAttack typeEffectAttack)
    {
        if (hasSheild)
        {
            return;
        }

        base.OnGetEffect(typeEffectAttack);
    }

    public override void OnGetEffect(TypeEffectAttack typeEffectAttack, float damage)
    {
        if (hasSheild)
        {
            return;
        }

        base.OnGetEffect(typeEffectAttack, damage);
    }

    public override void OnGetHit(TypeWeapon typeWeapon, float damage)
    {
        if (hasSheild)
        {
            switch (typeWeapon)
            {
                case TypeWeapon.MeleeCrit:
                    iContactObject.GetHealth().SubHealth(typeWeapon, damage * 0.3f * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Oppressor_Enemy_B_Index_Damage_Earn_Crit_Has_Sheild), "");
                    return;
                case TypeWeapon.RangeCrit:
                    iContactObject.GetHealth().SubHealth(typeWeapon, damage * 0.3f * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Oppressor_Enemy_B_Index_Damage_Earn_Crit_Has_Sheild), "");
                    return;
            }
        }

        base.OnGetHit(typeWeapon, damage);
    }
}
