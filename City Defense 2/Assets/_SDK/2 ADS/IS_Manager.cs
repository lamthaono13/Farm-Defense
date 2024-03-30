using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IS_Manager : AdsManager
{
    [SerializeField] string appKey = "";
    public override void Init()
    {
        base.Init();
        IronSourceEvents.onImpressionDataReadyEvent += (infor) => { OnAdRevenuePaidEvent(new RevalueInfor("ironSource", infor.adNetwork, infor.adUnit, infor.instanceName, (double)infor.revenue, "USD")); };
        IronSourceEvents.onSdkInitializationCompletedEvent += () => { OnSdkInitializedEvent(); };
        IronSource.Agent.validateIntegration();
        IronSource.Agent.init(appKey);
        GoogleADManager.Initialize(aoaID, nativeID, imageInterID, InitAOACallback, InitNativeADCallback);
    }
    #region Banner
    public override void InitBannerCallback()
    {
        IronSourceBannerEvents.onAdLoadedEvent += (inf) => { OnBannerLoadedEvent(); };
        IronSourceBannerEvents.onAdLoadFailedEvent += (err) => { OnBannerLoadFailedEvent((int)err.getCode()); };
        IronSourceBannerEvents.onAdClickedEvent += (infor) => { };
        //LoadBanner();
    }
    public override void LoadBanner()
    {
        if (IsRemoveAds) return;
        if (!FirebaseRemoteData.BANNER_AD_ON_OFF) return;
        IronSourceBannerSize size = IronSourceBannerSize.BANNER;
        size.SetAdaptive(!IsPortrait);
        IronSource.Agent.loadBanner(size, IronSourceBannerPosition.BOTTOM);
    }
    public override void ShowBanner()
    {
        if (IsRemoveAds) return;
        if (!FirebaseRemoteData.BANNER_AD_ON_OFF) return;
        Debug.Log("ShowBanner");
        IronSource.Agent.displayBanner();
    }
    public override void HideBanner()
    {
        IronSource.Agent.hideBanner();
    }
    #endregion
    #region Interstitial
    public override void InitInterstitialCallback()
    {

        IronSourceInterstitialEvents.onAdReadyEvent += (infor) => { OnInterstitialLoadedEvent(); };
        IronSourceInterstitialEvents.onAdLoadFailedEvent += (err) => { OnInterstitialLoadFailedEvent((int)err.getCode()); };
        IronSourceInterstitialEvents.onAdShowSucceededEvent += (infor) => { OnInterstitialDisplayedEvent(); };
        IronSourceInterstitialEvents.onAdShowFailedEvent += (err, infor) => { OnInterstitialDisplayFailedEvent((int)err.getCode()); };
        IronSourceInterstitialEvents.onAdClickedEvent += (infor) => { OnInterstitialClickedEvent(); };
        IronSourceInterstitialEvents.onAdClosedEvent += (infor) => { OnInterstitialHiddenEvent(); };

        Invoke("LoadInterstitial", 0.5f);
    }
    public override void LoadInterstitial()
    {
        if (IsRemoveAds) return;
        IronSource.Agent.loadInterstitial();
    }
    public override void ShowInterstitial(string placement, bool ignoreCapping = false)
    {
        this.placementInter = placement;
        if (IsRemoveAds) { return; }
        if (!FirebaseRemoteData.INTER_AD_ON_OFF) { return; }
        HandleAppsflyer.Instance.LogEventWithName(HandleAppsflyer.AF_INTERS_CALL_SHOW);
        DateTime now = DateTime.Now;
        if (!ignoreCapping && (now - timeCloseIntersAds).TotalSeconds < FirebaseRemoteData.INTER_AD_CAPPING_TIME) { return; }
        HandleAppsflyer.Instance.LogEventWithName(HandleAppsflyer.AF_INTERS_PASSED_CAPPING_TIME);
        if (!IronSource.Agent.isInterstitialReady()) { return; }
        HandleAppsflyer.Instance.LogEventWithName(HandleAppsflyer.AF_INTERS_AVAILABLE);
        interShowing = true;
        IronSource.Agent.showInterstitial();
    }

    #endregion
    #region Rewarded
    public override void InitRewardedCallback()
    {
        IronSourceRewardedVideoEvents.onAdAvailableEvent += (infor) => { OnRewardedLoadedEvent(); };
        IronSourceRewardedVideoEvents.onAdLoadFailedEvent += (err) => { OnRewardedLoadFailedEvent((int)err.getCode()); };
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += (err, infor) => { OnRewardedDisplayFailedEvent((int)err.getCode()); };
        IronSourceRewardedVideoEvents.onAdOpenedEvent += (infor) => { OnRewardedDisplayedEvent(); };
        IronSourceRewardedVideoEvents.onAdClickedEvent += (placement, infor) => { OnRewardedClickedEvent(); };
        IronSourceRewardedVideoEvents.onAdClosedEvent += (infor) => { OnRewardedHiddenEvent(); };
        IronSourceRewardedVideoEvents.onAdRewardedEvent += (reward, adInfo) => { OnAdReceivedRewardEvent(); };
    }
    public override void ShowRewarded(Action callback = null, string placement = "", Action onNoADS = null)
    {
        this.placementReward = placement;
        this.callbackReward = callback;
        if (isTestReward)
        {
            callbackReward.Invoke();
            return;
        }
        HandleAppsflyer.Instance.LogEventWithName(HandleAppsflyer.AF_REWARDED_CALL_SHOW);
        if (!IronSource.Agent.isRewardedVideoAvailable())
        {
            onNoADS?.Invoke();
            return;
        }
        HandleAppsflyer.Instance.LogEventWithName(HandleAppsflyer.AF_REWARDED_AVAILABLE);
        rewardShowing = true;
        IronSource.Agent.showRewardedVideo();
    }


    #endregion
    #region AOA
    public override void InitAOACallback()
    {

        GA_AOA.OnAdDisplayFailedEvent = (err) =>
        {
            SetAoaShowSuccess(true);

            OnAOAFailedEvent(err);
        };
        GA_AOA.OnAdHiddenEvent = () =>
        {
            SetAoaShowSuccess(true);
            OnAOAHiddenEvent();
        };
        GA_AOA.OnAdLoadedEvent = OnAOALoadedEvent;
        GA_AOA.OnAdLoadFailedEvent = (err) => { OnAOAFailedEvent(err); };
        GA_AOA.OnAdRevenuePaidEvent = (value, CurrencyCode) => { OnAdRevenuePaidEvent(new RevalueInfor("Admob", "Admob", aoaID, "AOA", value, CurrencyCode)); };
        LoadAOA();
    }
    public override void LoadAOA()
    {
        if (IsRemoveAds) return;
        GA_AOA.LoadAd();
    }
    public override void ShowFirstAOA()
    {
        if (AoaShowSuccess) return;
        if (isRemoveAds)
        {
            SetAoaShowSuccess(true);
            return;
        }
        if (!FirebaseRemoteData.OPEN_AD_ON_OFF) { SetAoaShowSuccess(true); return; }
        if (aoaShowing)
        {
            return;
        }
        if (!GA_AOA.IsAppOpenAdAvailable)
        {
            SetAoaShowSuccess(true);
            return;
        }
        aoaShowing = true;
        GA_AOA.ShowAd();
    }
    public override void ShowAOA(bool irgoneCampingTime = false)
    {
        Debug.Log("AOA 1 ");
        if (isRemoveAds) return;
        Debug.Log("AOA 2");
        if (interShowing) return;

        Debug.Log("AOA 3");

        if (rewardShowing) return;

        Debug.Log("AOA 4");

        if (!FirebaseRemoteData.OPEN_AD_ON_OFF) return;
        Debug.Log("AOA 5");

        if (aoaShowing) return;

        Debug.Log("AOA 6");

        if (!irgoneCampingTime && (DateTime.Now - timeOutGame).TotalSeconds < FirebaseRemoteData.OPEN_AD_CAPPING_TIME) return;
        Debug.Log("AOA 7");

        //if (OpenGameCount == 0 && onPauseCount == 0) return;
        //Debug.Log("AOA 8");

        if (!GA_AOA.IsAppOpenAdAvailable) return;
        Debug.Log("AOA 9");
        aoaShowing = true;
        GA_AOA.ShowAd();
    }
    private void OnApplicationPause(bool pause)
    {
        IronSource.Agent.onApplicationPause(pause);

        if (pause)
        {
            onPauseCount++;
            timeOutGame = DateTime.Now;
        }
        else
        {

            ShowAOA();
        }
    }
    #endregion
    #region MREC
    public override void InitMRECCallback()
    {
    }


    public override void HideMREC()
    {
    }

    public override bool IsNativeADReady()
    {
        return GA_Native.NativeAd != null;
    }

    public override bool IsRewardedAdReady()
    {
        return IronSource.Agent.isRewardedVideoAvailable();
    }






    public override void LoadMREC()
    {
    }

    public override void LoadRewarded()
    {
    }

    public override void ShowMREC()
    {
    }



    public override void UpdateMRecPosition(EMRecPosition mrecPosition)
    {
    }
    public override void ShowNativeAD(INativeADViewCallback nativeADViewCallback)
    {
        if (IsRemoveAds) return;
        if (nativeID == "") return;
        if (!FirebaseRemoteData.NATIVE_AD_ON_OFF) return;
        GA_Native.ShowNativeAD(nativeADViewCallback);
    }

    public override void LoadNativeAD()
    {
        if (nativeID == "") return;
        GA_Native.LoadNativeAD();
    }
    public override void HideAllNativeAD()
    {
        NativeAdsMessenger.DestroyView();
    }
    #endregion
    #region Image Inter
    public override void InitNativeADCallback()
    {
        GA_Native.OnNativeAdFailedToLoadEvent = OnNativeAdFailedToLoadEvent;
        GA_Native.OnNativeAdLoadedEvent = OnNativeAdLoadedEvent;
        GA_Native.OnAdRevenuePaidEvent = (value, CurrencyCode) => { OnAdRevenuePaidEvent(new RevalueInfor("Admob", "Admob", nativeID, "Native", value, CurrencyCode)); };

        LoadNativeAD();
    }


    public override void ShowImageInter(Action callback)
    {
        if (IsRemoveAds)
        {
            callback?.Invoke();
            return;
        }
        GA_Inter.ShowAd(callback);
    }
    #endregion
}
