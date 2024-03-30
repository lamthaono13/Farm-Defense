using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppressorEnermyAbility : AbilityBase
{
    protected bool hasSheild;

    protected int numberSheildBlockGunner;

    [SerializeField] private ParticleSystem particleSystem;

    private Coroutine coroutineWaitParticle;

    private bool isPlayingParticle;

    private void OnDisable()
    {
        if(coroutineWaitParticle != null)
        {
            StopCoroutine(coroutineWaitParticle);
        }
    }

    public override void Init(IContactObject _iContactObject)
    {
        base.Init(_iContactObject);

        hasSheild = true;

        numberSheildBlockGunner = (int)GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Oppressor_Enemy_C_Number_To_Break_Sheild);

        iContactObject.GetHealth().EventChangeHealth += (a, b, c) => { if ((a - b) <= 0) { particleSystem.gameObject.SetActive(false); } };
    }

    public override void OnGetHit(TypeWeapon typeWeapon, float damage)
    {
        if(typeWeapon == TypeWeapon.NuclearBoom)
        {
            iContactObject.GetHealth().SubAllHealth(typeWeapon, "");

            return;
        }

        if (!hasSheild)
        {
            base.OnGetHit(typeWeapon, damage);
        }
        else
        {
            if (!isPlayingParticle)
            {
                StartCoroutine(WaitParicle());
            }

            if (typeWeapon == TypeWeapon.AOE)
            {
                numberSheildBlockGunner--;
            }

            if(numberSheildBlockGunner <= 0)
            {
                particleSystem.gameObject.SetActive(false);
                hasSheild = false;
            }

            iContactObject.GetHealth().SubHealth(typeWeapon, damage * GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Oppressor_Enemy_C_Index_Damage_Earn_Has_Sheild), "");
        }
    }

    private IEnumerator WaitParicle()
    {
        particleSystem.Play();
        isPlayingParticle = true;
        yield return new WaitForSeconds(0.5f);
        isPlayingParticle = false;
    }
}
