using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Spine.Unity;
using Spine;

public class PopupProfileAlly : UiCanvas
{
    [SerializeField] private Button btnUnlock;
    [SerializeField] private Button btnUnlockGold;
    [SerializeField] private Button btnUpgrade;
    [SerializeField] private Button btnUpgradeStar;
    [SerializeField] private Button btnUse;

    [SerializeField] private GameObject objBtnPurchase;
    [SerializeField] private GameObject objBtnUse;
    [SerializeField] private GameObject objBtnUnlockGold;
    [SerializeField] private GameObject objBtnUnlockGem;

    [SerializeField] private TextMeshProUGUI textHealth;
    [SerializeField] private TextMeshProUGUI textDamage;

    [SerializeField] private TextMeshProUGUI textUnlock;
    [SerializeField] private TextMeshProUGUI textUpgrade;
    [SerializeField] private TextMeshProUGUI textUpgradeStar;

    [SerializeField] private GameObject objHideUnlock;
    [SerializeField] private GameObject objHideUpgrade;
    [SerializeField] private GameObject objHideUpgradeStar;
    [SerializeField] private GameObject objHideUnlockGold;

    [SerializeField] private List<GameObject> imgTiers;

    [SerializeField] private List<GameObject> imgGroups;

    [SerializeField] private Image imgRender;

    [SerializeField] private TextMeshProUGUI textLevel;

    [SerializeField] private List<Image> imgStars;

    [SerializeField] private TextMeshProUGUI textName;

    [SerializeField] private TextMeshProUGUI textDescription;

    [SerializeField] private TextMeshProUGUI textDescriptionAbility;

    [SerializeField] private TextMeshProUGUI textGoldUnlock;

    [SerializeField] private TextMeshProUGUI textDesGoldUnlock;

    [SerializeField] private Sprite spriteStar;

    [SerializeField] private Sprite spriteUnStar;

    [SerializeField] private PopupProfileAllySwap popupProfileAllySwap;

    [SerializeField] private ParticleSystem particleSystemUpgrade;

    [SerializeField] private ParticleSystem particleSystemLvUpStar;

    private DataProfileAlly dataProfileAlly;

    private bool isSetInitialSize;

    private Vector2 initialSize;

    private DataEachUnlock dataEachUnlock;

    private bool isInitialPositionRender;

    private Vector2 InitialPositionRender;

    private GameObject objLoadRender;

    private SkeletonGraphic skeletonGraphicLoadRender;

    private TrackEntry trackEntry;

    protected override void Start()
    {
        base.Start();

        btnUnlock.onClick.AddListener(OnClickBtnUnlock);
        btnUnlockGold.onClick.AddListener(OnClickBtnUnlockGold);
        btnUpgrade.onClick.AddListener(OnClickBtnUpgrade);
        btnUpgradeStar.onClick.AddListener(OnClickBtnStar);
        btnUse.onClick.AddListener(OnClickBtnUse);
    }

    public override void Show(bool _isShow)
    {
        base.Show(_isShow);

        if (_isShow)
        {
            popupProfileAllySwap.Show(false);

            TypeEquip typeEquip = LobbyManager.Instance.GetTypeEquipProfile();

            dataEachUnlock = GameManager.Instance.DataManager.DataManagerMainGame.GetDataEachUnlock(typeEquip.TypeGroup, typeEquip.TypeTier, typeEquip.TypeId);

            Init(GameManager.Instance.DataManager.GetDataProfileAlly(typeEquip.TypeGroup, typeEquip.TypeTier, typeEquip.TypeId));

            InitRender(GameManager.Instance.DataManager.GetDataProfileAlly(typeEquip.TypeGroup, typeEquip.TypeTier, typeEquip.TypeId));
        }
        else
        {
            if(trackEntry != null)
            {
                trackEntry = null;
            }

            if(objLoadRender != null)
            {
                Destroy(objLoadRender);
            }
        }
    }

