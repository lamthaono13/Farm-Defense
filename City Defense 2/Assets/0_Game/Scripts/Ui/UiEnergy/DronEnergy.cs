using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using AssetKits.ParticleImage;

public class DronEnergy : MonoBehaviour
{
    [SerializeField] private float positionMove;

    [SerializeField] private float time;

    [SerializeField] private GameObject objFree;

    [SerializeField] private GameObject objReward;

    [SerializeField] private ParticleImage particleImage;

    private float initialPosition;

    private Tween tween;

    private bool isMoving;

    private bool isTutorial;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 u = Camera.main.ScreenToWorldPoint(Input.mousePosition) / 5 * LevelManagerMainGame.Instance.BaseCamera;

            RaycastHit2D raycastHit = Physics2D.Raycast(u, Input.mousePosition);

            if (raycastHit)
            {
                if (raycastHit.collider.tag == "Dron")
                {
                    OnClickDron();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartMove();
        }
    }

    public void StartMove()
    {
        if (isMoving)
        {
            return;
        }

        isMoving = true;

        bool isFirstDron = GameManager.Instance.DataManager.GetFirstDron();

        if (isFirstDron)
        {
            objFree.gameObject.SetActive(true);

            objReward.gameObject.SetActive(false);

            GameManager.Instance.DataManager.SetFirstDron();

            isTutorial = true;
        }
        else
        {
            isTutorial = false;

            objFree.gameObject.SetActive(false);

            objReward.gameObject.SetActive(true);
        }

        tween = transform.DOLocalMoveX(positionMove, time).SetEase(DG.Tweening.Ease.Linear).OnComplete(() => { tween = null; ResetToInitialPosition(); isMoving = false; });
    }

    public void StopMove()
    {
        if(tween != null)
        {
            tween.Kill();
        }

        ResetToInitialPosition();
    }

    private void ResetToInitialPosition()
    {
        transform.localPosition = new Vector3(initialPosition, transform.localPosition.y, transform.localPosition.z);
    }

    private void OnClickDron()
    {
        Vector3 positionClick = transform.position;

        HandleFireBase.Instance.LogEventWithParameter("Click_Dorn", new FirebaseParam[] { new FirebaseParam("Level", GameManager.Instance.DataManager.GetLevel())});

        if (isTutorial)
        {
            StopMove();
            ResetToInitialPosition();

            EnergyManager.Instance.OnClickDron();

            //LevelManagerMainGame.Instance.EnergyManager.AddEnergy(10000000);

            GameManager.Instance.SoundManager.PlaySoundButton();

            particleImage.gameObject.transform.position = positionClick;

            particleImage.gameObject.SetActive(true);

            particleImage.Play();
        }
        else
        {
            AdsManager.Instance.ShowRewarded(() =>
            {
                StopMove();
                ResetToInitialPosition();

                EnergyManager.Instance.OnClickDron();

                 //LevelManagerMainGame.Instance.EnergyManager.AddEnergy(10000000);

                 GameManager.Instance.SoundManager.PlaySoundButton();

                particleImage.gameObject.transform.position = positionClick;

                particleImage.gameObject.SetActive(true);

                particleImage.Play();

                //GameManager.Instance.DataManager.SetElementDailyQuest(TypeQuest.WatchingRewardAds);

                 //HandleFireBase.Instance.LogEventReward("Dron_Ingame");
             }, "Dron_Ingame");
        }
    }
}