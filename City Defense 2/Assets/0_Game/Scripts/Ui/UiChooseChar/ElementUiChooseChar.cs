using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ElementUiChooseChar : MonoBehaviour
{
    [SerializeField] private bool isResize;

    [SerializeField] private int id;
    [SerializeField] private int idSlot;
    [SerializeField] private Button btnChoose;
    [SerializeField] private Image imgHide;
    [SerializeField] private Image imgRender;
    [SerializeField] private Image imgBg;

    [SerializeField] private Image imgBgUnder;

    [SerializeField] private Image imgGroup;

    [SerializeField] private UiChooseChar uiChooseChar;

    //[SerializeField] private Sprite spriteChoose;
    //[SerializeField] private Sprite spriteUnChoose;

    [SerializeField] private TextMeshProUGUI textEnergy;

    [SerializeField] private TextMeshProUGUI textLevel;

    [SerializeField] private GameObject objScale;

    [SerializeField] private GameObject objOutline;

    private int numberSub;

    private bool isChoosing;

    private TypeEquip typeEquip;

    private bool isInitialPositionRender;

    private Vector2 InitialPositionRender;

    // Start is called before the first frame update
    void Start()
    {
        btnChoose.onClick.AddListener(OnClickBtnChoose);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        if (!isInitialPositionRender)
        {
            isInitialPositionRender = true;

            InitialPositionRender = imgRender.rectTransform.anchoredPosition;
        }


        typeEquip = GameManager.Instance.DataManager.GetEquipAlly((TypeSlotEquip)idSlot);

        int typeGroup = (int)typeEquip.TypeGroup;

        int levelAlly = GameManager.Instance.DataManager.GetLevelAlly(typeEquip.TypeGroup, typeEquip.TypeTier, typeEquip.TypeId);

        numberSub = GameManager.Instance.DataManager.DataManagerMainGame.GetConfigBaseIndex(typeEquip.TypeGroup, typeEquip.TypeTier, typeEquip.TypeId).dataConfigForTypeCharBase.Energy;

        textEnergy.text = numberSub.ToString();

        imgBg.sprite = GameManager.Instance.DataManager.DataManagerMainGame.DataSprite.GetSpriteBgTier(typeEquip.TypeTier);

        imgBgUnder.sprite = GameManager.Instance.DataManager.DataManagerMainGame.DataSprite.GetSpriteBgTierUnder(typeEquip.TypeTier);

        objScale.transform.localScale = Vector3.one;

        objOutline.gameObject.SetActive(false);

        EnergyManager.Instance.OnChangeEnergy += OnChangeEnergy;

        Sprite spriteGet = GameManager.Instance.DataManager.DataManagerMainGame.DataSprite.GetSpriteChar(typeEquip.TypeGroup, typeEquip.TypeTier, typeEquip.TypeId);




        if (isResize)
        {
            Vector4 border = spriteGet.border;

            Vector2 sizeDataBoder = new Vector2(spriteGet.rect.width, spriteGet.rect.height) - new Vector2(border.x + border.z, border.y + border.w);

            float index = ConvertSpriteSize.GetKResize(new Vector2(imgRender.rectTransform.sizeDelta.x, imgRender.rectTransform.sizeDelta.y), sizeDataBoder);

            imgRender.rectTransform.anchoredPosition = InitialPositionRender + ((new Vector2(spriteGet.rect.width, spriteGet.rect.height) * index) - imgRender.rectTransform.sizeDelta) / 2 - (new Vector2(border.x, 0) * index);

            imgRender.rectTransform.sizeDelta = new Vector2(spriteGet.rect.width, spriteGet.rect.height) * index;






            //Vector2 origin = new Vector2(spriteGet.rect.width, spriteGet.rect.height);

            //imgRender.rectTransform.sizeDelta = ConvertSpriteSize.GetResize(imgRender.rectTransform.sizeDelta, origin);


            imgRender.sprite = spriteGet;
        }

        textLevel.text = levelAlly.ToString();

        imgGroup.sprite = GameManager.Instance.DataManager.DataManagerMainGame.DataSprite.GetSpriteGroup(typeEquip.TypeGroup);
    }

    public void OnClickBtnChoose()
    {

        GameManager.Instance.SoundManager.PlaySoundChooseTroop();

        //imgBg.sprite = spriteChoose;

        objScale.transform.DOScale(Vector3.one * 1.1f, 0.1f).SetUpdate(true);

        objOutline.gameObject.SetActive(true);

        isChoosing = true;

        uiChooseChar.OnChoosing(id);

        CharManager.Instance.SqawnSystem.ChangeTypeSqawn(id, idSlot, numberSub);

        LevelManagerMainGame.Instance.UiManagerMainGame.UiTutorial.ChangeActionChoose();
    }

    public void OnUnChoose()
    {
        isChoosing = false;

        //imgBg.sprite = spriteUnChoose;

        objScale.transform.DOScale(Vector3.one, 0.1f).SetUpdate(true);

        objOutline.gameObject.SetActive(false);
    }

    public void OnChangeEnergy(float current, float max, bool none)
    {
        if(current >= numberSub)
        {
            ActiveElement(true);
        }
        else
        {
            ActiveElement(false);

            if (isChoosing)
            {
                OnUnChoose();

                CharManager.Instance.SqawnSystem.SetCanSqawn(false);
            }
        }
    }

    public void CheckActive()
    {

    }

    public void ActiveElement(bool active)
    {
        btnChoose.enabled = active;

        if (!GameManager.Instance.NoTutorial)
        {
            if (!active)
            {
                objScale.transform.DOScale(Vector3.one, 0.1f);

                objOutline.gameObject.SetActive(false);

                //imgBg.sprite = spriteUnChoose;

                imgRender.color = new Color(128, 128, 128);
            }
            else
            {
                imgRender.color = new Color(255, 255, 255);
            }
        }
        else
        {
            if (!active)
            {
                objScale.transform.DOScale(Vector3.one, 0.1f);

                objOutline.gameObject.SetActive(false);

                //imgBg.sprite = spriteUnChoose;

                //imgRender.color = new Color(128, 128, 128);
            }
            else
            {
                //imgRender.color = new Color(255, 255, 255);
            }
        }



        imgHide.gameObject.SetActive(!active);
    }
}
