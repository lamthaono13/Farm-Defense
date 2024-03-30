using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHealthEnermy : WeaponBase
{
    [SerializeField] private GameObject objVfxHealth;

    [SerializeField] private int numberHealth;

    [SerializeField] private float radiusMax;

    public override void Attack()
    {
        var enermies = CharManager.Instance.Enermies;

        float minDistance = 10000000;

        //Vector3 positonGet = Vector3.zero;

        List<HealthBase> listHealth = new List<HealthBase>();

        int count = 0;

        for (int i = 0; i < enermies.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, enermies[i].GetBody().position);

            if (distance <= radiusMax)
            {
                HealthBase health = enermies[i].GetHealth();

                if (health.GetHealth() > 0)
                {
                    //positonGet = health.GetPositionHealth();

                    count++;

                    Instantiate(objVfxHealth, enermies[i].GetBody());

                    health.AddHealth(health.GetHealthMax() * 0.03f, "");


                    Debug.Log("heal");

                    if(count > numberHealth)
                    {
                        break;
                    }

                    //listHealth.Add(health);

                    //minDistance = distance;
                }
            }
        }
    }
}
