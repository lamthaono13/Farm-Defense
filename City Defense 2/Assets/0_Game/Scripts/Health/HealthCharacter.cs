using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCharacter : HealthBase
{
    [SerializeField] protected CharacterBase character;

    [SerializeField] private bool cantDie;

    public override void SubHealth(TypeWeapon typeWeapon, float number, string mess)
    {
        base.SubHealth(typeWeapon, number, mess);

        Pooling.Instance.PoolEffect.SqawnAttackEffect(character.GetBody().position);

        if (!cantDie)
        {
            float a = currentHealth - number;

            if (a <= 0)
            {
                currentHealth = 0;

                character.OnGetHit(number, true);

                GameManager.Instance.SoundManager.PlaySoundDie();
            }
            else
            {
                currentHealth -= number;

                character.OnGetHit(number, false);
            }
        }
    }
}
