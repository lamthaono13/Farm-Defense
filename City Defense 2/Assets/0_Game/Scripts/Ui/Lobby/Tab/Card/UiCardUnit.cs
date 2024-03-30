using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiCardUnit : MonoBehaviour
{
    [SerializeField] protected Button btnCard;

    [SerializeField] private Image imgBg;

    [SerializeField] private Image imgBgUnder;

    [SerializeField] protected Image imgRender;

    [SerializeField] private Image imgIconGroup;

    [SerializeField] private TextMeshProUGUI textEnergy;

    [SerializeField] private TextMeshProUGUI textLevel;

    [SerializeField] private List<Image> listStar;

    [SerializeField] private Sprite spriteUnStar;

    [SerializeField] private Sprite spriteStar;

    protected DataCard _dataCard;

    private bool isSetInitialSize;

    private Vector2 initialSize;

    private bool isInitialPositionRender;

    private Vector2 InitialPositionRender;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        btnCard.onClick.AddListener(OnClickBtnCard);
    }

    protected virtual void OnClickBtnCard()
    {
        
    }

    protected virtual void Init(DataCard dataCard)
    {
        if (!isSetInitialSize)
        {
            isSetInitialSize = true;

            initialSize = imgRender.rectTransform.sizeDelta;
        }

        if (!isInitialPositionRender)
        {
            isInitialPositionRender = true;

            InitialPositionRender = imgRender.rectTransform.anchoredPosition;
        }

        _dataCard = dataCard;

        textEnergy.text = dataCard.Energy.ToString();

        textLevel.text = dataCard.Level.ToString();

        for(int i = 0; i < listStar.Count; i++)
        {
            if((i + 1) <= dataCard.Star)
            {
                listStar[i].sprite = spriteStar;
            }
            else
            {
                listStar[i].sprite = spriteUnStar;
            }
        }

        imgBg.sprite = GameManager.Instance.DataManager.DataManagerMainGame.DataSprite.GetSpriteBgTier(dataCard.TypeTier);

        imgBgUnder.sprite = GameManager.Instance.DataManager.DataManagerMainGame.DataSprite.GetSpriteBgTierUnder(dataCard.TypeTier);

        imgIconGroup.sprite = GameManager.Instance.DataManager.DataManagerMainGame.DataSprite.GetSpriteGroup(dataCard.TypeGroup);

        Sprite spriteGet = GameManager.Instance.DataManager.DataManagerMainGame.DataSprite.GetSpriteChar(dataCard.TypeGroup, dataCard.TypeTier, dataCard.TypeId);

        //Vector2 origin = new Vector2(spriteGet.rect.width, spriteGet.rect.height);

        //imgRender.rectTransform.sizeDelta = ConvertSpriteSize.GetResize(initialSize, origin);

        Vector4 border = spriteGet.border;

        Vector2 sizeDataBoder = new Vector2(spriteGet.rect.width, spriteGet.rect.height) - new Vector2(border.x + border.z, border.y + border.w);

        float index = ConvertSpriteSize.GetKResize(new Vector2(initialSize.x, initialSize.y), sizeDataBoder);

        imgRender.rectTransform.anchoredPosition = InitialPositionRender + ((new Vector2(spriteGet.rect.width, spriteGet.rect.height) * index) - initialSize) / 2 - (new Vector2(border.x, 0) * index);

        imgRender.rectTransform.sizeDelta = new Vector2(spriteGet.rect.width, spriteGet.rect.height) * index;

        imgRender.sprite = spriteGet;
    }

    public virtual void Init(DataCard dataCard, bool canChoose)
    {
        Init(dataCard);
    }

    public DataCard GetDataCard()
    {
        return _dataCard;
    }
}