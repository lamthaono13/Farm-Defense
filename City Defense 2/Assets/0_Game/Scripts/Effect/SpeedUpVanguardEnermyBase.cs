using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpVanguardEnermyBase : MonoBehaviour
{
    protected float timeEffect;

    protected float currentTimeEffect;

    protected float percentSlow;

    protected float initialSpeed;

    protected bool isStartEffect;

    protected Movement movement;

    public void Init(float _percentSlow, float _initialSpeed, float _timeEffect, Movement _movement)
    {
        percentSlow = _percentSlow;

        timeEffect = _timeEffect;

        initialSpeed = _initialSpeed;

        movement = _movement;

        movement.SetSpeed(initialSpeed * percentSlow);

        isStartEffect = true;
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

                Destroy(this);
            }
        }
    }
}