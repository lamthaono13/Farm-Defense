using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private List<TypeQuestGame> typeQuestGames;

    private List<QuestComplete> questCompletes;

    public void Init(List<TypeQuestGame> _typeQuestGames)
    {
        typeQuestGames = _typeQuestGames;

        questCompletes = new List<QuestComplete>();

        for(int i = 0; i < System.Enum.GetNames(typeof(TypeQuestGame)).Length; i++)
        {
            questCompletes.Add(new QuestComplete() { numberCount = 0 });
        }
    }

    public TypeQuestGame GetQuest_1()
    {
        return typeQuestGames[0];
    }

    public TypeQuestGame GetQuest_2()
    {
        return typeQuestGames[1];
    }

    public void SetQuest(TypeQuestGame typeQuestGame)
    {
        questCompletes[(int)typeQuestGame].numberCount++;

        switch (typeQuestGame)
        {
            case TypeQuestGame.Health_Percent_80:
                break;
            case TypeQuestGame.Health_Percent_60:
                break;
            case TypeQuestGame.Health_Percent_30:
                break;
            case TypeQuestGame.Use_Vanguard:
                break;
            case TypeQuestGame.Use_Sniper:
                break;
            case TypeQuestGame.Use_Gunner:
                break;
            case TypeQuestGame.Use_Oppressor:
                break;
            case TypeQuestGame.Use_Support_Item:
                break;
        }
    }

    public int CheckQuestCompleted()
    {
        int number = 0;

        float percentHealth = HealthGamePlay.Instance.GetPercentHealth();

        for(int i = 0; i < typeQuestGames.Count; i++)
        {
            switch (typeQuestGames[i])
            {
                case TypeQuestGame.Health_Percent_80:

                    if(percentHealth >= 0.8f)
                    {
                        number++;
                    }

                    break;
                case TypeQuestGame.Health_Percent_60:

                    if (percentHealth >= 0.6f)
                    {
                        number++;
                    }

                    break;
                case TypeQuestGame.Health_Percent_30:

                    if (percentHealth >= 0.3f)
                    {
                        number++;
                    }

                    break;
                case TypeQuestGame.Use_Vanguard:

                    if(questCompletes[(int)typeQuestGames[i]].numberCount > 0)
                    {
                        number++;
                    }

                    break;
                case TypeQuestGame.Use_Sniper:

                    if (questCompletes[(int)typeQuestGames[i]].numberCount > 0)
                    {
                        number++;
                    }

                    break;
                case TypeQuestGame.Use_Gunner:

                    if (questCompletes[(int)typeQuestGames[i]].numberCount > 0)
                    {
                        number++;
                    }

                    break;
                case TypeQuestGame.Use_Oppressor:

                    if (questCompletes[(int)typeQuestGames[i]].numberCount > 0)
                    {
                        number++;
                    }

                    break;
                case TypeQuestGame.Use_Support_Item:

                    if (questCompletes[(int)typeQuestGames[i]].numberCount > 0)
                    {
                        number++;
                    }

                    break;
                case TypeQuestGame.None:
                    number++;
                    break;
            }
        }

        return number;
    }
}

public class QuestComplete
{
    public int numberCount;
}
