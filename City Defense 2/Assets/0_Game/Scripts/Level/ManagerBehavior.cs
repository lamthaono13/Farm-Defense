using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagerBehavior : MonoBehaviour, IManager
{
    public virtual void Start()
    {

    }

    public abstract void OnEndGame(GameResult gameResult);

    public abstract void OnPause();

    public abstract void OnRemuse();

    public abstract void OnRevive();

    public abstract void OnStartLevel();
}