using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EditorEnermy : MonoBehaviour
{
    public TypeEnermy TypeEnermy;

    [SerializeField] private List<GameObject> listSkin;

    public float Speed;

    public float SpecialSpeed;

    public float Damage;

    public float SpecialDamage;

    public int EnergyErn;

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
#if UNITY_EDITOR

        for(int i = 0; i < listSkin.Count; i++)
        {
            if(i == (int)TypeEnermy)
            {
                listSkin[i].gameObject.SetActive(true);
            }
            else
            {
                listSkin[i].gameObject.SetActive(false);
            }
        }

#endif
    }
}
