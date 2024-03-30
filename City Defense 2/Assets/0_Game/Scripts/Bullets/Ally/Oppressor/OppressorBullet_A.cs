using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppressorBullet_A : OppressorBullet
{
    private bool isShooting;

    private List<IContactObject> contactObjects;

    public override void OnShoot(float _damage, Vector3 _postionTarget, Vector3 _direction)
    {
        base.OnShoot(_damage, _postionTarget, _direction);
        contactObjects = new List<IContactObject>();
        isShooting = true;
    }

    protected override void OnAttack(IContactObject iContactObject)
    {
        //base.OnAttack(iContactObject);

        //StopCoroutine(coroutineDestroy);

        iContactObject.Hited(typeWeapon, damage);

        iContactObject.GetAbility().OnGetEffect(TypeEffectAttack.SlowOppressor);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //base.OnTriggerEnter2D(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enermy") && isShooting)
        {
            ChildImpactDetect enermy = other.GetComponent<ChildImpactDetect>();

            IContactObject contact = enermy.ObjectBase.GetComponent<IContactObject>();

            if (!contactObjects.Contains(contact))
            {
                contactObjects.Add(contact);

                OnAttack(contact);
            }
        }
    }
}
