using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    public void Init(TypeEffect typeEffect, Transform body);

    public void Init(TypeEffect typeEffect, Vector3 positionSqawn);
}

public enum TypeEffect
{
    FollowBody,
    
}
