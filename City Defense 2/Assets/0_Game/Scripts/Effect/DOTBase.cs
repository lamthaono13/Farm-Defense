using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTBase : MonoBehaviour
{
    protected float timeEffect;

    protected float currentTimeEffect;

    protected float damage;

    protected bool isStartEffect;

    protected int numberDamage;

    protected IContactObject iContactObject;

    private GameObject objEffect;

    public void Init(float _damage, float _timeEffect, IContactObject _iContactObject)
    {
        damage = _damage;

        timeEffect = _timeEffect;

        iContactObject = _iContactObject;

        isStartEffect = true;

        numberDamage = 1;

        currentTimeEffect = 1;

        GameObject objLoad = ResourceManager.Instance.Load("Effect/Ally/Fire");

        objEffect = Instantiate(objLoad, iContactObject.GetBody());
    }

    public void ResetEffect()
    {
        //currentTimeEffect = 0;
        currentTimeEffect -= (numberDamage - 1);
        numberDamage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartEffect)
        {
            if(iContactObject.GetHealth().GetHealth() <= 0)
            {
                Destroy(objEffect);
                Destroy(this);
                return;
            }

            if(currentTimeEffect <= timeEffect)
            {
                currentTimeEffect += Time.deltaTime;
            }
            else
            {
                Destroy(objEffect);
                Destroy(this);
            }

            if(currentTimeEffect >= numberDamage)
            {
                iContactObject.Hited(TypeWeapon.DOT, damage);

                numberDamage++;
            }
        }
    }
}
