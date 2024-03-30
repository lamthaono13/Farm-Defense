using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "ScriptableObject/DataConfigCharacter")]
public class DataConfigCharacter : SerializedScriptableObject
{
    public float Health;

    public float Damage;

    public float RadiusCheck;

    public float RangeAttack;

    public float SpeedAttack;
    
    public float SpeedMove;
}