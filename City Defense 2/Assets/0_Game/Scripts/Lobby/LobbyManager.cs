using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance;

    [SerializeField] private UiLobbyManager uiLobbyManager;

    [SerializeField] private float timeWaitLoadDataDone;

    public UiLobbyManager UiLobbyManager => uiLobbyManager;

    [SerializeField] private Camera cameraLobby;

    public Camera Camera => cameraLobby;

    private TypeSlotEquip currentSwap;

    private TypeEquip typeEquipProfile;

    private TypeEquip typeEquipProfileEnermy;

    private int currentIdMapStage;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.SoundManager.PlaySoundBgLobby(true);

        StartCoroutine(WaitForLoadingData());

        uiLobbyManager.Init();
    }

    IEnumerator WaitForLoadingData()
    {
        yield return new WaitForSeconds(timeWaitLoadDataDone);
        GameManager.Instance.OnLoadingSceneDone(GameState.LOBBY);
    }

    public void OnClickBtn()
    {
        GameManager.Instance.LoadScene(2, TypeLoadScene.Additive, "");
    }

    public void SetCurrentSwap(TypeSlotEquip typeSlotEquip)
    {
        currentSwap = typeSlotEquip;
    }

    public TypeSlotEquip GetCurrentSwap()
    {
        return currentSwap;
    }

    public bool CheckCanSwap(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        if(!GameManager.Instance.DataManager.GetUnlockAlly(typeGroup, typeTier, typeId))
        {
            return false;
        }
        else
        {
            if(currentSwap == TypeSlotEquip.Slot1)
            {
                if (typeGroup != TypeGroup.Barrier) 
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if(typeGroup == TypeGroup.Barrier)
                {
                    return false;
                }
                else
                {
                    for(int i = 0; i < System.Enum.GetNames(typeof(TypeSlotEquip)).Length; i++)
                    {
                        TypeEquip typeEquip = GameManager.Instance.DataManager.GetEquipAlly((TypeSlotEquip)i);

                        if(typeGroup == typeEquip.TypeGroup && typeTier == typeEquip.TypeTier && typeId == typeEquip.TypeId && currentSwap != (TypeSlotEquip)i)
                        {
                            return false;
                        }


                    }

                    return true;
                }
            }
        }

        return true;
    }

    public bool CheckCanSwap(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId, TypeSlotEquip typeSlotEquip)
    {
        if (!GameManager.Instance.DataManager.GetUnlockAlly(typeGroup, typeTier, typeId))
        {
            return false;
        }
        else
        {
            if (typeSlotEquip == TypeSlotEquip.Slot1)
            {
                if (typeGroup != TypeGroup.Barrier)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (typeGroup == TypeGroup.Barrier)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < System.Enum.GetNames(typeof(TypeSlotEquip)).Length; i++)
                    {
                        TypeEquip typeEquip = GameManager.Instance.DataManager.GetEquipAlly((TypeSlotEquip)i);

                        if (typeGroup == typeEquip.TypeGroup && typeTier == typeEquip.TypeTier && typeId == typeEquip.TypeId && typeSlotEquip != (TypeSlotEquip)i)
                        {
                            return false;
                        }

                    }



                    return true;
                }
            }
        }

        return true;
    }

    public void SetTypeEquipProfile(TypeGroup typeGroup, TypeTier typeTier, TypeId typeId)
    {
        typeEquipProfile = new TypeEquip()
        {
            TypeGroup = typeGroup,
            TypeTier = typeTier,
            TypeId = typeId
        };
    }

    public TypeEquip GetTypeEquipProfile()
    {
        return typeEquipProfile;
    }

    public void SetTypeEquipProfileEnermy(TypeEquip typeEquip)
    {
        typeEquipProfileEnermy = typeEquip;
    }

    public TypeEquip GetTypeEquipProfileEnermy()
    {
        return typeEquipProfileEnermy;
    }

    public void SetCurrentIdMapStage(int a)
    {
        currentIdMapStage = a;
    }

    public int GetCurrentIdMapStage()
    {
        return currentIdMapStage;
    }
}