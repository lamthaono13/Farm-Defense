using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlowOppressorBase : MonoBehaviour
{
    protected float timeEffect;

    protected float currentTimeEffect;

    protected float initialSpeed;

    protected bool isStartEffect;

    protected Movement movement;

    private Tween tweenSpeed;

    private IContactObject contactObject;

    private GameObject objSlow;

    public void Init(float _initialSpeed, float _timeEffect, Movement _movement, IContactObject _contactObject)
    {
        timeEffect = _timeEffect;

        initialSpeed = _initialSpeed;

        movement = _movement;

        contactObject = _contactObject;

        SetSpeedToZero();

        isStartEffect = true;

        objSlow = Instantiate(ResourceManager.Instance.Load("Effect/Slow"), contactObject.GetBody());
    }

    public void ResetEffect()
    {
        //currentTimeEffect = 0;

        SetSpeedToZero();
    }

    public void SetSpeedToZero()
    {
        if(tweenSpeed != null)
        {
            tweenSpeed.Kill();
        }

        tweenSpeed = DOTween.To((x) => { movement.SetSpeed(x); }, initialSpeed, 0, timeEffect).OnComplete(() => 
        { 
            tweenSpeed = null;

            movement.SetSpeed(initialSpeed);

            Destroy(objSlow);

            Destroy(this);
        });

        
    }

    // Update is called once per frame
    void Update()
    {
        //if (isStartEffect)
        //{
        //    if (currentTimeEffect <= timeEffect)
        //    {
        //        currentTimeEffect += Time.deltaTime;
        //    }
        //    else
        //    {
        //        movement.SetSpeed(initialSpeed);

        //        Destroy(this);
        //    }
        //}
    }
}
