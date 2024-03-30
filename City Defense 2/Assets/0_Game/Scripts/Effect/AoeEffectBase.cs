using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeEffectBase : MonoBehaviour
{
    private IContactObject icoContactObject;

    private float damage;

    private float damagePerSecond;

    private float timeDestroy;

    private float timeCountDamage;

    private float timeCountDestroy;

    private bool canDamage;

    private GameObject objEffectPoison;

    EffectBase effectPoison;

    ObjectBase objectBase;

    // Start is called before the first frame update
    void Start()
    {
        objectBase = GetComponent<ObjectBase>();

        GameObject objLoad = ResourceManager.Instance.Load("Effect/EffectPoison");

        objEffectPoison = Instantiate(objLoad, transform);

        objEffectPoison.transform.localPosition = Vector3.zero;

        objEffectPoison.transform.localRotation = Quaternion.Euler(0, 0, 0);

        //float x = transform.lossyScale.x / transform.localScale.x;

        //float y = transform.lossyScale.y / transform.localScale.y;

        //float z = transform.lossyScale.z / transform.localScale.z;

        //objEffectPoison.transform.localScale = new Vector3(0.25f / x, 0.25f / y, 0.25f / z);

        objEffectPoison.transform.localScale = Vector3.one * 0.25f;

        effectPoison = objEffectPoison.GetComponent<EffectBase>();
    }

    public void Init(float _damage, float _damagePerSecond, float _timeDestroy)
    {
        icoContactObject = GetComponent<IContactObject>();

        damage = _damage;

        damagePerSecond = _damagePerSecond;

        timeCountDamage = damagePerSecond;

        timeDestroy = _timeDestroy;

        canDamage = true;
    }

    public void ResetEffect()
    {
        timeCountDestroy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        objEffectPoison.transform.position = objectBase.GetBody().position;

        if (canDamage)
        {
            if(icoContactObject.GetHealth().GetHealth() <= 0)
            {
                Destroy(objEffectPoison);

                Destroy(this);
            }

            if(timeCountDestroy >= timeDestroy)
            {
                effectPoison.DestroyEffect();

                Destroy(objEffectPoison);

                Destroy(this);

                return;
            }
            else
            {
                timeCountDestroy += Time.deltaTime;
            }

            if(timeCountDamage >= damagePerSecond)
            {
                Damage();

                timeCountDamage = 0;
            }
            else
            {
                timeCountDamage += Time.deltaTime;
            }
        }
    }

    private void Damage()
    {
        icoContactObject.Hited(TypeWeapon.AOE, damage);
    }
}
