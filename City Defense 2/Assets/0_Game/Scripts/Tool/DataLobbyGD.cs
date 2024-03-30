using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataLobbyGD")]
public class DataLobbyGD : SerializedScriptableObject
{
    public int LevelChoose;

    public DataEquipSlot Slot1;
    public DataEquipSlot Slot2;
    public DataEquipSlot Slot3;
}

public class DataEquipSlot
{
    public TypeEquip TypeEquip;

    public int level;
}