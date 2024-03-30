using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataGetTicket")]
public class DataGetTicket : SerializedScriptableObject
{
    public List<ElementGetTicket> ElementGetTickets;
}

public class ElementGetTicket
{
    public Buy Buy;

    public float IndexEarn;
}
