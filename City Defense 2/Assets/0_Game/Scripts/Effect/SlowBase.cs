using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBase : MonoBehaviour
{
    protected float timeEffect;

    protected float currentTimeEffect;

    protected float percentSlow;

    protected float initialSpeed;

    protected bool isStartEffect;

    protected Movement movement;

    private IContactObject contactObject;

    private GameObject objSlow;

    public void Init(float _percentSlow, float _initialSpeed, float _timeEffect, Movement _movement, IContactObject _contactObject)
    {
        percentSlow = _percentSlow;

        timeEffect = _timeEffect;

        initialSpeed = _initialSpeed;

        movement = _movement;

        contactObject = _contactObject;

        movement.SetSpeed(initialSpeed * percentSlow);

        isStartEffect = true;

        objSlow = Instantiate(ResourceManager.Instance.Load("Effect/Slow"), contactObject.GetBody());
    }

    public void ResetEffect()
    {
        currentTimeEffect = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartEffect)
        {
            if (currentTimeEffect <= timeEffect)
            {
                currentTimeEffect += Time.deltaTime;
            }
            else
            {
                movement.SetSpeed(initialSpeed);

                Destroy(objSlow);

                Destroy(this);
            }
        }
    }
}
