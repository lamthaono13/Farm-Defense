using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardAbility_A : VanguardAbility
{
    public override void Init(IContactObject _iContactObject)
    {
        base.Init(_iContactObject);

        iContactObject.GetHealth().EventChangeHealth += (current, sub, max) =>
        {
            float mv = GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Vanguard_Ally_A_HP_Lost);

            float hm = GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Vanguard_Ally_A_Damage_Inscrease);

            var after = current - sub;

            float k = after / max;

            float h = 1 - k;

            int v = (int)(h % mv);

            float k1 = current / max;

            float h1 = 1 - k1;

            int v1 = (int)(h1 % mv);

            int b = v - v1;

            iContactObject.GetWeapon().InscreasePercentDamage(b * hm);
        };
    }
}
