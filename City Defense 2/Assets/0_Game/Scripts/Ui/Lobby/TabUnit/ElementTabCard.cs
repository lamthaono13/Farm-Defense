using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementTabCard : MonoBehaviour
{
    [SerializeField] private Button btnCard;

    [SerializeField] private Image imgBg;

    [SerializeField] private Image imgRender;

    [SerializeField] private Sprite spriteBgChoose;

    [SerializeField] private Sprite spriteBgUnChoose;

    [SerializeField] private Color colorRenderChoose;

    [SerializeField] private Color colorRenderUnChoose;

    private TabUnitManager tabUnitManager;

    private int idTab;

    // Start is called before the first frame update
    void Start()
    {
        btnCard.onClick.AddListener(OnClickBtnCard);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(TabUnitManager _tabUnitManager, int _idTab, bool isActive)
    {
        tabUnitManager = _tabUnitManager;

        idTab = _idTab;

        ActiveCard(isActive);
    }

    public void ActiveCard(bool isActive)
    {
        imgBg.sprite = isActive ? spriteBgChoose : spriteBgUnChoose;

        imgBg.SetNativeSize();

        imgRender.color = isActive ? colorRenderChoose : colorRenderUnChoose;
    }

    private void OnClickBtnCard()
    {
        tabUnitManager.ChangeTab(idTab);
    }
}
