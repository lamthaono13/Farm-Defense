using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Yade.Runtime;

[CreateAssetMenu(menuName = "DataGame")]
public class DataGame : SerializedScriptableObject
{
    public List<DataLevel> DataLevels;

    public float TimeDelay;

    public int HealthInGame;

    public int MaxTicket;

    public int SecondToRebornTicket;

    public int TicketReborn;

    public int TicketPlay;

    public int TicketStart;

    public int GoldStart;

    public int GemStart;

    public int NuclearStart;

    public List<DataGrowConfigLevel> DataGrowConfigLevels;

    public void Init(YadeSheetData yadeSheet, YadeSheetData yadeSheetDataShop, float timeDelay)
    {
        TimeDelay = timeDelay;

        DataLevels = new List<DataLevel>();

        DataLevels.Clear();

        // index

        int indexStageId = 0;

        int indexQuest_1 = 1;

        int indexQuest_2 = 2;

        int indexEnergyCap = 3;

        int indexGoldDrop = 4;

        int indexGemDrop = 5;

        int indexWaveId = 6;

        int indexLaneSqawn = 7;

        int indexDelaySqawn = 8;

        int indexEnermyId = 9;

        int indexEnermyLevel = 10;

        int indexNumberEnermy = 11;

        int indexTotalEnermy = 12;

        int indexMapId = 13;

        int indexEnergySub = 14;


        // Get Start Index

        int indexRowStart = 2;

        int indexColumnHealthGame = 6;

        int indexColumnGoldStart = 7;

        int indexColumnGemStart = 8;

        int indexColumnTicketStart = 9;

        int indexColumnMaxTicket = 10;

        int indexColumnSecond = 11;

        int indexColumnTicketReborn = 12;

        int indexColumnTicketNeedToPlay = 13;

        int indexColumnNuclearStart = 14;


        HealthInGame = yadeSheetDataShop.GetCell(indexRowStart, indexColumnHealthGame).GetInt();

        GoldStart = yadeSheetDataShop.GetCell(indexRowStart, indexColumnGoldStart).GetInt();

        GemStart = yadeSheetDataShop.GetCell(indexRowStart, indexColumnGemStart).GetInt();

        TicketStart = yadeSheetDataShop.GetCell(indexRowStart, indexColumnTicketStart).GetInt();

        MaxTicket = yadeSheetDataShop.GetCell(indexRowStart, indexColumnMaxTicket).GetInt();

        SecondToRebornTicket = yadeSheetDataShop.GetCell(indexRowStart, indexColumnSecond).GetInt();

        TicketReborn = yadeSheetDataShop.GetCell(indexRowStart, indexColumnTicketReborn).GetInt();

        TicketPlay = yadeSheetDataShop.GetCell(indexRowStart, indexColumnTicketNeedToPlay).GetInt();

        NuclearStart = yadeSheetDataShop.GetCell(indexRowStart, indexColumnNuclearStart).GetInt();

        // logic

        //Debug.Log(yadeSheet.GetCell(2, 0).ToString());

        int countDatalevel = -1;

        int checkStateID = 0;

        int row = 1;

        float coinEarn = 0;

        float delaySqawn = 0;

        List<TypeEquip> typeEquips = new List<TypeEquip>();

        while (yadeSheet.GetCell(row, indexWaveId)!= null)
        {
            //get index

            int waveId = yadeSheet.GetCell(row, indexWaveId).GetInt();

            int landSqawn = yadeSheet.GetCell(row, indexLaneSqawn).GetInt();

            delaySqawn = yadeSheet.GetCell(row, indexDelaySqawn).GetFloat();

            int enermyId = yadeSheet.GetCell(row, indexEnermyId).GetInt();

            int enermyLv = yadeSheet.GetCell(row, indexEnermyLevel).GetInt();

            int numberEnermy = yadeSheet.GetCell(row, indexNumberEnermy).GetInt();

            // logic id enermy

            string a = enermyId.ToString();

            Debug.Log(a);

            //int typeGroup = System.Int32.Parse(a[0].ToString());

            //int typeTier = System.Int32.Parse(a[1].ToString());

            //int typeId = System.Int32.Parse(a[3].ToString());

            //TypeEquip typeEquip = new TypeEquip() { TypeGroup = (TypeGroup)(typeGroup - 1), TypeTier = (TypeTier)(typeId - 1), TypeId = TypeId.Id0 };

            //bool checkContain = false;

            //for(int i = 0; i < typeEquips.Count; i++)
            //{
            //    if(typeEquips[i].TypeGroup == typeEquip.TypeGroup && typeEquips[i].TypeTier == typeEquip.TypeTier && typeEquips[i].TypeId == typeEquip.TypeId)
            //    {
            //        checkContain = true;
            //    }
            //}

            //if (!checkContain)
            //{
            //    typeEquips.Add(typeEquip);
            //}

            // logic

            if (yadeSheet.GetCell(row, indexStageId)!= null)
            {
                int stageId = yadeSheet.GetCell(row, indexStageId).GetInt();

                if (stageId != checkStateID)
                {
                    int idQuest_1 = yadeSheet.GetCell(row, indexQuest_1).GetInt();

                    int idQuest_2 = yadeSheet.GetCell(row, indexQuest_2).GetInt();

                    int energy = yadeSheet.GetCell(row, indexEnergyCap).GetInt();

                    float goldGrop = yadeSheet.GetCell(row, indexGoldDrop).GetFloat();

                    float gemDrop = yadeSheet.GetCell(row, indexGemDrop).GetFloat();

                    int mapId = yadeSheet.GetCell(row, indexMapId).GetInt();

                    float totalEnermy = yadeSheet.GetCell(row, indexTotalEnermy).GetFloat();

                    bool EnergySub = yadeSheet.GetCell(row, indexEnergySub).GetInt() == 1 ? true : false;

                    coinEarn = goldGrop / totalEnermy;

                    checkStateID = stageId;

                    //

                    int typeGroup = System.Int32.Parse(a[0].ToString());

                    int typeTier = System.Int32.Parse(a[1].ToString());

                    int typeId = System.Int32.Parse(a[3].ToString());

                    TypeEquip typeEquip = new TypeEquip() { TypeGroup = (TypeGroup)(typeGroup - 1), TypeTier = (TypeTier)(typeId - 1), TypeId = TypeId.Id0 };


                    //


                    DataLevels.Add(new DataLevel(new DataInitDataLevel() 
                    {
                       DataMap = new DataMap() 
                       {
                           TypeShapeMap = (TypeShapeMap)(mapId - 1)
                       },
                       

                       IndexQuest_1 = idQuest_1,
                       IndexQuest_2 = idQuest_2,
                       Energy = energy,
                       GoldDrop = goldGrop,
                       GemDrop = gemDrop,
                       DataTurnEnermies = new DataTurnEnermy(enermyId, enermyLv, numberEnermy, (DirectionSqawn)(landSqawn - 1), delaySqawn, coinEarn) 

                    }, (int)gemDrop, EnergySub, typeEquip));

                    countDatalevel++;


                }
            }
            else
            {
                int typeGroup = System.Int32.Parse(a[0].ToString());

                int typeTier = System.Int32.Parse(a[1].ToString());

                int typeId = System.Int32.Parse(a[3].ToString());

                TypeEquip typeEquip = new TypeEquip() { TypeGroup = (TypeGroup)(typeGroup - 1), TypeTier = (TypeTier)(typeId - 1), TypeId = TypeId.Id0 };

                //

                DataLevels[countDatalevel].SetDataLevel(new DataTurnEnermy(enermyId, enermyLv, numberEnermy, (DirectionSqawn)(landSqawn - 1), delaySqawn, coinEarn), typeEquip);

                //
            }

            row++;


        }

        //Debug.Log(DataLevels[0].GoldDrop);
    }
}

public class DataGrowConfigLevel
{
    public int NumberCheckConfigLevel;

    public float IndexConfigLevel;
}
