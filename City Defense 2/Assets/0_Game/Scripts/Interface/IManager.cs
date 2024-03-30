using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManager
{
    public void OnPause();

    public void OnRemuse();

    public void OnStartLevel();

    public void OnEndGame(GameResult gameResult);

    public void OnRevive();
}
