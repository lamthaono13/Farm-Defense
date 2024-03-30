using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Yade.Runtime;

[CreateAssetMenu(menuName = "DataUnlock")]
public class DataUnlock : SerializedScriptableObject
{
    public List<DataEachUnlock> dataEachUnlocks;

    public void Init(YadeSheetData yadeSheet)
    {
        // index

        int columnBarrier = 1;

        int columnMelee = 2;

        int columnRange = 3;

        int indexGold = 4;

        int indexGoldGrow = 5;

        // Barrier

        //List<float> listBarrier = new List<float>();

        //for(int i = 1; i < yadeSheet.GetRowCount(); i++)
        //{
        //    float a = Mathf.Round(yadeSheet.GetCell(i, columnBarrier).GetFloat());

        //    listBarrier.Add(a);
        //}

        //dataEachUnlocks[0].Init(yadeSheet.GetCell(columnBarrier, indexGold).GetFloat(), yadeSheet.GetCell(columnBarrier, indexGoldGrow).GetFloat());

        // Melee

        //List<float> listMelee = new List<float>();

        //for (int i = 1; i < yadeSheet.GetRowCount(); i++)
        //{
        //    float a = Mathf.Round(yadeSheet.GetCell(i, columnMelee).GetFloat());

        //    listMelee.Add(a);
        //}

        //dataEachUnlocks[1].Init(yadeSheet.GetCell(columnMelee, indexGold).GetFloat(), yadeSheet.GetCell(columnMelee, indexGoldGrow).GetFloat());

        // Range

        //List<float> listRange = new List<float>();

        //for (int i = 1; i < yadeSheet.GetRowCount(); i++)
        //{
        //    float a = Mathf.Round(yadeSheet.GetCell(i, columnRange).GetFloat());

        //    listRange.Add(a);
        //}

        //dataEachUnlocks[2].Init(yadeSheet.GetCell(columnRange, indexGold).GetFloat(), yadeSheet.GetCell(columnRange, indexGoldGrow).GetFloat());
    }
}