using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : BulletBase
{
    protected Coroutine coroutineDestroy;

    public override void Init(DataBullet dataBullet)
    {
        base.Init(dataBullet);
        
        
    }

    public override void OnShoot(float _damage, Vector3 _postionTarget, Vector3 _direction)
    {
        base.OnShoot(_damage, _postionTarget, _direction);

        canMove = true;



        coroutineDestroy = StartCoroutine(WaitDestroy());
    }

    public override void Update()
    {
        base.Update();

        if (canMove)
        {
            transform.position += direction * speedMove * Time.deltaTime;
        }
    }

    protected virtual void OnAttack(IContactObject iContactObject)
    {
        StopCoroutine(coroutineDestroy);

        iContactObject.Hited(typeWeapon, damage);
        
        Destroy(gameObject);
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enermy"))
        {
            ChildImpactDetect enermy = other.GetComponent<ChildImpactDetect>();

            OnAttack(enermy.ObjectBase.GetComponent<IContactObject>());
        }
    }
}
