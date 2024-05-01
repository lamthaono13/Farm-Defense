using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

public class NavMeshManager : MonoBehaviour
{
    //[SerializeField] private NavMeshSurface navMeshSurface;

    //[SerializeField] private List<NavMeshSurface> listShapeNavmesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    public void Build(int idShape)
    {
        //for (int i = 0; i < objsBake.Count; i++)
        //{
        //    objsBake[i].gameObject.SetActive(true);
        //}
        if (GameManager.Instance.IsGameDesign)
        {
            idShape = 0;
        }

        GameObject objLoad = Resources.Load<GameObject>("Navmesh/Navmesh Shape " + (idShape + 1).ToString());

        Instantiate(objLoad, transform);

        //listShapeNavmesh[idShape].gameObject.SetActive(true);

        //navMeshSurface.BuildNavMesh();

        //for (int i = 0; i < objsBake.Count; i++)
        //{
        //    objsBake[i].gameObject.SetActive(false);
        //}
    }
}
