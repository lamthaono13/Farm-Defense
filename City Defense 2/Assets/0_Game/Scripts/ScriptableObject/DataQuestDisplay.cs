using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "DataQuestDisplay")]
public class DataQuestDisplay : SerializedScriptableObject
{
    public List<string> ListQuestStringDisplay;

    public void Init(List<string> _ListQuestStringDisplay)
    {
        ListQuestStringDisplay = _ListQuestStringDisplay;
    }

    public string GetDisplayString(int idQuest)
    {
        return ListQuestStringDisplay[idQuest];
    }
}
