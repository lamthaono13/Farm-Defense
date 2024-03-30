using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObject : HealthBase
{
    [SerializeField] private DynamicObject dynamicObject;

    public override void SubHealth(TypeWeapon typeWeapon, float number, string mess)
    {
        base.SubHealth(typeWeapon, number, mess);

        float a = currentHealth - (number - number * (ReduceDamage - 1));

        if (a <= 0)
        {
            currentHealth = 0;

            dynamicObject.OnGetHit(number, true);
        }
        else
        {
            currentHealth -= number;

            dynamicObject.OnGetHit(number, false);
        }
    }
}