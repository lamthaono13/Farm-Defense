using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardEnermyAbility_B : VanguardEnermyAbility
{
    protected bool isSkill;

    public override void Init(IContactObject _iContactObject)
    {
        base.Init(_iContactObject);

        //var a = iContactObject.GetHealth().EventChangeHealth;

        iContactObject.GetHealth().EventChangeHealth += OnChangeHealth;
    }

    public void OnChangeHealth(float current, float sub, float max)
    {
        Debug.Log(213123);

        if (isSkill)
        {
            return;
        }

        var after = current - sub;

        float k = after / max;

        if (k < GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Vanguard_Enemy_B_Index_Crazy_State))
        {
            iContactObject.GetObject<VanguardEnermy_B>().OnChangeToSkill();



            isSkill = true;
        }
    }
}
