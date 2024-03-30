using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ElementLevelStage : MonoBehaviour
{
    [SerializeField] private Button btnLevel;

    [SerializeField] private Image imgStar_1;

    [SerializeField] private Image imgStar_2;

    [SerializeField] private Image imgStar_3;

    [SerializeField] private GameObject objStar;

    [SerializeField] private Image imgBg;

    [SerializeField] private TextMeshProUGUI textLevel;

    [SerializeField] private Sprite spriteStar;

    [SerializeField] private Sprite spriteUnStar;

    [SerializeField] private Sprite spriteBgPass;

    [SerializeField] private Sprite spriteBgLock;

    [SerializeField] private Sprite spriteBgMax;

    private int level;

    private bool canButton;

    private UITabStageLobby uITabStageLobby;

    // Start is called before the first frame update
    void Start()
    {
        btnLevel.onClick.AddListener(OnClickBtnChoose);
    }

    public void Init(UITabStageLobby _uITabStageLobby, int _level)
    {
        uITabStageLobby = _uITabStageLobby;

        level = _level;

        int levelMaxReach = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        if(level < levelMaxReach)
        {
            textLevel.gameObject.SetActive(true);
            objStar.gameObject.SetActive(true);
            textLevel.text = level.ToString();

            canButton = true;

            imgBg.sprite = spriteBgPass;

            imgBg.SetNativeSize();

            int star = GameManager.Instance.DataManager.GetLevelStage(level).StarMax;

            if(star >= 1)
            {
                imgStar_1.sprite = spriteStar;
            }
            else
            {
                imgStar_1.sprite = spriteUnStar;
            }

            if (star >= 2)
            {
                imgStar_2.sprite = spriteStar;
            }
            else
            {
                imgStar_2.sprite = spriteUnStar;
            }

            if (star >= 3)
            {
                imgStar_3.sprite = spriteStar;
            }
            else
            {
                imgStar_3.sprite = spriteUnStar;
            }
        }
        else
        {
            objStar.gameObject.SetActive(false);

            if (level == levelMaxReach)
            {
                canButton = true;

                textLevel.gameObject.SetActive(true);
                textLevel.text = level.ToString();

                objStar.gameObject.SetActive(false);

                imgBg.sprite = spriteBgMax;

                imgBg.SetNativeSize();
            }
            else
            {
                canButton = false;

                textLevel.gameObject.SetActive(false);

                objStar.gameObject.SetActive(false);

                imgBg.sprite = spriteBgLock;

                imgBg.SetNativeSize();
            }
        }


    }

    private void OnClickBtnChoose()
    {
        if (!canButton)
        {
            return;
        }

        GameManager.Instance.DataManager.SetLevel(level);

        uITabStageLobby.ChangeTab(1);
    }
}