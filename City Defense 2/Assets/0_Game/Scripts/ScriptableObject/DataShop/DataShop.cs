using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataShop")]
public class DataShop : SerializedScriptableObject
{
    public DataGetPack DataGetPack;

    public DataSpecialWeapon DataSpecialWeapon;

    public DataGetGold DataGetGold;

    public DataGetGem DataGetGem;
}
