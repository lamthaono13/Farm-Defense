using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnermyBullet_B : SniperEnermyBullet
{
    protected override void OnAttack(IContactObject iContactObject)
    {
        iContactObject.GetAbility().OnGetEffect(TypeEffectAttack.DOTSniperEnermy, damage * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Sniper_Enemy_B_Index_Damage_DOT));

        base.OnAttack(iContactObject);
    }
}