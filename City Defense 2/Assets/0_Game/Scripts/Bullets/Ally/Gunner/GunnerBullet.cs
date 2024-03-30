using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerBullet : BulletBase
{
    //[SerializeField] private float timeToDestroy;

    [SerializeField] private float timePerSecond;

    [SerializeField] private float timeToEndFire;

    [SerializeField] private GameObject objEffect;

    //[SerializeField] protected float speedMove;

    //protected bool canMove;

    private Coroutine coroutine;

    [SerializeField] private float radiusImpact;

    [SerializeField] protected float radiusBreak;

    //[SerializeField] private GameObject objBoom;

    public override void OnShoot(float _damage, Vector3 _postionTarget, Vector3 _direction)
    {
        base.OnShoot(_damage, _postionTarget, _direction);

        canMove = true;

        float u = Vector3.Distance(transform.position, postionTarget);

        float v = speedMove;

        transform.DOMove(postionTarget, u / v).SetEase(DG.Tweening.Ease.Linear).OnComplete(() => { OnBreak(); });

        radiusBreak = GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Ally_C_Range_AOE);

        //coroutine = StartCoroutine(WaitDestroy());
    }

    public override void Update()
    {
        base.Update();
    }

    protected virtual void OnBreak()
    {
        //GameObject objInstan = Instantiate(objBoom, transform.position, Quaternion.identity);

        //objInstan.transform.localScale = Vector3.one * radiusBreak;

        //canMove = false;

        GameManager.Instance.SoundManager.PlaySoundBoom();

        var enermies = CharManager.Instance.Enermies;

        GameObject obj = Instantiate(objEffect, transform.position, Quaternion.identity);

        obj.transform.localScale = Vector3.one * radiusBreak;

        //float minDistance = 10000000;

        //List<IContactObject> contactObjectsEnermy = new List<IContactObject>();

        for (int i = 0; i < enermies.Count; i++)
        {
            IContactObject icontactEnermy = enermies[i];

            float distance = Vector3.Distance(transform.position, icontactEnermy.GetBody().position);

            if (distance <= radiusImpact)
            {
                if (icontactEnermy.GetHealth().GetHealth() > 0)
                {
                    //minDistance = distance;

                    icontactEnermy.Hited(TypeWeapon.Range, damage * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Ally_C_Percent_Normal_Damage));

                    //continue;
                }
            }

            if (distance <= radiusBreak)
            {
                if (icontactEnermy.GetHealth().GetHealth() > 0)
                {
                    //minDistance = distance;

                    icontactEnermy.Hited(TypeWeapon.AOE, damage * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Ally_C_Percent_AOE_Damage));
                }
            }
        }

        //for (int i = 0; i < healthBases.Count; i++)
        //{
        //    //break

        //    if (!healthBases[i].gameObject.TryGetComponent<Fire>(out Fire fire1))
        //    {
        //        Fire fire = healthBases[i].gameObject.AddComponent<Fire>();

        //        fire.Init(damage, timePerSecond, timeToEndFire);
        //    }
        //    else
        //    {
        //        fire1.ResetEffect();
        //    }
        //}

        Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        //if (canMove)
        //{
        //    ri.MovePosition(direction * speedMove * Time.fixedDeltaTime);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Enermy"))
        //{
        //    ChildImpactDetect enermy = collision.GetComponent<ChildImpactDetect>();

        //    Enermy enermy1 = enermy.ObjectBase.GetComponent<Enermy>();

        //    if (enermy1.IsFlying)
        //    {
        //        enermy1.OnBreak();

        //        return;
        //    }


        //    HealthBase healthBase = enermy.ObjectBase.GetComponent<HealthBase>();

        //    healthBase.SubHealth(damage, "BulletDamage");

        //    StopCoroutine(coroutine);

        //    Destroy(gameObject);
        //}
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.yellow;

        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radiusBreak);
    }

#endif
}
