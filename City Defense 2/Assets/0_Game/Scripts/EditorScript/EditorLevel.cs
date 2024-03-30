using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class EditorLevel : MonoBehaviour
{
    public int currentLevel;

    [SerializeField] private GameObject objEditorMap;

    public EditorMap CurrentEditorMap;

    [SerializeField] private GameObject objEditorEnermy;

    public EditorEnermy CurrentEditorEnermy;

    public DataGame DataGame;

    public DataLevel DataLevel;

    public DataMap DataMap;

    public DataTurnEnermy DataTurnEnermy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [Button]
    public void InstantiateMapEditor()
    {
        GameObject obj = Instantiate(objEditorMap, Vector3.zero, Quaternion.identity);

        CurrentEditorMap = obj.GetComponent<EditorMap>();
    }

    [Button]
    public void SaveMapEditorToData()
    {
        DataMap.TypeShapeMap = CurrentEditorMap.TypeShapeMap;

        DataMap.TypeMap = CurrentEditorMap.typeMap;
    }

    [Button]
    public void InstantiateEnermyEditor()
    {
        GameObject obj = Instantiate(objEditorEnermy, Vector3.zero, Quaternion.identity);

        CurrentEditorEnermy = obj.GetComponent<EditorEnermy>();
    }

    [Button]
    public void SaveEnermyEditorToData()
    {
        DataTurnEnermy.DataSqawns.Add(new DataSqawn() 
        {
            //TypeEnermy = CurrentEditorEnermy.TypeEnermy,
            //Damage = CurrentEditorEnermy.Damage,
            //SpecialDamage = CurrentEditorEnermy.SpecialDamage,
            //Speed = CurrentEditorEnermy.Speed,
            //SpecialSpeed = CurrentEditorEnermy.SpecialSpeed,
            //PostionSqawn = CurrentEditorEnermy.transform.position,
            //EnergyErn = CurrentEditorEnermy.EnergyErn
        });
    }

    [Button]
    public void ClearCurrentEnermyEditor()
    {

    }

    [Button]
    public void SaveLevel()
    {

    }

    //[Button]
    //public void LoadLevel(int level)
    //{
    //    currentLevel = level;

    //    DataGame = Resources.Load<DataGame>("Data/Data Game");

    //    DataLevel = Resources.Load<DataLevel>("Data/Level " + level.ToString() + "/Data Level");

    //    DataMap = Resources.Load<DataMap>("Data/Level " + level.ToString() + "/Data Map");
    //}

    //[Button]
    //public void LoadTurn(int turn)
    //{
    //    DataTurnEnermy = Resources.Load<DataTurnEnermy>("Data/Level " + currentLevel.ToString() + "/Data Turn Enermy " + turn.ToString());
    //}

    [Button]
    public void Saveturn()
    {
        DataLevel.DataTurnEnermy.Add(DataTurnEnermy);
    }

    private void OnValidate()
    {

    }
}
