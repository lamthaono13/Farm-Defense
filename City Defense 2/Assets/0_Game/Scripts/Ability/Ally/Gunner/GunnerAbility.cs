using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerAbility : AbilityBase
{
    public override void OnGetHit(TypeWeapon typeWeapon, float damage)
    {
        switch (typeWeapon)
        {
            case TypeWeapon.Melee:
                iContactObject.GetHealth().SubHealth(typeWeapon, damage * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Ally_C_Index_Damage_Earn_From_Melee), "");
                return;
            case TypeWeapon.MeleeCrit:
                iContactObject.GetHealth().SubHealth(typeWeapon, damage * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Ally_C_Index_Damage_Earn_From_Melee), "");
                return;
            case TypeWeapon.Range:
                iContactObject.GetHealth().SubHealth(typeWeapon, damage * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Ally_C_Index_Damage_Earn_From_Range), "");
                return;
            case TypeWeapon.RangeCrit:
                iContactObject.GetHealth().SubHealth(typeWeapon, damage * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Ally_C_Index_Damage_Earn_From_Range), "");
                return;
        }

        base.OnGetHit(typeWeapon, damage);

    }
}
