//using DG.Tweening;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class Loading : MonoBehaviour
//{
//    [SerializeField] Image fillbar;
//    [SerializeField] CanvasGroup canvasGroup;
//    IEnumerator IEStart()
//    {
//        //canvasGroup.alpha = 0;
//        //DOVirtual.Float(0, 1, 0.2f, (val) => { canvasGroup.alpha = val; });

//        Transform temp = fillbar.transform.parent;
//        temp.localScale = Vector3.zero;
//        temp.DOScale(1, 0.2f).SetEase(Ease.OutBack);
//        yield return new WaitForSeconds(0.25f);
//        fillbar.fillAmount = 0;
//        fillbar.DOFillAmount(0.1f, 0.5f);
//        yield return new WaitForSeconds(0.5f);
//#if GleyIAPGooglePlay
//        HandleIAP.Instance.Initialize();
//#endif
//        yield return new WaitForSeconds(0.5f);
//        AdsManager.Instance.Init();
//        float fillTime1 = 1;
//        float fillTime2 = 2;
//        if (AdsManager.Instance.OpenGameCount != 0)
//        {
//            fillTime1 = 2;
//            fillTime2 = 3;
//        }

//#if UNITY_EDITOR
//        fillTime1 = 1;
//        fillTime2 = 1;
//#endif
//        CalculateData();
//        fillbar.DOFillAmount(0.7f, fillTime1);
//        yield return new WaitForSeconds(fillTime1);
//        var home = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
//        fillbar.DOFillAmount(1f, fillTime2);
//        yield return new WaitForSeconds(fillTime2);
//        yield return new WaitForSeconds(0.5f);
//        while (!home.isDone) yield return new WaitForEndOfFrame();
//        yield return new WaitForSeconds(0.5f);
//        if (!AdsManager.Instance.IsRemoveAds)
//        {
//            if (PlayerPrefs.GetInt("IsRemoveAds") == 1)
//            {
//                AdsManager.Instance.SetIsRemoveAds(true);
//                AdsManager.Instance.HideBanner();
//            }
//            //else if (HandleIAP.Instance.HaveBought(ShopProductNames.RemoveADS))
//            //{
//            //    AdsManager.Instance.SetIsRemoveAds(true);
//            //    AdsManager.Instance.HideBanner();
//            //}
//        }
//        AdsManager.Instance.ShowAOA();
//        yield return new WaitForSeconds(0.2f);
//        TransitionUI.Instance.Show(() =>
//        {
//            SceneManager.UnloadSceneAsync(0);
//            SceneManager.UnloadSceneAsync(1);
//            TransitionUI.Instance.Hide(() => { });
//        });
//    }
//    private void Start()
//    {
//        StartCoroutine(IEStart());
//    }
//    void CalculateData() { }
//}
