using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiBase : MonoBehaviour, IUi
{
    [SerializeField] private TypeSort _typeSort;
    
    public virtual void Show(bool isShow)
    {
                    
    }

    public virtual void Init<T>(T data)
    {
                
    }
}
