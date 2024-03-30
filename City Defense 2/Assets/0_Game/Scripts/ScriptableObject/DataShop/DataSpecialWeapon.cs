using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataSpecialWeapon")]
public class DataSpecialWeapon : SerializedScriptableObject
{
    public List<ElementGetSpecial> ElementGetSpecials;
}

public class ElementGetSpecial
{
    public Buy Buy;

    public float IndexEarn;
}
