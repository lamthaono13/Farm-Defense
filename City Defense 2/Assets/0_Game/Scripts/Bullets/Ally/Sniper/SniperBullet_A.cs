using System.Collections;
using System.Collections.Generic;
using Pathfinding.RVO;
using UnityEngine;

public class SniperBullet_A : SniperBullet
{
    protected float rateOneShot;
    
    protected override void OnAttack(IContactObject iContactObject)
    {
        //base.OnAttack(iContactObject);

        rateOneShot = GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Sniper_Ally_A_Percent_OneShot);

        int a = Random.Range(1, 101);

        if (a <= rateOneShot)
        {
            if (iContactObject.GetTypeGroup() == TypeGroup.Gunner)
            {
                iContactObject.Hited(TypeWeapon.OneShot, damage);
            }
            else
            {
                if (iContactObject.GetHealth().GetPercentHealth() <= GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Sniper_Ally_A_Condition_HP_Percent))
                {
                    iContactObject.Hited(TypeWeapon.OneShot, damage);
                }
                else
                {
                    iContactObject.Hited(typeWeapon, damage);
                }
            }
        }
        else
        {
            iContactObject.Hited(typeWeapon, damage);
        }
        
        Destroy(gameObject);
    }
}
