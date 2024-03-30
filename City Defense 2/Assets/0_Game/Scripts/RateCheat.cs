using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using Google.Play.Review;
using System;

public class RateCheat : MonoBehaviour
{
    public GameObject TextInput;
    public GameObject CheatPanel;
    public GameObject[] StarOn;

    public delegate void event1(GameResult gameResult);

    private event1 action;

    private GameResult gameResult;

    // Start is called before the first frame update
    public void OnClickBelow5(int starcount)
    {
        TextInput.SetActive(true);

        for (int i = 0; i < starcount; i++)
        {
            StarOn[i].SetActive(true);
        }
    }


    public void TurnOff()
    {
        action?.Invoke(gameResult);

        GameManager.Instance.DataManager.SetRate();

        CheatPanel.SetActive(false);

        //AdsController.Instance.ShowIntertital();

    }
    public void Onclick5()
    {
        Debug.Log("FirebaseRemoteData.LEVEL_SHOW_RATE:" + HandleFireBase.LEVEL_SHOW_RATE);
        RateAndReview();
        TurnOff();

    }

    public void OnShow(event1 _action, GameResult _gameResult)
    {
        action = _action;

        gameResult = _gameResult;

        gameObject.SetActive(true);
    }





    private ReviewManager _reviewManager;
    private PlayReviewInfo _playReviewInfo;
    private Coroutine _coroutine;

    private void Start()
    {

#if UNITY_ANDROID

        _coroutine = StartCoroutine(InitReview());
#endif
    }
    private IEnumerator InitReview(bool force = false)
    {
        if (_reviewManager == null) _reviewManager = new ReviewManager();

        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            if (force) DirectlyOpen();
            yield break;
        }

        _playReviewInfo = requestFlowOperation.GetResult();
    }
    public void RateAndReview()
    {
        if (PlayerPrefs.GetInt("Rate", 0) != 0) return;
        PlayerPrefs.SetInt("Rate", 1);
#if UNITY_IOS
        Device.RequestStoreReview();
#elif UNITY_ANDROID
        StartCoroutine(LaunchReview());
#endif
    }

    public IEnumerator LaunchReview()
    {
        if (_playReviewInfo == null)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
            yield return StartCoroutine(InitReview(true));
        }

        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null;
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            DirectlyOpen();
            yield break;
        }
    }

    private void DirectlyOpen() { Application.OpenURL($"https://play.google.com/store/apps/details?id={Application.identifier}"); }
}
#endif