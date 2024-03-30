using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour, ISingleton
{
    public static Pooling Instance;
    
    [SerializeField] private PoolAlly poolAlly;

    [SerializeField] private PoolEnermy poolEnermy;

    [SerializeField] private PoolEffect poolEffect;

    [SerializeField] private PoolBullet poolBullet;

    public PoolAlly PoolAlly => poolAlly;

    public PoolEnermy PoolEnermy => poolEnermy;

    public PoolEffect PoolEffect => poolEffect;

    public PoolBullet PoolBullet => poolBullet;

    public void InitSingleton()
    {
        Instance = this;
        
        //Init();
    }
    
    public void Init()
    {
        poolEnermy.Init();

        poolBullet.Init();

        StartCoroutine(WaitInit());
    }

    IEnumerator WaitInit()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        poolAlly.Init();
        yield return new WaitForSecondsRealtime(0.5f);
        poolEffect.Init();
    }
}