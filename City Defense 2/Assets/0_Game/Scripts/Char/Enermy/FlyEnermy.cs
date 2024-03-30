using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnermy : Enermy
{
    public override void Hited(TypeWeapon typeWeapon, float damage)
    {
        if (typePosition == TypePosition.Sky)
        {
            // falling
            
            return;
        }
        
        base.Hited(typeWeapon, damage);
    }
}
