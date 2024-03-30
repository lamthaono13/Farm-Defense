using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1EnermyBullet : BulletBase
{
    protected Coroutine coroutineDestroy;

    private bool isShooting;

    private List<IContactObject> contactObjects;

    public override void Init(DataBullet dataBullet)
    {
        base.Init(dataBullet);
    }

    public override void OnShoot(float _damage, Vector3 _postionTarget, Vector3 _direction)
    {
        base.OnShoot(_damage, _postionTarget, _direction);

        canMove = true;

        GameManager.Instance.VibrateManager.Vibate(80);

        coroutineDestroy = StartCoroutine(WaitDestroy());

        contactObjects = new List<IContactObject>();
        isShooting = true;
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
        //StopCoroutine(coroutineDestroy);

        iContactObject.Hited(typeWeapon, damage);

        //Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isShooting)
        {
            ChildImpactDetect enermy = other.GetComponent<ChildImpactDetect>();

            IContactObject contact = enermy.ObjectBase.GetComponent<IContactObject>();

            if (!contactObjects.Contains(contact))
            {
                contactObjects.Add(contact);

                OnAttack(contact);
            }
        }

        if (other.CompareTag("Barel") && isShooting)
        {
            //ChildImpactDetect enermy = other.GetComponent<ChildImpactDetect>();

            IContactObject contact = other.GetComponent<IContactObject>();

            if (!contactObjects.Contains(contact))
            {
                contactObjects.Add(contact);

                OnAttack(contact);
            }
        }
    }
}
