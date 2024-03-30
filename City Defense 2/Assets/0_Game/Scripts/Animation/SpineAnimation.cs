using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
using System;

public class SpineAnimation : AnimationBase
{
    //[SerializeField] private bool hasMixSkin;
    
    [SerializeField] private SkeletonAnimation skeletonAnimation;

    public override void Animate(TypeAnimation _typeAnimation, float speed, bool isLoop, Action _actionEvent, Action _actionComplete)
    {
        base.Animate(_typeAnimation, speed, isLoop, _actionEvent, _actionComplete);

        skeletonAnimation.AnimationState.TimeScale = speed * GetSpeedBaseAnimation(_typeAnimation);
        
        skeletonAnimation.AnimationState.SetAnimation(0, GetNameAnimation(_typeAnimation), isLoop);
    }

    public override void Start()
    {
        base.Start();

        skeletonAnimation.AnimationState.Event += OnAnimationEvent;

        skeletonAnimation.AnimationState.Complete += OnAnimationComplete;

        //if (hasMixSkin)
        //{
        //    int a = UnityEngine.Random.Range(0, Enum.GetNames(typeof(TypeEyesSkin)).Length);

        //    skeletonAnimation.AnimationState.SetAnimation(1, ((TypeEyesSkin)a).ToString(), true);
        //}

    }

    public override void SetFlip(bool isFlip)
    {
        base.SetFlip(isFlip);

        if (isFlip)
        {
            skeletonAnimation.transform.localRotation = Quaternion.Euler(-90, 180, 0);
        }
        else
        {
            skeletonAnimation.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
    }

    public override IEnumerator WaitForTurnOffAnimation(string nameAnimation)
    {
        return base.WaitForTurnOffAnimation(nameAnimation);
    }

    public override void OnChangeMaterial(Material[] materialsOrigin, List<Material> materialsChange)
    {
        base.OnChangeMaterial(materialsOrigin, materialsChange);

        for (int i = 0; i < materialsOrigin.Length; i++)
        {
            skeletonAnimation.CustomMaterialOverride.Add(materialsOrigin[i], materialsChange[i]);
        }
    }

    protected virtual void OnAnimationComplete(TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name.Contains(GetNameAnimation(currentTypeAnimation)))
        {
            actionComplete?.Invoke();
        }
    }

    protected virtual void OnAnimationEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if(trackEntry == null || e == null)
        {
            return;
        }

        // Check if the event key string matches "hit"
        if (e.Data.Name.Equals(GetEventNameAnimation(currentTypeAnimation)))
        {
            if (trackEntry.Animation.Name != GetNameAnimation(TypeAnimation.Die))
            {
                actionEvent?.Invoke();
            }
        }
    }

    public override float GetTimeAnimation(TypeAnimation typeAnimation, bool hasSpeed)
    {
        if (hasSpeed)
        {
            return skeletonAnimation.Skeleton.Data.FindAnimation(GetNameAnimation(typeAnimation)).Duration * GetSpeedBaseAnimation(typeAnimation);
        }
        else
        {
            return skeletonAnimation.Skeleton.Data.FindAnimation(GetNameAnimation(typeAnimation)).Duration;
        }
        

    }
}
