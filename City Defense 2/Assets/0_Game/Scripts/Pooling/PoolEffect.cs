using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEffect : MonoBehaviour
{
    [SerializeField] private int numberEnergyEffect;

    [SerializeField] private int numberAttackEffect;

    [SerializeField] private int numberBlockEffect;

    [SerializeField] private GameObject objEnergyEffect;

    [SerializeField] private GameObject objAttackEffect;

    [SerializeField] private GameObject objBlockEffect;

    private List<EffectEnergy> effectEnergies;

    private List<EffectAttack> effectAttacks;

    private List<EffectBlock> effectBlocks;

    public void Init()
    {
        effectEnergies = new List<EffectEnergy>();

        for(int i = 0; i < numberEnergyEffect; i++)
        {
            GameObject obj = Instantiate(objEnergyEffect, transform);

            effectEnergies.Add(obj.GetComponent<EffectEnergy>());
        }

        effectAttacks = new List<EffectAttack>();

        for(int i = 0; i < numberAttackEffect; i++)
        {
            GameObject obj = Instantiate(objAttackEffect, transform);

            effectAttacks.Add(obj.GetComponent<EffectAttack>());
        }

        effectBlocks = new List<EffectBlock>();

        for(int i = 0; i < numberBlockEffect; i++)
        {
            GameObject obj = Instantiate(objBlockEffect, transform);

            effectBlocks.Add(obj.GetComponent<EffectBlock>());
        }
    }

    public void SqawnEnergyEffect(Vector3 positionSqawn)
    {
        if (effectEnergies.Count == 0)
        {
            return;
        }

        EffectEnergy effectEnergy = effectEnergies[0];

        effectEnergy.transform.position = positionSqawn;

        effectEnergy.gameObject.SetActive(true);

        effectEnergy.Play();

        effectEnergies.RemoveAt(0);
    }

    public void DeSqawnEnergyEffect(EffectEnergy _effectEnergy)
    {
        _effectEnergy.gameObject.SetActive(false);

        effectEnergies.Add(_effectEnergy);
    }

    public void SqawnAttackEffect(Vector3 positionSqawn)
    {
        if(effectAttacks.Count == 0)
        {
            return;
        }

        EffectAttack effectAttack = effectAttacks[0];

        effectAttack.transform.position = positionSqawn;

        effectAttack.gameObject.SetActive(true);

        effectAttack.Play();

        effectAttacks.RemoveAt(0);
    }

    public void DeSqawnAttackEffect(EffectAttack _effectAttack)
    {
        _effectAttack.gameObject.SetActive(false);

        effectAttacks.Add(_effectAttack);
    }

    public void SqawnBlockEffect(Vector3 positionSqawn)
    {
        if(effectBlocks.Count == 0)
        {
            return;
        }

        EffectBlock effectBlock = effectBlocks[0];

        effectBlock.transform.position = positionSqawn;

        effectBlock.gameObject.SetActive(true);

        effectBlock.Play();

        effectBlocks.RemoveAt(0);
    }

    public void DeSqawnBlockEffect(EffectBlock _effectBlock)
    {
        _effectBlock.gameObject.SetActive(false);

        effectBlocks.Add(_effectBlock);
    }
}

public enum TypeVFX
{
    EnergyEffect,
    AttackEffect,
    BlockEffect
}