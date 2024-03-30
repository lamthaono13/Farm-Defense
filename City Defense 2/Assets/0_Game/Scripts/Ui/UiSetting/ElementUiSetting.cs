using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ElementUiSetting : MonoBehaviour
{
    [SerializeField] private TypeSetting typeSetting;

    [SerializeField] private List<Sprite> spritesBg;

    [SerializeField] private List<Sprite> spritesBtn;

    [SerializeField] private Button btn;

    [SerializeField] private Image imgBtn;

    [SerializeField] private Image imgBg;

    [SerializeField] private float zOff;

    [SerializeField] private float zOn;

    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(OnClickBtn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        bool isActive = GameManager.Instance.DataManager.GetSetting(typeSetting);

        if (isActive)
        {
            imgBtn.transform.localPosition = new Vector3(zOn, imgBtn.transform.localPosition.y, imgBtn.transform.localPosition.z);
            imgBtn.sprite = spritesBtn[0];
            imgBg.sprite = spritesBg[0];
        }
        else
        {
            imgBtn.transform.localPosition = new Vector3(zOff, imgBtn.transform.localPosition.y, imgBtn.transform.localPosition.z);
            imgBtn.sprite = spritesBtn[1];
            imgBg.sprite = spritesBg[1];
        }
    }

    public void OnClickBtn()
    {
        bool isActive = GameManager.Instance.DataManager.GetSetting(typeSetting);

        GameManager.Instance.DataManager.SetSetting(typeSetting, !isActive);

        if (!isActive)
        {
            imgBtn.transform.DOLocalMoveX(zOn, 0.2f).SetUpdate(true);
            imgBtn.sprite = spritesBtn[0];
            imgBg.sprite = spritesBg[0];
        }
        else
        {
            imgBtn.transform.DOLocalMoveX(zOff, 0.2f).SetUpdate(true);
            imgBtn.sprite = spritesBtn[1];
            imgBg.sprite = spritesBg[1];
        }
    }
}
