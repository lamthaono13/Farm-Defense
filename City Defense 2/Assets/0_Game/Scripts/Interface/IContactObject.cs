using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IContactObject
{
    public HealthBase GetHealth();

    public RenderControl GetRender();

    public Transform GetBody();

    public void Hited(TypeWeapon typeWeapon, float damage);

    public void OnStun(float timeStun);

    public AbilityBase GetAbility();

    public WeaponBase GetWeapon();
    
    public TypePosition GetTypePosition();
    
    public T GetObject<T>();

    public Help.OnDie GetActionWhenDie();

    public TypeGroup GetTypeGroup();
}