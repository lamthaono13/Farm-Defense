using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private UICloud uiLoadingHide;

    [SerializeField] private UiLoadingInitialGame uiLoadingInitialGame;

    //[SerializeField] private Text textFPS;

    //private void Update()
    //{
    //    int fps = (int)(1f / Time.unscaledDeltaTime);

    //    textFPS.text = fps.ToString();
    //}


    public void OnLoading(TypeLoading typeLoading, Action actionOnLoadingDone)
    {
        //actionOnLoadingDone?.Invoke();
        
        //return;
        
        switch (typeLoading)
        {
            case TypeLoading.NotDeciced:
                break;
            case TypeLoading.LoadingInitialGame:
                uiLoadingInitialGame.OnLoading(() => {  uiLoadingHide.OnOpen(() => { HideInititalUi(); actionOnLoadingDone?.Invoke(); }, "LoadingInitialGame"); });
                break;
            case TypeLoading.LoadingToInGame:
                uiLoadingHide.OnOpen(() => { HideInititalUi(); actionOnLoadingDone?.Invoke(); }, "LoadingToInGame");
                break;
            case TypeLoading.LoadingToLobby:
                uiLoadingHide.OnOpen(() => { HideInititalUi(); actionOnLoadingDone?.Invoke(); }, "LoadingToLobby");
                //actionOnLoadingDone?.Invoke();
                break;
            default:
                return;
        }
    }

    private void HideInititalUi()
    {
        uiLoadingInitialGame.Show(false);
    }

    public void CloseUiHide()
    {
        uiLoadingHide.OnClose(null, "CloseWhenLoadSceneDone");
    }
}

public enum TypeLoading
{
    NotDeciced,
    LoadingInitialGame,
    LoadingToInGame,
    LoadingToLobby
}
