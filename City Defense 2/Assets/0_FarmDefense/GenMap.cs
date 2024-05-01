using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GenMap : MonoBehaviour
{
    [SerializeField] private List<GameObject> objPrefabMaps;

    public List<GameObject> objMaps;

    [Button]
    public void GenRandomMap(int numberWidth, int numberHeight)
    {
        try
        {
            for(int i = 0; i < objMaps.Count; i++)
            {
                GameObject objRemove = objMaps[i];

                Destroy(objRemove);
            }

            objMaps.Clear();
        }
        catch
        {

        }

        float width = 0.71f;

        float height = 0.71f;

        float rootX = (numberWidth / 2 - 0.5f) * (- width);

        float rootY = (numberHeight / 2 - 0.5f) * (-height);

        for(int i = 0; i < numberHeight; i++)
        {
            for(int j = 0; j < numberWidth; j++)
            {
                int randomNumber = Random.Range(0, objPrefabMaps.Count);

                GameObject objInstance = Instantiate(objPrefabMaps[randomNumber], transform);

                objInstance.transform.localPosition = new Vector3(rootX, rootY, 0);

                objMaps.Add(objInstance);

                rootX += width;
            }

            rootY += height;

            rootX = (numberWidth / 2 - 0.5f) * (-width);
        }
    }
}
