using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolAlly : MonoBehaviour
{
    [SerializeField] private int numberSqawn;

    private List<List<GameObject>> listAllySqawn;

    private List<List<GameObject>> listAllyContainSqawn;

    private List<List<GameObject>> listAllyHasSqawnSqawn;

    private List<GameObject> listLoad;

    public void Init()
    {
        listAllySqawn = new List<List<GameObject>>();

        listAllyContainSqawn = new List<List<GameObject>>();

        listAllyHasSqawnSqawn = new List<List<GameObject>>();

        listLoad = new List<GameObject>();

        for(int i = 0; i < Enum.GetNames(typeof(TypeSlotEquip)).Length; i++)
        {
            //int levelAlly = GameManager.Instance.DataManager.GetLevelAlly((TypeAlly)i);
            int levelAlly = 1;

            TypeEquip typeEquip = GameManager.Instance.DataManager.GetEquipAlly((TypeSlotEquip)i);

            GameObject obj = ResourceManager.Instance.Load("Ally/" + (typeEquip.TypeGroup).ToString() + "/" + (typeEquip.TypeGroup).ToString() + " Ally " + (typeEquip.TypeTier).ToString()); // Resources.Load<GameObject>("Ally/" + (typeEquip.TypeGroup).ToString() + "/" + (typeEquip.TypeGroup).ToString() + " Ally " + (typeEquip.TypeTier).ToString());

            listLoad.Add(obj);
        }

        for(int i = 0; i < Enum.GetNames(typeof(TypeSlotEquip)).Length; i++)
        {
            //int levelAlly = GameManager.Instance.DataManager.GetLevelAlly((TypeAlly)i);

            List<GameObject> listCoppy = new List<GameObject>();

            TypeEquip typeEquip = GameManager.Instance.DataManager.GetEquipAlly((TypeSlotEquip)i);

            for (int j = 0; j < numberSqawn; j++)
            {
                //GameObject obj = Resources.Load<GameObject>("Ally/" + ((TypeAlly)i).ToString() + "/" + ((TypeAlly)i).ToString() + " Level " + levelAlly);

                GameObject obj = listLoad[i];

                GameObject objCoppy = Instantiate(obj, transform);

                listCoppy.Add(objCoppy);

                bool a = objCoppy.TryGetComponent<ObjectBase>(out ObjectBase characterBase);

                if (a)
                {
                    characterBase.InitIndexConfig(new DataSqawn() 
                    {
                        level = GameManager.Instance.DataManager.GetLevelAlly(typeEquip.TypeGroup, typeEquip.TypeTier, typeEquip.TypeId),
                        PostionSqawn = transform.position
                    });
                }

            }

            listAllySqawn.Add(listCoppy);

            listAllyContainSqawn.Add(listCoppy);

            listAllyHasSqawnSqawn.Add(new List<GameObject>());
        }
    }

    public GameObject Sqawn(TypeSlotEquip typeSlotEquip, Vector3 positionSqawn)
    {
        //Debug.Log(listAllyContainSqawn[(int)typeAlly].Count);

        if(listAllyContainSqawn[(int)typeSlotEquip].Count > 0)
        {
            GameObject obj = listAllyContainSqawn[(int)typeSlotEquip][0];

            listAllyContainSqawn[(int)typeSlotEquip].RemoveAt(0);

            listAllyHasSqawnSqawn[(int)typeSlotEquip].Add(obj);

            obj.transform.position = positionSqawn;

            ObjectBase characterBase = obj.GetComponent<ObjectBase>();

            characterBase.Init(typeSlotEquip);

            obj.gameObject.SetActive(true);

            return obj;
        }
        else
        {
            //int levelAlly = GameManager.Instance.DataManager.GetLevelAlly(typeAlly);
            int levelAlly = 1;

            GameObject obj = listLoad[(int)typeSlotEquip];

            GameObject objCoppy = Instantiate(obj, transform);

            bool a = objCoppy.TryGetComponent<ObjectBase>(out ObjectBase characterBase);

            if (a)
            {
                characterBase.InitIndexConfig(new DataSqawn());
            }

            listAllySqawn[(int)typeSlotEquip].Add(objCoppy);

            listAllyHasSqawnSqawn[(int)typeSlotEquip].Add(objCoppy);

            objCoppy.transform.position = positionSqawn;

            ObjectBase character = objCoppy.GetComponent<ObjectBase>();

            character.Init(typeSlotEquip);

            objCoppy.gameObject.SetActive(true);

            return objCoppy;
        }
    }

    public void DeSqawn(TypeSlotEquip typeSlotEquip, GameObject objReturn)
    {
        objReturn.gameObject.SetActive(false);

        listAllyContainSqawn[(int)typeSlotEquip].Add(objReturn);

        listAllyHasSqawnSqawn[(int)typeSlotEquip].Remove(objReturn);
    }
}