using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataGetGem")]
public class DataGetGem : SerializedScriptableObject
{
    public List<ElementGetGem> ElementGetGems;
}

public class ElementGetGem
{
    public Buy Buy;

    public float IndexEarn;
}
