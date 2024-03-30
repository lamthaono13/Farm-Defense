using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEffectBase : MonoBehaviour
{
    [SerializeField] protected float timeUpdateEffect;

    [SerializeField] protected float timeToDestroy;

    [SerializeField] protected float radiusEffect;

    protected float currentTimeUpdateEffect;

    protected bool canEffect;

    protected float damage;

    public virtual void StartEffect(float _damage)
    {
        canEffect = true;

        damage = _damage;

        currentTimeUpdateEffect = timeUpdateEffect;

        StartCoroutine(WaitDestroy());
    }

    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(timeToDestroy);

        Destroy(gameObject);
    }
}
