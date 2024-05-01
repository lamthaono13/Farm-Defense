using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGamePlay : MonoBehaviour, ISingleton
{
    public static HealthGamePlay Instance;

    private int maxHealth;

    private int currentHealth;

    public delegate void EventSubHealthGamePlay(int current, int max);

    public EventSubHealthGamePlay OnSubHealthGamePlay;

    [SerializeField] private UiHealthGame uiHealthGame;

    public void InitSingleton()
    {
        Instance = this;
    }

    public void Init(int _numberHealth)
    {
        maxHealth = _numberHealth;

        currentHealth = maxHealth;

        uiHealthGame.Init(maxHealth);
    }

    public void SubHealth()
    {
        if (GameManager.Instance.IsGameDesign)
        {
            return;
        }

        currentHealth--;

        if(currentHealth <= 0)
        {
            // Lose

            currentHealth = 0;

            LevelManagerMainGame.Instance.OnEndGame(GameResult.Lose);
        }

        uiHealthGame.SubHealth(currentHealth);

        OnSubHealthGamePlay?.Invoke(currentHealth, maxHealth);
    }

    public void SubAllHealth()
    {
        currentHealth = 0;

        if (currentHealth <= 0)
        {
            // Lose

            currentHealth = 0;

            LevelManagerMainGame.Instance.OnEndGame(GameResult.Lose);
        }

        uiHealthGame.SubHealth(currentHealth);

        OnSubHealthGamePlay?.Invoke(currentHealth, maxHealth);
    }

    public float GetPercentHealth()
    {
        float a = (float)currentHealth / (float)maxHealth;

        return a;
    }

    public void OnRevive()
    {
        currentHealth += maxHealth / 2;

        uiHealthGame.SetHealth(currentHealth);

        OnSubHealthGamePlay?.Invoke(currentHealth, maxHealth);
    }
}
