using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppressorWeapon_B : OppressorWeapon
{
    public override void Attack(IContactObject iContactObject)
    {
        rateCrit = GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Oppressor_Ally_B_Percent_Crit);

        base.Attack(iContactObject);
    }
}
