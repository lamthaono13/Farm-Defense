using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataSkinChar")]
public class DataSkinChar : SerializedScriptableObject
{
    public List<List<string>> ListNameSkin;

    public string GetNameSkin(int idAlly, int id)
    {
        return ListNameSkin[idAlly][id];
    }
}
