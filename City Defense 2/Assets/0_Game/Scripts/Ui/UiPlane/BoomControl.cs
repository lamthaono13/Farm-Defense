using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoomControl : MonoBehaviour
{
    [SerializeField] private Button btnBoom;

    [SerializeField] private TextMeshProUGUI textBoom;

    [SerializeField] private Image imgAdsBoom;

    [SerializeField] private AnimBoom animBoom;

    [SerializeField] private UiPlane uiPlane;

    // Start is called before the first frame update
    void Start()
    {
        if ((!GameManager.Instance.DataManager.HasTutorialBoom()) && GameManager.Instance.DataManager.GetLevel() == 7)
        {
            if (GameManager.Instance.DataManager.GetBoom() <= 0)
            {
                GameManager.Instance.DataManager.AddBoom(1);
            }
        }

        int a = GameManager.Instance.DataManager.GetBoom();

        if(a > 0)
        {
            textBoom.text = a.ToString();

            textBoom.gameObject.SetActive(true);

            imgAdsBoom.gameObject.SetActive(false);
        }
        else
        {
            textBoom.gameObject.SetActive(false);

            imgAdsBoom.gameObject.SetActive(true);
        }

        btnBoom.onClick.AddListener(OnClickBtnBoom);

        if (GameManager.Instance.IsGameDesign)
        {
            btnBoom.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClickBtnBoom()
    {
        LevelManagerMainGame.Instance.UiManagerMainGame.UiTutorial.StopTutorialBoom();

        bool isTutorialBoom = LevelManagerMainGame.Instance.UiManagerMainGame.UiTutorial.isTutorialBoom;

        int a = GameManager.Instance.DataManager.GetBoom();

        if (a > 0)
        {
            GameManager.Instance.DataManager.AddBoom(-1);

            StartBoom(isTutorialBoom);

            btnBoom.gameObject.SetActive(false);
        }
        else
        {
            //LevelManagerMainGame.Instance.UiManagerMainGame.UiBoomShow.Show(true);

            AdsManager.Instance.ShowRewarded(() =>
            {
                StartBoom(isTutorialBoom);

                btnBoom.gameObject.SetActive(false);

            }, "Nuclear_Ingame");
        }
    }

    private void OnEnable()
    {
        //GameManager.Instance.DataManager.OnChangeBoom += OnChangeBoom;
    }

    private void OnDisable()
    {
        //GameManager.Instance.DataManager.OnChangeBoom -= OnChangeBoom;
    }

    private void OnChangeBoom()
    {
        //textBoom.text = GameManager.Instance.DataManager.GetNumberBoom().ToString();
    }

    public void StartBoom(bool isTutorial)
    {
        StartCoroutine(WaitForActionBoom());

        if (!isTutorial)
        {
            //StartCoroutine(WaitForActionBoom());
        }
        else
        {
            //StartCoroutine(WaitForActionBoomTutorial());
            GameManager.Instance.DataManager.SetHasTutorialBoom();
        }
    }

    IEnumerator WaitForActionBoomTutorial()
    {
        CharManager.Instance.SqawnSystem.SqawnAll();

        GameManager.Instance.SetTimeScale(0.1f);

        textBoom.text = GameManager.Instance.DataManager.GetBoom().ToString();

        btnBoom.enabled = false;

        uiPlane.StartMove();

        yield return new WaitForSecondsRealtime(1f);

        animBoom.StartAnim();

        yield return new WaitForSecondsRealtime(1f);

        //Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.transform.position.z);

        //Vector3 screenHeight = new Vector3(Screen.width / 2, Screen.height, Camera.main.transform.position.z);

        //Vector3 screenWidth = new Vector3(Screen.width, Screen.height / 2, Camera.main.transform.position.z);

        GameManager.Instance.SetTimeScale(1);

        CameraShake.Instance.Shake(0.5f, 1);

        List<IContactObject> enermy = new List<IContactObject>(CharManager.Instance.Enermies);

        for (int i = 0; i < enermy.Count; i++)
        {
            if(enermy[i] == null)
            {
                continue;
            }

            enermy[i].Hited(TypeWeapon.NuclearBoom, 100000000);

            //Vector3 goscreen = Camera.main.WorldToScreenPoint(enermy[i].transform.position / 15 * 5);

            //float distX = Vector3.Distance(new Vector3(Screen.width / 2, 0f, 0f), new Vector3(goscreen.x, 0f, 0f));

            //float distY = Vector3.Distance(new Vector3(0f, Screen.height / 2, 0f), new Vector3(0f, goscreen.y, 0f));

            //if (distX > Screen.width / 2 || distY > Screen.height / 2)
            //{
            //    enermy[i].GetComponent<HealthBase>().SubHealth(100000000, "");
            //}
        }
        GameManager.Instance.SoundManager.PlaySoundExplositon();

        GameManager.Instance.VibrateManager.Vibate(100);

        LevelManagerMainGame.Instance.QuestManager.SetQuest(TypeQuestGame.Use_Support_Item);

        yield return new WaitForSecondsRealtime(1.5f);

        btnBoom.enabled = true;
    }

    IEnumerator WaitForActionBoom()
    {
        HandleFireBase.Instance.LogEventWithParameter("Boom_Click", new FirebaseParam[] { new FirebaseParam("support_count", GameManager.Instance.DataManager.GetBoom()) });

        GameManager.Instance.SetTimeScale(0.1f);

        textBoom.text = GameManager.Instance.DataManager.GetBoom().ToString();

        btnBoom.enabled = false;

        uiPlane.StartMove();

        yield return new WaitForSecondsRealtime(1f);

        animBoom.StartAnim();

        yield return new WaitForSecondsRealtime(1f);

        //Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.transform.position.z);

        //Vector3 screenHeight = new Vector3(Screen.width / 2, Screen.height, Camera.main.transform.position.z);

        //Vector3 screenWidth = new Vector3(Screen.width, Screen.height / 2, Camera.main.transform.position.z);

        GameManager.Instance.SetTimeScale(1);

        CameraShake.Instance.Shake(0.5f, 1);

        List<IContactObject> enermy = new List<IContactObject>(CharManager.Instance.Enermies);

        //Debug.LogError(enermy.Count + "Boom");

        for (int i = 0; i < enermy.Count; i++)
        {
            if(enermy[i] == null)
            {
                continue;
            }

            var screenPos = Camera.main.WorldToScreenPoint(enermy[i].GetBody().position / LevelManagerMainGame.Instance.BaseCamera * 5);
            var onScreen = screenPos.x > 0f && screenPos.x < Screen.width && screenPos.y > 0f && screenPos.y < Screen.height;


            if (onScreen)
            {
                enermy[i].Hited(TypeWeapon.NuclearBoom, 100000000);
            }

            //Vector3 goscreen = Camera.main.WorldToScreenPoint(enermy[i].transform.position / 15 * 5);

            //float distX = Vector3.Distance(new Vector3(Screen.width / 2, 0f, 0f), new Vector3(goscreen.x, 0f, 0f));

            //float distY = Vector3.Distance(new Vector3(0f, Screen.height / 2, 0f), new Vector3(0f, goscreen.y, 0f));

            //if (distX > Screen.width / 2 || distY > Screen.height / 2)
            //{
            //    enermy[i].GetComponent<HealthBase>().SubHealth(100000000, "");
            //}
        }

        GameManager.Instance.SoundManager.PlaySoundExplositon();

        GameManager.Instance.VibrateManager.Vibate(100);

        LevelManagerMainGame.Instance.QuestManager.SetQuest(TypeQuestGame.Use_Support_Item);

        yield return new WaitForSecondsRealtime(1.5f);

        btnBoom.enabled = true;
    }
}
