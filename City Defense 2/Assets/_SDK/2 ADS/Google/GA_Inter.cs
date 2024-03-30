using GoogleMobileAds.Api;
using System;

using UnityEngine;

public class GA_Inter
{
    static InterstitialAd _interstitialAd;
    static Action callbackInter;

    static string _adUnitId = "";

    public static void Initialize(string id)
    {
        _adUnitId = id;
        LoadAd();
    }

    public static void LoadAd()
    {
        if (_interstitialAd != null)
        {
            DestroyAd();
        }
        var adRequest = new AdRequest();

        InterstitialAd.Load(_adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {

            if (error != null)
            {
                return;
            }
            if (ad == null)
            {
                return;
            }
            _interstitialAd = ad;
            RegisterEventHandlers(ad);

        });
    }


    public static void ShowAd(Action callbackInter)
    {
        GA_Inter.callbackInter = callbackInter;
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            _interstitialAd.Show();
        }
        else
        {
            callbackInter?.Invoke();
        }

    }

    /// <summary>
    /// Destroys the ad.
    /// </summary>
    static void DestroyAd()
    {
        if (_interstitialAd != null)
        {
            Debug.Log("Destroying interstitial ad.");
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }
    }
    static void RegisterEventHandlers(InterstitialAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) => { AdsManager.Instance.OnAdRevenuePaidEvent(new RevalueInfor("Admob", "Admob", _adUnitId, "Inter", adValue.Value, adValue.CurrencyCode)); };
        ad.OnAdFullScreenContentClosed += () => { callbackInter?.Invoke(); };
        ad.OnAdFullScreenContentFailed += (err) => { callbackInter?.Invoke(); };

        //ad.OnAdFullScreenContentClosed += () =>        {
        //};       ad.OnAdFullScreenContentFailed += (AdError error) =>
        //{
        //};
    }
}
