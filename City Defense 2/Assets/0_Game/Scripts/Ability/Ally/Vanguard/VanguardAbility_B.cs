using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardAbility_B : VanguardAbility
{
    public override void Init(IContactObject _iContactObject)
    {
        base.Init(_iContactObject);

        //var a = iContactObject.GetHealth().EventChangeHealth;

        iContactObject.GetHealth().EventChangeHealth += OnChangeHealth;
    }

    public void OnChangeHealth(float current, float sub, float max)
    {
        if((current - sub) <= 0)
        {
            EnergyManager.Instance.AddEnergy((int)(iContactObject.GetObject<CharacterBase>().Energy * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Vanguard_Ally_B_Index_Energy)));
        }
    }
}