    public void InitRender(DataProfileAlly _dataProfileAlly)
    {
        if(_dataProfileAlly.DataCard.TypeGroup == TypeGroup.Barrier)
        {
            imgRender.enabled = true;

            Sprite spriteGet = GameManager.Instance.DataManager.DataManagerMainGame.DataSprite.GetSpriteChar(dataProfileAlly.DataCard.TypeGroup, dataProfileAlly.DataCard.TypeTier, dataProfileAlly.DataCard.TypeId);

            //imgRender.sprite = spriteGet;

            //Vector2 origin = new Vector2(spriteGet.rect.width, spriteGet.rect.height);

            //imgRender.rectTransform.sizeDelta = ConvertSpriteSize.GetResize(initialSize, origin);

            Vector4 border = spriteGet.border;

            Vector2 sizeDataBoder = new Vector2(spriteGet.rect.width, spriteGet.rect.height) - new Vector2(border.x + border.z, border.y + border.w);

            float index = ConvertSpriteSize.GetKResize(initialSize, sizeDataBoder);

            imgRender.rectTransform.anchoredPosition = InitialPositionRender + ((new Vector2(spriteGet.rect.width, spriteGet.rect.height) * index) - initialSize) / 2 - (new Vector2(border.x, 0) * index);

            imgRender.rectTransform.sizeDelta = new Vector2(spriteGet.rect.width, spriteGet.rect.height) * index;

            imgRender.sprite = spriteGet;
        }
        else
        {
            imgRender.enabled = false;

            ConfigBaseIndex configBaseIndex = GameManager.Instance.DataManager.DataManagerMainGame.GetConfigBaseIndex(_dataProfileAlly.DataCard.TypeGroup, _dataProfileAlly.DataCard.TypeTier, _dataProfileAlly.DataCard.TypeId);

            textName.text = configBaseIndex.dataConfigForTypeCharBase.Name;

            string stringLoad = "Ui/AllyUi/" + _dataProfileAlly.DataCard.TypeGroup.ToString() + "/" + _dataProfileAlly.DataCard.TypeTier.ToString() + " " + _dataProfileAlly.DataCard.TypeId.ToString();

            GameObject objLoad = Resources.Load<GameObject>(stringLoad);

            objLoadRender = Instantiate(objLoad, imgRender.transform);

            skeletonGraphicLoadRender = objLoadRender.GetComponent<SkeletonGraphic>();
        }
    }

