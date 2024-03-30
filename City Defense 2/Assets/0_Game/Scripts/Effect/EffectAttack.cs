using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAttack : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;

    [SerializeField] private float timeToDeActive;

    public void Play()
    {
        particleSystem.Play();

        Invoke("DeActive", timeToDeActive);
    }

    private void DeActive()
    {
        particleSystem.Stop();

        Pooling.Instance.PoolEffect.DeSqawnAttackEffect(this);
    }
}
