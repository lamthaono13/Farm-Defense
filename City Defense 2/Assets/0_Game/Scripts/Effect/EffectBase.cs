using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
    [SerializeField] private bool isLooping;

    [SerializeField] private float timeAlive;

    [SerializeField] private ParticleSystem particle;

    [SerializeField] private bool isDead;

    [SerializeField] private float radiusEffect;

    private void Awake()
    {
        if (isDead)
        {
            StartCoroutine(WaitDestroy());
        }

        PlayParticle(true);
    }

    public virtual void PlayParticle(bool isDestroy)
    {
        if(particle == null)
        {
            Debug.LogError("Null Particle When ");
        }
        else
        {
            particle.Play();
        }

        if (isDestroy)
        {
            StartCoroutine(WaitDestroy());
        }
    }

    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(timeAlive);

        DestroyEffect();
    }

    IEnumerator WaitDoneParticle(Action actionOnDone, bool useTimeAlive, float timeDelay)
    {
        if (useTimeAlive)
        {
            yield return new WaitForSeconds(timeAlive + timeDelay);
        }
        else
        {
            yield return new WaitForSeconds(timeDelay);
        }

        actionOnDone?.Invoke();
    }

    public virtual void DestroyEffect()
    {
        Destroy(gameObject);
    }

#if UNITY_EDITOR
    public virtual void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.yellow;

        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radiusEffect);
    }

#endif
}
