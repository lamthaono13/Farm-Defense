using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EffectEnergy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteEnergy;

    public void Play()
    {
        spriteEnergy.transform.localPosition = Vector3.zero;

        spriteEnergy.DOFade(1, 0).OnComplete(() => { spriteEnergy.DOFade(0, 0.25f).SetDelay(0.25f); });

        spriteEnergy.transform.DOLocalMove(new Vector3(0, 0.6f, 0), 0.5f).OnComplete(() => { Pooling.Instance.PoolEffect.DeSqawnEnergyEffect(this); });
    }
}
