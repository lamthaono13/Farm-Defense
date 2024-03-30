using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet_B : SniperBullet
{
    private float rateStun;
    
    protected override void OnAttack(IContactObject iContactObject)
    {
        rateStun = GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Sniper_Ally_B_Percent_Stun);

        int a = Random.Range(1, 101);

        if (a <= rateStun)
        {
            iContactObject.GetAbility().OnGetEffect(TypeEffectAttack.SniperStun);
        }
        
        base.OnAttack(iContactObject);
    }
}
