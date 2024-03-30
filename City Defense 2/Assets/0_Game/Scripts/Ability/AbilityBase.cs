using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBase : MonoBehaviour
{
    protected IContactObject iContactObject;

    protected Dictionary<string, float> dictionaryAbility;
    
    public virtual void Init(IContactObject _iContactObject)
    {
        dictionaryAbility = new Dictionary<string, float>();
        
        iContactObject = _iContactObject;
    }

    public virtual (bool,float) GetIndexAbility(string typeAbility)
    {
        if (dictionaryAbility.ContainsKey(typeAbility))
        {
            return (true, dictionaryAbility[typeAbility]);
        }
        else
        {
            return (false, 0);
        }
    }

    public virtual void OnGetHit(TypeWeapon typeWeapon, float damage)
    {
        switch (typeWeapon)
        {
            case TypeWeapon.OneShot:

                Instantiate(ResourceManager.Instance.Load("Effect/OneShot"), iContactObject.GetBody());

                iContactObject.GetHealth().SubAllHealth(typeWeapon, "");

                return;

            case TypeWeapon.MeleeCrit:

                Instantiate(ResourceManager.Instance.Load("Effect/Crit"), iContactObject.GetBody());

                break;
            case TypeWeapon.RangeCrit:

                Instantiate(ResourceManager.Instance.Load("Effect/Crit"), iContactObject.GetBody());

                break;
            case TypeWeapon.NuclearBoom:
                iContactObject.GetHealth().SubAllHealth(typeWeapon, "");

                return;
        }

        iContactObject.GetHealth().SubHealth(typeWeapon, damage, "");
    }

    public virtual void OnGetEffect(TypeEffectAttack typeEffectAttack)
    {
        if(iContactObject.GetHealth().GetHealth() <= 0)
        {
            return;
        }

        switch (typeEffectAttack)
        {
            case TypeEffectAttack.SniperStun:
                iContactObject.OnStun(GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Sniper_Ally_B_Time_Stun));
                break;
            case TypeEffectAttack.Slow:

                bool a = TryGetComponent<SlowBase>(out SlowBase slowBase);

                if (a)
                {
                    slowBase.ResetEffect();
                }
                else
                {
                    SlowBase slow = gameObject.AddComponent<SlowBase>();

                    Movement movement = iContactObject.GetObject<Movement>();

                    slow.Init(GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Ally_A_Index_Reduce_Speed),
                        movement.GetSpeed(),
                        GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Ally_A_Time_Reduce_Speed),
                        movement, iContactObject);
                }

                break;
            case TypeEffectAttack.DOT:
                break;
            case TypeEffectAttack.SlowOppressor:

                bool c = TryGetComponent<SlowOppressorBase>(out SlowOppressorBase slowOppressorBase);

                if (c)
                {
                    slowOppressorBase.ResetEffect();
                }
                else
                {
                    SlowOppressorBase slow = gameObject.AddComponent<SlowOppressorBase>();

                    Movement movement = iContactObject.GetObject<Movement>();

                    slow.Init(movement.GetSpeed(), GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Oppressor_Ally_C_Time_Reduce_Speed), movement, iContactObject);
                }

                break;
            case TypeEffectAttack.SpeedUp:

                bool e = TryGetComponent<SpeedUpVanguardEnermyBase>(out SpeedUpVanguardEnermyBase speedUpVanguardEnermyBase);

                if (e)
                {
                    speedUpVanguardEnermyBase.ResetEffect();
                }
                else
                {
                    SpeedUpVanguardEnermyBase speedUp = gameObject.AddComponent<SpeedUpVanguardEnermyBase>();

                    Movement movement = iContactObject.GetObject<Movement>();

                    speedUp.Init(GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Vanguard_Enemy_C_Index_Inscrease_Speed), 
                        movement.GetSpeed(),
                        GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Vanguard_Enemy_C_Time_Inscrease_Speed), 
                        movement);
                }

                break;
        }
    }
    
    public virtual void OnGetEffect(TypeEffectAttack typeEffectAttack, float damage)
    {
        if (iContactObject.GetHealth().GetHealth() <= 0)
        {
            return;
        }

        switch (typeEffectAttack)
        {
            case TypeEffectAttack.SniperStun:
                iContactObject.OnStun(Help.TIME_SNIPER_STUN);
                break;
            case TypeEffectAttack.Slow:

                bool a = TryGetComponent<SlowBase>(out SlowBase slowBase);

                if (a)
                {
                    slowBase.ResetEffect();
                }
                else
                {
                    SlowBase slow = gameObject.AddComponent<SlowBase>();

                    Movement movement = iContactObject.GetObject<Movement>();

                    slow.Init(GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Ally_A_Index_Reduce_Speed), 
                        movement.GetSpeed(),
                        GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Ally_A_Time_Reduce_Speed), 
                        movement, iContactObject);
                }

                break;
            case TypeEffectAttack.DOT:

                bool b = TryGetComponent<DOTBase>(out DOTBase dOTBase);

                if (b)
                {
                    dOTBase.ResetEffect();
                }
                else
                {
                    DOTBase dOT = gameObject.AddComponent<DOTBase>();

                    dOT.Init(damage, GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Ally_B_Time_DOT), iContactObject);
                }

                break;
            case TypeEffectAttack.DOTSniperEnermy:

                bool e = TryGetComponent<DOTSniperEnermyBase>(out DOTSniperEnermyBase dOTSniperEnermyBase);

                if (e)
                {
                    dOTSniperEnermyBase.ResetEffect();
                }
                else
                {
                    DOTSniperEnermyBase dOTSniperEnermy = gameObject.AddComponent<DOTSniperEnermyBase>();

                    dOTSniperEnermy.Init(damage, GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Sniper_Enemy_B_DOT_Time), iContactObject);
                }


                break;
            case TypeEffectAttack.SlowOppressor:

                bool c = TryGetComponent<SlowOppressorBase>(out SlowOppressorBase slowOppressorBase);

                if (c)
                {
                    slowOppressorBase.ResetEffect();
                }
                else
                {
                    SlowOppressorBase slow = gameObject.AddComponent<SlowOppressorBase>();

                    Movement movement = iContactObject.GetObject<Movement>();

                    slow.Init(movement.GetSpeed(), GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Oppressor_Ally_C_Time_Reduce_Speed), movement, iContactObject);
                }

                break;
            case TypeEffectAttack.SpeedUp:

                bool d = TryGetComponent<SpeedUpVanguardEnermyBase>(out SpeedUpVanguardEnermyBase speedUpVanguardEnermyBase);

                if (d)
                {
                    speedUpVanguardEnermyBase.ResetEffect();
                }
                else
                {
                    SpeedUpVanguardEnermyBase speedUp = gameObject.AddComponent<SpeedUpVanguardEnermyBase>();

                    Movement movement = iContactObject.GetObject<Movement>();

                    speedUp.Init(1.3f, movement.GetSpeed(), 0.5f, movement);
                }

                break;
            case TypeEffectAttack.DOTGunnerEnermy:

                bool f = TryGetComponent<DOTGunnerEnermyBase>(out DOTGunnerEnermyBase dOTGunnerEnermyBase);

                if (f)
                {
                    dOTGunnerEnermyBase.ResetEffect();
                }
                else
                {
                    DOTGunnerEnermyBase dOTGunnerEnermy = gameObject.AddComponent<DOTGunnerEnermyBase>();

                    dOTGunnerEnermy.Init(damage, GameManager.Instance.DataManager.DataManagerMainGame.DataSpecialIndex.GetSpecialIndex(TypeSpecialIndex.Gunner_Enemy_B_DOT_Time), iContactObject);
                }

                break;
        }
    }
}