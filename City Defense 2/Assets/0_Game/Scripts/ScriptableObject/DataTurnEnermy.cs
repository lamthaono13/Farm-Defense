using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class DataTurnEnermy
{
    public float timeToSqawn;

    public DirectionSqawn DirectionSqawn;

    public List<DataSqawn> DataSqawns;

    public DataTurnEnermy(int _IdEnermy, int _level, int number, DirectionSqawn directionSqawn, float _timeToSqawn, float coinEnrn)
    {
        GenerateEnermy(_IdEnermy, _level, number, coinEnrn);

        DirectionSqawn = directionSqawn;

        timeToSqawn = _timeToSqawn;

        RandomPosition();
    }

    //[Button]
    public void RandomPosition()
    {
        switch (DirectionSqawn)
        {
            case DirectionSqawn.Staight:

                for (int i = 0; i < DataSqawns.Count; i++)
                {
                    DataSqawns[i].PostionSqawn = new Vector3(Random.Range(-2.8f, 2.8f), Random.Range(15f, 30f), 0);
                }

                break;
            case DirectionSqawn.Left:

                for (int i = 0; i < DataSqawns.Count; i++)
                {
                    DataSqawns[i].PostionSqawn = new Vector3(Random.Range(-10f, - 20f), Random.Range(2.5f, 6.5f), 0);
                }

                break;
            case DirectionSqawn.Right:

                for (int i = 0; i < DataSqawns.Count; i++)
                {
                    DataSqawns[i].PostionSqawn = new Vector3(Random.Range(10f, 20f), Random.Range(2.5f, 6.5f), 0);
                }

                break;
        }
    }

    //[Button]
    public void ClearEnermy()
    {
        DataSqawns.Clear();
    }

    //[Button]
    public void GenerateEnermy(int _IdEnermy, int _level, int number, float coinEarn)
    {
        DataSqawns = new List<DataSqawn>();

        for (int i = 0; i < number; i++)
        {
            DataSqawns.Add(new DataSqawn() 
            {
                IdEnermy = _IdEnermy,
                level = _level,
                CoinEarn = coinEarn
            });
        }
    }
}

public class DataSqawn
{
    public int IdEnermy;

    public Vector3 PostionSqawn;

    public int level;

    public float CoinEarn;
}
