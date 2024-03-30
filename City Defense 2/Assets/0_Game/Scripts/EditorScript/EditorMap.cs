using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorMap : MonoBehaviour
{
    public TypeMap typeMap;

    public TypeShapeMap TypeShapeMap;

    [SerializeField] private List<GameObject> listMaps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        int id = (int)TypeShapeMap;

        for(int i = 0; i < listMaps.Count; i++)
        {
            if(i == id)
            {
                listMaps[i].gameObject.SetActive(true);
            }
            else
            {
                listMaps[i].gameObject.SetActive(false);
            }
        }
    }

    public void AddSpecialObject()
    {

    }
}
