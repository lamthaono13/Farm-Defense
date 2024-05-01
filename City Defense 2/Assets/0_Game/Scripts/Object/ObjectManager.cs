using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour, ISingleton
{
    public static ObjectManager Instance;

    [SerializeField] private NavMeshManager navMeshManager;
    
    protected Transform postionFinish;

    //[SerializeField] private List<Map> listObjTypeShapeMap;

    protected Map map;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitSingleton()
    {
        Instance = this;
    }
    
    public virtual void Init(DataMap dataMap)
    {
        int idMap = GameManager.Instance.DataManager.GetMap();

        if (dataMap.DataSpecialObjects != null)
        {

            for (int i = 0; i < dataMap.DataSpecialObjects.Count; i++)
            {
                // Instantiate Special Object in Here

                GameObject objLoad = Resources.Load<GameObject>("SpecialObject/" + dataMap.DataSpecialObjects[i].specialObj.ToString());

                GameObject objEnermy = Instantiate(objLoad, dataMap.DataSpecialObjects[i].position, dataMap.DataSpecialObjects[i].rotation);
            }
        }

        int idTypeMap = (int)dataMap.TypeShapeMap;

        if (GameManager.Instance.IsGameDesign)
        {
            idTypeMap = 0;

            idMap = 1;
        }

        //Debug.LogError((idMap + 1).ToString());


        GameObject objLoadMap = Resources.Load<GameObject>("Map/Map" + (idMap).ToString() + "/TypeMap " + (idTypeMap + 1).ToString());

        GameObject objMap = Instantiate(objLoadMap, transform);



        //postionFinish = listObjTypeShapeMap[idTypeMap].GetFinish();

        //for (int i = 0; i < listObjTypeShapeMap.Count; i++)
        //{
        //    if(i == idTypeMap)
        //    {
        //        listObjTypeShapeMap[i].gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        listObjTypeShapeMap[i].gameObject.SetActive(false);
        //    }
        //}

        //map = listObjTypeShapeMap[idTypeMap];

        map = objMap.GetComponent<Map>();

        postionFinish = map.GetFinish();

        //map.SetActiveObjBake(true);
    }

    public Vector3 GetPositionFinish()
    {
        return postionFinish.position;
    }

    //public Vector3 GetRandomGoStraight(Vector3 positionCurrent)
    //{

    //}

    public void DeActiveObjBake()
    {
        map.SetActiveObjBake(false);
    }

    public void ActiveWarning(DirectionSqawn directionSqawn)
    {
        map.ActiveWarning(directionSqawn);
    }

    public Map GetMap()
    {
        return map;
    }
}
