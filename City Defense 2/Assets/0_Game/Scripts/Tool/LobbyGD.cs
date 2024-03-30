using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyGD : MonoBehaviour
{
    [SerializeField] private DataLobbyGD dataLobbyGD;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBtnPlay()
    {
        GameManager.Instance.DataManager.SetLevel(dataLobbyGD.LevelChoose);

        GameManager.Instance.DataManager.SetEquipAlly(TypeSlotEquip.Slot1, dataLobbyGD.Slot1.TypeEquip.TypeGroup, dataLobbyGD.Slot1.TypeEquip.TypeTier, dataLobbyGD.Slot1.TypeEquip.TypeId);

        GameManager.Instance.DataManager.SetLevelAlly(dataLobbyGD.Slot1.TypeEquip.TypeGroup, dataLobbyGD.Slot1.TypeEquip.TypeTier, dataLobbyGD.Slot1.TypeEquip.TypeId, dataLobbyGD.Slot1.level);

        GameManager.Instance.DataManager.SetEquipAlly(TypeSlotEquip.Slot2, dataLobbyGD.Slot2.TypeEquip.TypeGroup, dataLobbyGD.Slot2.TypeEquip.TypeTier, dataLobbyGD.Slot2.TypeEquip.TypeId);

        GameManager.Instance.DataManager.SetLevelAlly(dataLobbyGD.Slot2.TypeEquip.TypeGroup, dataLobbyGD.Slot2.TypeEquip.TypeTier, dataLobbyGD.Slot2.TypeEquip.TypeId, dataLobbyGD.Slot2.level);

        GameManager.Instance.DataManager.SetEquipAlly(TypeSlotEquip.Slot3, dataLobbyGD.Slot3.TypeEquip.TypeGroup, dataLobbyGD.Slot3.TypeEquip.TypeTier, dataLobbyGD.Slot3.TypeEquip.TypeId);

        GameManager.Instance.DataManager.SetLevelAlly(dataLobbyGD.Slot3.TypeEquip.TypeGroup, dataLobbyGD.Slot3.TypeEquip.TypeTier, dataLobbyGD.Slot3.TypeEquip.TypeId, dataLobbyGD.Slot3.level);

        LobbyManager.Instance.OnClickBtn();
    }
}
