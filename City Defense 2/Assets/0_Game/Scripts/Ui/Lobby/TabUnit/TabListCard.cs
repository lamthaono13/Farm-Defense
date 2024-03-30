using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabListCard : MonoBehaviour
{
    [SerializeField] private List<ElementTabCard> elementTabCards;

    private TabUnitManager tabUnitManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(TabUnitManager _tabUnitManager, int idDefaul)
    {
        tabUnitManager = _tabUnitManager;

        for(int i = 0; i < elementTabCards.Count; i++)
        {
            elementTabCards[i].Init(tabUnitManager, i, idDefaul == i);
        }
    }

    public void ChangeTab(int idTab)
    {
        for (int i = 0; i < elementTabCards.Count; i++)
        {
            elementTabCards[i].ActiveCard(i == idTab);
        }
    }
}
