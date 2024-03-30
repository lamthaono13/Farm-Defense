using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBlock : MonoBehaviour
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

        Pooling.Instance.PoolEffect.DeSqawnBlockEffect(this);
    }
}
