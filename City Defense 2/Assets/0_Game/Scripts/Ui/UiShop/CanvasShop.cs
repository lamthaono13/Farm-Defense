using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasShop : MonoBehaviour
{
    [SerializeField] private UiShop uiShop;
    
    public UiShop GetUiShop()
    {
        return uiShop;
    }
}
