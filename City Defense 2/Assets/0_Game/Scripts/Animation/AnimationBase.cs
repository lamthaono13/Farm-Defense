using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class AnimationBase : MonoBehaviour
{
    [SerializeField] protected bool hasGun;

    [SerializeField] private bool noRotate;

    [ShowIf("hasGun")] [SerializeField] private Transform rotateGun;

    [SerializeField] protected DataConfigAnimation dataConfigAnimation;
    
    private Tween tween;

    protected bool _isFlip;
    
    protected Action actionEvent;

    protected Action actionComplete;
    
    [SerializeField] private bool isUseRotateConfig;

    [SerializeField] private Vector3 rotateConfig;

    protected float speed;

    protected TypeAnimation currentTypeAnimation;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    public void Init(float _speed)
    {
        speed = _speed;
    }

    public virtual void Animate(TypeAnimation _typeAnimation, float speed, bool isLoop, Action _actionEvent, Action _actionComplete)
    {
        currentTypeAnimation = _typeAnimation;
        
        actionEvent = _actionEvent;
        
        actionComplete = _actionComplete;
    }

    public virtual IEnumerator WaitForTurnOffAnimation(string nameAnimation)
    {
        yield return new WaitForSeconds(0.001f);
    }

    public virtual void SetFlip(bool isFlip)
    {
        _isFlip = isFlip;
    }

    public virtual void SetToIdle()
    {
        if (noRotate)
        {
            return;
        }

        if (rotateGun != null)
        {
            if (isUseRotateConfig)
            {
                tween = rotateGun.transform.DOLocalMove(rotateConfig, 0.3f).OnComplete(() => { tween = null; });
            }
            else
            {
                tween = rotateGun.transform.DOLocalMove(new Vector3(10, 1, 0), 0.3f).OnComplete(() => { tween = null; });
            }


        }
    }

    public void Rotate(Vector3 target)
    {
        if(tween != null)
        {
            tween.Kill();

            tween = null;
        }

        if (hasGun)
        {
            tween = rotateGun.DOMove(target, 0.1f).SetEase(DG.Tweening.Ease.Linear);
        }
    }
     
    public virtual void OnChangeMaterial(Material[] materialsOrigin, List<Material> materialsChange)
    {

    }

    public virtual float GetTimeAnimation(TypeAnimation typeAnimation, bool hasSpeed)
    {
        return 0.5f;
    }

    public virtual string GetNameAnimation(TypeAnimation typeAnimation)
    {
        return dataConfigAnimation.DataEachAnimations[(int)typeAnimation].NameAnimtion;
    }

    public virtual string GetEventNameAnimation(TypeAnimation typeAnimation)
    {
        return dataConfigAnimation.DataEachAnimations[(int)typeAnimation].NameEvent;
    }

    public virtual float GetSpeedBaseAnimation(TypeAnimation typeAnimation)
    {
        return dataConfigAnimation.DataEachAnimations[(int)typeAnimation].speedAnimBase;
    }
}
