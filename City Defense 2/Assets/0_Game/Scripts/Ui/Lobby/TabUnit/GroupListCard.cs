using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class GroupListCard : SerializedMonoBehaviour
{
    [SerializeField] private List<List<UiCardUnit>> uiCardUnits;

    [SerializeField] private List<UiCardUnit> uiCardUnitsAll;

    [SerializeField] private List<GameObject> ListObjGroups;

    [SerializeField] private RectTransform contentScroll;

    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] private Transform positionTutorialLevel3;

    [SerializeField] private Transform positionTutorialLevel6;

    private TabUnitManager tabUnitManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.DataManager.OnChangeStar += Init;

        GameManager.Instance.DataManager.OnChangeLevelAlly += Init;
    }

    private void OnDestroy()
    {
        GameManager.Instance.DataManager.OnChangeStar -= Init;

        GameManager.Instance.DataManager.OnChangeLevelAlly -= Init;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        int countCardAll = 0;

        for (int i = 0; i < System.Enum.GetNames(typeof(TypeGroup)).Length; i++)
        {
            for (int j = 0; j < System.Enum.GetNames(typeof(TypeTier)).Length; j++)
            {
                for (int k = 0; k < System.Enum.GetNames(typeof(TypeId)).Length; k++)
                {
                    if(i >= uiCardUnits.Count)
                    {
                        continue;
                    }

                    uiCardUnits[i][j].Init(GameManager.Instance.DataManager.GetDataCard((TypeGroup)i, (TypeTier)j, (TypeId)k), LobbyManager.Instance.CheckCanSwap((TypeGroup)i, (TypeTier)j, (TypeId)k));

                    uiCardUnitsAll[countCardAll].Init(GameManager.Instance.DataManager.GetDataCard((TypeGroup)i, (TypeTier)j, (TypeId)k), LobbyManager.Instance.CheckCanSwap((TypeGroup)i, (TypeTier)j, (TypeId)k));

                    countCardAll++;
                }

            }
        }

        for (int i = 0; i < uiCardUnits.Count; i++)
        {
            for(int j = 0; j < uiCardUnits[i].Count; j++)
            {

            }
        }
    }

    public void Init(TabUnitManager _tabUnitManager, int idDefaul)
    {
        tabUnitManager = _tabUnitManager;

        int countCardAll = 0;

        List<int> listSorting = new List<int>();

        for (int i = 0; i < System.Enum.GetNames(typeof(TypeGroup)).Length; i++)
        {
            for(int j = 0; j< System.Enum.GetNames(typeof(TypeTier)).Length; j++)
            {
                for (int k = 0; k < System.Enum.GetNames(typeof(TypeId)).Length; k++)
                {
                    if(i >= uiCardUnits.Count)
                    {
                        continue;
                    }

                    uiCardUnits[i][j].Init(GameManager.Instance.DataManager.GetDataCard((TypeGroup)i, (TypeTier)j, (TypeId)k), LobbyManager.Instance.CheckCanSwap((TypeGroup)i, (TypeTier)j, (TypeId)k));

                    uiCardUnitsAll[countCardAll].Init(GameManager.Instance.DataManager.GetDataCard((TypeGroup)i, (TypeTier)j, (TypeId)k), LobbyManager.Instance.CheckCanSwap((TypeGroup)i, (TypeTier)j, (TypeId)k));

                    listSorting = SortListAll(listSorting, countCardAll, (TypeGroup)i, (TypeTier)j, (TypeId)k);

                    countCardAll++;
                }

            }       
        }

        for(int i = 0; i < ListObjGroups.Count; i++)
        {
            if(i == idDefaul)
            {
                ListObjGroups[i].gameObject.SetActive(true);
            }
            else
            {
                ListObjGroups[i].gameObject.SetActive(false);
            }
        }

        for(int i = 0; i < listSorting.Count; i++)
        {
            uiCardUnitsAll[listSorting[i]].transform.SetAsLastSibling();
        }
    }

    public void ChangeTab(int idTab)
    {
        for (int i = 0; i < ListObjGroups.Count; i++)
        {
            if (i == idTab)
            {
                ListObjGroups[i].gameObject.SetActive(true);
            }
            else
            {
                ListObjGroups[i].gameObject.SetActive(false);
            }
        }
    }

    public List<int> SortListAll(List<int> listBefore, int idSort, TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        int position = 0;

        bool a = GameManager.Instance.DataManager.GetUnlockAlly(typeGroup, typeTier, typeId);

        for (int i = 0; i < listBefore.Count; i++)
        {
            DataCard dataCard = uiCardUnitsAll[listBefore[i]].GetDataCard();

            bool b = GameManager.Instance.DataManager.GetUnlockAlly(dataCard.TypeGroup, dataCard.TypeTier, dataCard.TypeId);

            if (a)
            {
                if (b)
                {
                    if((int)typeTier == (int)dataCard.TypeTier)
                    {
                        if((int)typeGroup == (int)dataCard.TypeGroup)
                        {
                            if((int)typeId < (int)dataCard.TypeId)
                            {
                                break;
                            }
                            else
                            {
                                position++;
                            }
                        }
                        else
                        {
                            if((int)typeGroup < (int)dataCard.TypeGroup)
                            {
                                break;
                            }
                            else
                            {
                                position++;
                            }
                        }
                    }
                    else
                    {
                        if((int)typeTier < (int)dataCard.TypeTier)
                        {
                            break;
                        }
                        else
                        {
                            position++;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            else
            {
                if (b)
                {
                    position++;
                }
                else
                {
                    if ((int)typeTier == (int)dataCard.TypeTier)
                    {
                        if ((int)typeGroup == (int)dataCard.TypeGroup)
                        {
                            if ((int)typeId < (int)dataCard.TypeId)
                            {
                                break;
                            }
                            else
                            {
                                position++;
                            }
                        }
                        else
                        {
                            if ((int)typeGroup < (int)dataCard.TypeGroup)
                            {
                                break;
                            }
                            else
                            {
                                position++;
                            }
                        }
                    }
                    else
                    {
                        if ((int)typeTier < (int)dataCard.TypeTier)
                        {
                            break;
                        }
                        else
                        {
                            position++;
                        }
                    }
                }
            }
        }

        listBefore.Insert(position, idSort);

        return listBefore;
    }

    public void ScrollTabAllTo(float y)
    {
        contentScroll.localPosition = new Vector3(contentScroll.localPosition.x, y, contentScroll.localPosition.z);
    }

    public void OnTutorial(float y)
    {
        scrollRect.enabled = false;

        ScrollTabAllTo(y);
    }

    public void OnTutorialDone()
    {
        scrollRect.enabled = true;
    }

    public Transform GetTransformTutorialLevel3()
    {
        return positionTutorialLevel3;
    }

    public Transform GetTransformTutorialLevel6()
    {
        return positionTutorialLevel6;
    }
}
