using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerBullet_A : GunnerBullet
{
    [SerializeField] protected GameObject objGroundEffect;

    protected override void OnBreak()
    {
        GameObject objInstan = Instantiate(objGroundEffect, transform.position, Quaternion.identity);

        GroundEffectBase groundEffectBase = objInstan.GetComponent<GroundEffectBase>();

        groundEffectBase.StartEffect(damage);

        base.OnBreak();
    }

    public override void OnShoot(float _damage, Vector3 _postionTarget, Vector3 _direction)
    {
        base.OnShoot(_damage, _postionTarget, _direction);

        radiusBreak = GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Ally_A_Range_AOE);
    }
}
