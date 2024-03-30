using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEnermy : MonoBehaviour
{
    private List<GameObject> listSqawn;
    private List<GameObject> listContainSqawn;
    private List<GameObject> listHasSqawnSqawn;

    [SerializeField] private int numberSqawn;

    public void Init()
    {
        //listSqawn = new List<GameObject>();
        //listContainSqawn = new List<GameObject>();
        //listHasSqawnSqawn = new List<GameObject>();

        //for(int i = 0; i < typeEnermies.Count; i++)
        //{
        //    for(int j = 0; j < numberSqawn; j++)
        //    {

        //    }
        //}
    }

    public GameObject Sqawn(TypeEnermy typeEnermy, Vector3 positionSqawn)
    {
        return null;
    }

    public void DeSqawn(GameObject objReturn)
    {
        
    }
}
