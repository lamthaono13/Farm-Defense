using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

[RequireComponent(typeof(AbilityBase))]
public class ObjectBase : MonoBehaviour, IContactObject
{
    [SerializeField] protected TypeGroup typeGroup;

    [SerializeField] protected TypeTier typeTier;

    [SerializeField] protected TypePosition typePosition;

    [SerializeField] protected TypeState typeState;
    
    [SerializeField] private Transform body;

    //[SerializeField] protected bool useDataConfig;

    //protected DataConfig dataConfig;

    //protected int level;

    [SerializeField] protected HealthBase health;
    
    [SerializeField] protected RenderControl render;

    [SerializeField] protected AbilityBase abilityBase;
    
    public Help.OnDie actionWhenDie;
    
    public virtual void Awake()
    {

    }
    
    public virtual void Init(TypeSlotEquip _typeSlotEquip = TypeSlotEquip.Slot1)
    {
        //ChangeState(StateChar.Run);
        if(render != null)
        {
            render.Init();
        }

        if (abilityBase != null)
        {
            abilityBase.Init(this);
        }
    }

    public virtual void InitIndexConfig(DataSqawn dataSqawn)
    {

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

    public virtual void OnEnable()
    {

    }

    public virtual void OnDisable()
    {
        actionWhenDie = null;
    }

    public HealthBase GetHealth()
    {
        return health;
    }

    public RenderControl GetRender()
    {
        return render;
    }

    public Transform GetBody()
    {
        return body;
    }

    public AbilityBase GetAbility()
    {
        return abilityBase;
    }

    public virtual WeaponBase GetWeapon()
    {
        return null;
    }
    
    public T GetObject<T>()
    {
        return gameObject.GetComponent<T>();
    }

    public Help.OnDie GetActionWhenDie()
    {
        return actionWhenDie;
    }
    
    public virtual void Hited(TypeWeapon typeWeapon, float damage)
    {
        if(health.GetHealth() <= 0)
        {
            return;
        }
    }

    public virtual void OnStun(float timeStun)
    {

    }

    public virtual void OnTriggerEnterChildDetect(Collider2D collider)
    {

    }

    public virtual void OnTriggerStayChildDetect(Collider2D collider)
    {

    }

    public virtual void OnTriggerExitChildDetect(Collider2D collider)
    {

    }

    public virtual void OnCollistionEnterChildDetect(Collision2D collision)
    {

    }

    public virtual void OnCollistionStayChildDetect(Collision2D collision)
    {

    }

    public virtual void OnCollistionExitChildDetect(Collision2D collision)
    {

    }

    public TypePosition GetTypePosition()
    {
        return typePosition;
    }

    public TypeGroup GetTypeGroup()
    {
        return typeGroup;
    }
}