    public void Init(DataProfileAlly _dataProfileAlly)
    {
        dataProfileAlly = _dataProfileAlly;

        int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        bool b = GameManager.Instance.DataManager.GetHasTutorialLobbyLv6();

        if ((levelUnlock == 6 && !b) || (levelUnlock < 6 && levelUnlock > 3 && GameManager.Instance.DataManager.GetIsTutorialUpgradeLobby() && !GameManager.Instance.DataManager.GetHasTutorialUpgrade()))
        {
            if (GameManager.Instance.DataManager.GetGold() < (long)dataEachUnlock.GetLevelUp(dataProfileAlly.DataCard.Level))
            {
                GameManager.Instance.DataManager.AddGold((long)dataEachUnlock.GetLevelUp(dataProfileAlly.DataCard.Level), "Trick_Add_Gold_Tutorial");
            }
        }


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


        //tier

        for (int i = 0; i < imgTiers.Count; i++)
        {
            if(i == (int)dataProfileAlly.DataCard.TypeTier)
            {
                imgTiers[i].gameObject.SetActive(true);
            }
            else
            {
                imgTiers[i].gameObject.SetActive(false);
            }
        }

        //group

        for(int i = 0; i < imgGroups.Count; i++)
        {
            if(i == (int)dataProfileAlly.DataCard.TypeGroup)
            {
                imgGroups[i].gameObject.SetActive(true);
            }
            else
            {
                imgGroups[i].gameObject.SetActive(false);
            }
        }

        //star

        for(int i = 0; i < imgStars.Count; i++)
        {
            if((i + 1) <= dataProfileAlly.DataCard.Star)
            {
                imgStars[i].sprite = spriteStar;
            }
            else
            {
                imgStars[i].sprite = spriteUnStar;
            }
        }

        //render

        //Sprite spriteGet = GameManager.Instance.DataManager.DataManagerMainGame.DataSprite.GetSpriteChar(dataProfileAlly.DataCard.TypeGroup, dataProfileAlly.DataCard.TypeTier, dataProfileAlly.DataCard.TypeId);

        ////imgRender.sprite = spriteGet;

        ////Vector2 origin = new Vector2(spriteGet.rect.width, spriteGet.rect.height);

        ////imgRender.rectTransform.sizeDelta = ConvertSpriteSize.GetResize(initialSize, origin);

        //Vector4 border = spriteGet.border;

        //Vector2 sizeDataBoder = new Vector2(spriteGet.rect.width, spriteGet.rect.height) - new Vector2(border.x + border.z, border.y + border.w);

        //float index = ConvertSpriteSize.GetKResize(initialSize, sizeDataBoder);

        //imgRender.rectTransform.anchoredPosition = InitialPositionRender + ((new Vector2(spriteGet.rect.width, spriteGet.rect.height) * index) - initialSize) / 2 - (new Vector2(border.x, 0) * index);

        //imgRender.rectTransform.sizeDelta = new Vector2(spriteGet.rect.width, spriteGet.rect.height) * index;

        //imgRender.sprite = spriteGet;


        // text

        textHealth.text = NumberToString.ChangeNumberToString((int)dataProfileAlly.Health);

        textDamage.text = NumberToString.ChangeNumberToString((int)dataProfileAlly.Damage);

        textLevel.text = "Lv." + dataProfileAlly.DataCard.Level.ToString();

        textName.text = dataProfileAlly.Name;

        textDescription.text = dataProfileAlly.Description;

        textDescriptionAbility.text = dataProfileAlly.DescriptionAbility;

        // Check Unlock => CheckMaxLevelStar => 

        bool isUnlock = GameManager.Instance.DataManager.GetUnlockAlly(dataProfileAlly.DataCard.TypeGroup, dataProfileAlly.DataCard.TypeTier, dataProfileAlly.DataCard.TypeId);

        bool isMaxLevelStar = GameManager.Instance.DataManager.CheckIsMaxLevelInStarAlly(dataProfileAlly.DataCard.TypeGroup, dataProfileAlly.DataCard.TypeTier, dataProfileAlly.DataCard.TypeId);

        bool isMaxStar = GameManager.Instance.DataManager.CheckIsMaxStarAlly(dataProfileAlly.DataCard.TypeGroup, dataProfileAlly.DataCard.TypeTier, dataProfileAlly.DataCard.TypeId);

        objBtnUse.gameObject.SetActive(isUnlock);

        if (isUnlock)
        {
            objBtnUnlockGem.gameObject.SetActive(false);

            objBtnUnlockGold.gameObject.SetActive(false);

            if (isMaxLevelStar)
            {
                if (isMaxStar)
                {
                    // false all btn

                    btnUnlock.gameObject.SetActive(false);
                    btnUpgrade.gameObject.SetActive(false);
                    btnUpgradeStar.gameObject.SetActive(false);
                    objBtnPurchase.gameObject.SetActive(false);

                    Debug.Log("Bug Profile 1");

                }
                else
                {
                    // btn Upgrade Star

                    int gem = (int)dataEachUnlock.GetGemUpgradeStar();

                    textUpgradeStar.text = NumberToString.ChangeNumberToString(gem);

                    btnUnlock.gameObject.SetActive(false);
                    btnUpgrade.gameObject.SetActive(false);
                    btnUpgradeStar.gameObject.SetActive(true);

                    if (GameManager.Instance.DataManager.GetGem() >= gem)
                    {
                        btnUpgradeStar.enabled = true;
                        objHideUpgradeStar.gameObject.SetActive(false);
                    }
                    else
                    {
                        btnUpgradeStar.enabled = false;
                        objHideUpgradeStar.gameObject.SetActive(true);
                    }

                    objBtnPurchase.gameObject.SetActive(true);

                    Debug.Log("Bug Profile 2");
                }
            }
            else
            {
                // btn Ungrade

                objBtnPurchase.gameObject.SetActive(true);

                objHideUpgradeStar.gameObject.SetActive(false);

                long gold = (long)dataEachUnlock.GetLevelUp(dataProfileAlly.DataCard.Level);

                textUpgrade.text = NumberToString.ChangeNumberToString(gold);

                btnUnlock.gameObject.SetActive(false);
                btnUpgrade.gameObject.SetActive(true);
                btnUpgradeStar.gameObject.SetActive(false);

                if (GameManager.Instance.DataManager.GetGold() >= gold)
                {
                    btnUpgrade.enabled = true;
                    objHideUpgrade.gameObject.SetActive(false);
                }
                else
                {
                    btnUpgrade.enabled = false;
                    objHideUpgrade.gameObject.SetActive(true);
                }

                Debug.Log("Bug Profile 3");
            }

            for(int i = 0; i < System.Enum.GetNames(typeof(TypeSlotEquip)).Length; i++)
            {
                TypeEquip typeEquip = GameManager.Instance.DataManager.GetEquipAlly((TypeSlotEquip)i);

                if(typeEquip.TypeGroup == _dataProfileAlly.DataCard.TypeGroup && typeEquip.TypeTier == _dataProfileAlly.DataCard.TypeTier && typeEquip.TypeId == _dataProfileAlly.DataCard.TypeId)
                {
                    objBtnUse.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            objBtnPurchase.gameObject.SetActive(false);

            objHideUpgradeStar.gameObject.SetActive(false);

            objHideUpgrade.gameObject.SetActive(false);

            // btn Unlock

            int gem = (int)dataEachUnlock.GetGemUnlock();

            textGoldUnlock.text = dataProfileAlly.GoldUnlock.ToString();

            textDesGoldUnlock.text = dataProfileAlly.DesUnlockGold;

            if(dataProfileAlly.LevelConditionUnlock == 0)
            {
                objBtnUnlockGold.gameObject.SetActive(false);
            }
            else
            {
                objBtnUnlockGold.gameObject.SetActive(true);
            }

            objBtnUnlockGem.gameObject.SetActive(true);

            if (!GameManager.Instance.DataManager.CheckCanUnlockWithGold(dataProfileAlly))
            {
                btnUnlockGold.enabled = false;
                objHideUnlockGold.gameObject.SetActive(true);
            }
            else
            {
                btnUnlockGold.enabled = true;
                objHideUnlockGold.gameObject.SetActive(false);
            }

            textUnlock.text = NumberToString.ChangeNumberToString(gem);

            btnUnlock.gameObject.SetActive(true);
            btnUpgrade.gameObject.SetActive(false);
            btnUpgradeStar.gameObject.SetActive(false);

            if (GameManager.Instance.DataManager.GetGem() >= gem)
            {
                btnUnlock.enabled = true;
                objHideUnlock.gameObject.SetActive(false);
            }
            else
            {
                btnUnlock.enabled = false;
                objHideUnlock.gameObject.SetActive(true);
            }

            Debug.Log("Bug Profile 4");
        }
    }

    private void OnClickBtnUnlock()
    {
        GameManager.Instance.DataManager.AddGem(-(int)dataEachUnlock.GetGemUnlock(), "Use_Gem_Unlock_Ally");

        GameManager.Instance.DataManager.SetUnlockAlly(dataProfileAlly.DataCard.TypeGroup, dataProfileAlly.DataCard.TypeTier, dataProfileAlly.DataCard.TypeId);

        TypeEquip typeEquip = LobbyManager.Instance.GetTypeEquipProfile();

        Init(GameManager.Instance.DataManager.GetDataProfileAlly(typeEquip.TypeGroup, typeEquip.TypeTier, typeEquip.TypeId));

        int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        bool a = GameManager.Instance.DataManager.GetHasTutorialLobbyLv3();

        if (levelUnlock == 3 && !a)
        {
            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUnlockUnit();
        }

        StartAnimationCongratulation();
    }

    private void OnClickBtnUnlockGold()
    {
        TypeEquip typeEquip = LobbyManager.Instance.GetTypeEquipProfile();

        GameManager.Instance.DataManager.AddGold(-(int)GameManager.Instance.DataManager.GetDataProfileAlly(typeEquip.TypeGroup, typeEquip.TypeTier, typeEquip.TypeId).GoldUnlock, "Use_Gold_Unlock");

        GameManager.Instance.DataManager.SetUnlockAlly(dataProfileAlly.DataCard.TypeGroup, dataProfileAlly.DataCard.TypeTier, dataProfileAlly.DataCard.TypeId);

        Init(GameManager.Instance.DataManager.GetDataProfileAlly(typeEquip.TypeGroup, typeEquip.TypeTier, typeEquip.TypeId));

        //int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        //bool a = GameManager.Instance.DataManager.GetHasTutorialLobbyLv3();

        //if (levelUnlock == 3 && !a)
        //{
        //    LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUnlockUnit();
        //}

        StartAnimationCongratulation();
    }

    private void OnClickBtnUpgrade()
    {
        GameManager.Instance.DataManager.AddGold(-(long)dataEachUnlock.GetLevelUp(dataProfileAlly.DataCard.Level), "Use_Gold_Upgrade");

        GameManager.Instance.DataManager.SetLevelUpAlly(dataProfileAlly.DataCard.TypeGroup, dataProfileAlly.DataCard.TypeTier, dataProfileAlly.DataCard.TypeId);

        TypeEquip typeEquip = LobbyManager.Instance.GetTypeEquipProfile();

        Init(GameManager.Instance.DataManager.GetDataProfileAlly(typeEquip.TypeGroup, typeEquip.TypeTier, typeEquip.TypeId));

        int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        bool b = GameManager.Instance.DataManager.GetHasTutorialLobbyLv6();

        if (levelUnlock == 6 && !b)
        {
            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUpgradeUnit();
        }

        if (levelUnlock < 6 && levelUnlock > 3 && GameManager.Instance.DataManager.GetIsTutorialUpgradeLobby() && !GameManager.Instance.DataManager.GetHasTutorialUpgrade())
        {
            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUpgradeLose();
        }

        particleSystemUpgrade.Play();

        StartAnimationCongratulation();
    }

    private void OnClickBtnStar()
    {
        GameManager.Instance.DataManager.AddGem(-(int)dataEachUnlock.GetGemUpgradeStar(), "Use_Gem_Upgrade_Star");

        GameManager.Instance.DataManager.LevelUpStarAlly(dataProfileAlly.DataCard.TypeGroup, dataProfileAlly.DataCard.TypeTier, dataProfileAlly.DataCard.TypeId);

        TypeEquip typeEquip = LobbyManager.Instance.GetTypeEquipProfile();

        particleSystemLvUpStar.Play();

        Init(GameManager.Instance.DataManager.GetDataProfileAlly(typeEquip.TypeGroup, typeEquip.TypeTier, typeEquip.TypeId));

        StartAnimationCongratulation();
    }

    private void OnClickBtnUse()
    {
        int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        bool a = GameManager.Instance.DataManager.GetHasTutorialLobbyLv3();

        if (levelUnlock == 3 && !a)
        {
            GameManager.Instance.DataManager.SetEquipAlly(TypeSlotEquip.Slot3, LobbyManager.Instance.GetTypeEquipProfile().TypeGroup, LobbyManager.Instance.GetTypeEquipProfile().TypeTier, LobbyManager.Instance.GetTypeEquipProfile().TypeId);

            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUnlockUnit();

            Show(false);

            return;
        }

        popupProfileAllySwap.Init(LobbyManager.Instance.GetTypeEquipProfile());

        popupProfileAllySwap.Show(true);
    }

    public void OnClickBtnClose()
    {
        int levelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        bool b = GameManager.Instance.DataManager.GetHasTutorialLobbyLv6();

        if (levelUnlock == 6 && !b)
        {
            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUpgradeUnit();
        }

        if (levelUnlock < 6 && levelUnlock > 3 && GameManager.Instance.DataManager.GetIsTutorialUpgradeLobby() && !GameManager.Instance.DataManager.GetHasTutorialUpgrade())
        {
            LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUpgradeLose();
        }

        //bool c = GameManager.Instance.DataManager.GetHasTutorialLobbyLv3();

        //if (levelUnlock == 3 && !c)
        //{
        //    LobbyManager.Instance.UiLobbyManager.UiTutorialLobby.OnChangeTutorialUnlockUnit();
        //}

        Show(false);
    }

    public void StartAnimationCongratulation()
    {
        if(skeletonGraphicLoadRender == null || trackEntry != null)
        {
            return;
        }

        DataConfigAnimation dataConfigAnimation = GameManager.Instance.DataManager.DataManagerMainGame.GetDataConfigAnimationAlly(dataProfileAlly.DataCard.TypeGroup, dataProfileAlly.DataCard.TypeTier, dataProfileAlly.DataCard.TypeId);

        string animCongratulation = dataConfigAnimation.DataEachAnimations[(int)TypeAnimation.Congratulation].NameAnimtion;

        string animIdle = dataConfigAnimation.DataEachAnimations[(int)TypeAnimation.Idle].NameAnimtion;

        trackEntry = skeletonGraphicLoadRender.AnimationState.SetAnimation(0, animCongratulation, false);

        trackEntry.Complete += (a) =>
        {
            skeletonGraphicLoadRender.AnimationState.SetAnimation(0, animIdle, true);
            trackEntry = null;
        };
    }
}
