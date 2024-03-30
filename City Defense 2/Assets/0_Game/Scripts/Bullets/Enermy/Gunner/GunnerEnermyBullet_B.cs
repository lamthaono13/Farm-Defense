using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerEnermyBullet_B : BulletBase
{
    private float radiusAttack;

    public override void OnShoot(float _damage, Vector3 _postionTarget, Vector3 _direction)
    {
        base.OnShoot(_damage, _postionTarget, _direction);

        GameManager.Instance.SoundManager.PlaySoundEnermyBoom();

        radiusAttack = GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Enemy_B_Range_AOE);

        var allies = CharManager.Instance.Allies;

        float minDistance = 10000000;

        for (int i = 0; i < allies.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, allies[i].GetBody().position);

            if (distance <= radiusAttack)
            {
                if (distance < minDistance)
                {
                    HealthBase health = allies[i].GetHealth();

                    if (health.GetHealth() > 0)
                    {
                        if (allies[i].GetTypeGroup() == TypeGroup.Barrier)
                        {
                            allies[i].Hited(TypeWeapon.AOE, damage * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Enemy_B_Index_Damage_AOE_Barrier));
                        }
                        else
                        {
                            allies[i].Hited(TypeWeapon.AOE, damage);

                            allies[i].GetAbility().OnGetEffect(TypeEffectAttack.DOTGunnerEnermy, GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Enemy_B_Index_Damage_DOT) * damage);
                        }
                    }
                }
            }
        }

        var enernies = CharManager.Instance.Enermies;

        for (int i = 0; i < enernies.Count; i++)
        {
            if (enernies[i] == null)
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, enernies[i].GetBody().position);

            if (distance <= radiusAttack)
            {
                if (distance < minDistance)
                {
                    HealthBase health = enernies[i].GetHealth();

                    if (health.GetHealth() > 0)
                    {
                        enernies[i].Hited(TypeWeapon.AOE, GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Enemy_C_Index_Damage_AOE_Enemy));
                    }
                }
            }
        }

        StartCoroutine(WaitDestroy());
    }

#if UNITY_EDITOR
    public virtual void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.yellow;

        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radiusAttack);
    }

#endif
}
