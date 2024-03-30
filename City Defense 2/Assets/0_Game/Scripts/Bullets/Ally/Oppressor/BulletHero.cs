using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHero : BulletBase
{
    [SerializeField] private Collider2D collider;

    [SerializeField] private ParticleSystem particleSystemShoot;

    private Coroutine coroutine;

    public override void OnShoot(float _damage, Vector3 _postionTarget, Vector3 _direction)
    {
        base.OnShoot(_damage, _postionTarget, _direction);

        collider.enabled = true;

        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        particleSystemShoot.Play();

        //coroutine = StartCoroutine(DeActiveActiveCollider());
    }

    public override void OnStopShoot()
    {
        base.OnStopShoot();

        collider.enabled = false;

        particleSystemShoot.Stop();
    }

    private IEnumerator DeActiveActiveCollider()
    {
        yield return new WaitForSeconds(0.3f);

        collider.enabled = false;

        coroutine = null;

        particleSystemShoot.Stop();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enermy"))
        {
            //Enermy enermy = collision.gameObject.GetComponent<ChildImpactDetect>().ObjectBase.GetComponent<Enermy>();

            //if (enermy.IsFlying && enermy.CanBreak)
            //{
            //    enermy.OnBreak();
            //}

            //if (enermy.IsBoss)
            //{
            //    enermy.Health.SubHealth(enermy.Health.GetHealthMax() * 0.0005f, "");
            //}
            //else
            //{
            //    enermy.Health.SubHealth(damage, "");
            //}
        }
    }
}
