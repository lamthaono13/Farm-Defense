using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupLobbyManager : MonoBehaviour
{
    [SerializeField] private List<UiCanvas> listPopup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPopup(TypePopupLobby typePopupLobby, bool isTrue)
    {
        listPopup[(int)typePopupLobby].Show(isTrue);
    }
}
