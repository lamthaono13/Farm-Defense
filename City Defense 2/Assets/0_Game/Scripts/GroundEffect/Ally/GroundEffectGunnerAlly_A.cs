using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEffectGunnerAlly_A : GroundEffectBase
{
    public override void StartEffect(float _damage)
    {
        base.StartEffect(_damage);
    }

    // Update is called once per frame
    void Update()
    {
        if (canEffect)
        {
            if(currentTimeUpdateEffect <= timeUpdateEffect)
            {
                currentTimeUpdateEffect += Time.deltaTime;
            }
            else
            {
                var enermies = CharManager.Instance.Enermies;

                for(int i = 0; i < enermies.Count; i++)
                {
                    float distance = Vector3.Distance(enermies[i].GetBody().position, transform.position);

                    if(distance <= radiusEffect)
                    {
                        enermies[i].GetAbility().OnGetEffect(TypeEffectAttack.Slow);
                    }
                }

                currentTimeUpdateEffect = 0;
            }
        }
    }

#if UNITY_EDITOR
    public virtual void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.yellow;

        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radiusEffect);
    }

#endif
}
