using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class HealthBase : MonoBehaviour
{
    [SerializeField] protected bool isShow;

    [SerializeField] protected float healthMax;

    [ShowIf("isShow")] [SerializeField] protected GameObject objHealth;

    protected float currentHealth;

    protected float ReduceDamage;

    public delegate void OnChangeHealth(float current, float numberSub, float max);

    public OnChangeHealth EventChangeHealth;
    
    public virtual void Awake()
    {
        ReduceDamage = 1;
    }

    public virtual void OnDisable()
    {
        EventChangeHealth = delegate { };
    }

    public virtual void OnEnable()
    {
        
    }

    public void InitReduceDamage(float reduce)
    {

    }

    public virtual void InitIndexConfig(float _health)
    {
        healthMax = _health;
        currentHealth = _health;
    }

    public void Init()
    {
        currentHealth = healthMax;
    }

    public float GetPercentHealth()
    {
        return currentHealth / healthMax;
    }
    
    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void LateUpdate()
    {

    }

    public virtual void Show(bool isShow)
    {
        objHealth.gameObject.SetActive(isShow);
    }

    public virtual void AddHealth(float number, string mess)
    {
        var u = currentHealth + number;

        currentHealth = u > healthMax ? healthMax : u;
    }

    public virtual void SubHealth(TypeWeapon typeWeapon, float number, string mess)
    {
        if(currentHealth == 0)
        {
            return;
        }
        
        EventChangeHealth?.Invoke(currentHealth, number, healthMax);
    }

    public virtual void SubAllHealth(TypeWeapon typeWeapon, string mess)
    {
        SubHealth(typeWeapon, currentHealth, mess);
    }

    public virtual void AddMaxHealth(float number, string mess)
    {
        healthMax += number;
    }

    public virtual void AddFullHealth()
    {
        currentHealth = healthMax;
    }

    public virtual float GetHealth()
    {
        return currentHealth;
    }

    public virtual void SubHealthToZero()
    {
        currentHealth = 0;
    }

    public float GetHealthMax()
    {
        return healthMax;
    }
}
