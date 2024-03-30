using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTabLobbyManager : MonoBehaviour
{
    [SerializeField] private List<UiBtnTabLobby> uiBtnTabLobbies;

    [SerializeField] private List<UiTabLobby> uiTabLobbies;

    [SerializeField] private TypeTabLobby currentTabLobby;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        for(int i = 0; i < uiBtnTabLobbies.Count; i++)
        {
            if(i == (int)currentTabLobby)
            {
                uiBtnTabLobbies[i].Init(i, true);
            }
            else
            {
                uiBtnTabLobbies[i].Init(i, false);
            }
        }

        for (int i = 0; i < uiTabLobbies.Count; i++)
        {
            if (i == (int)currentTabLobby)
            {
                uiTabLobbies[i].Init(i, true);
            }
            else
            {
                uiTabLobbies[i].Init(i, false);
            }
        }
    }

    public void SetTab(int id)
    {
        if(id == (int)currentTabLobby)
        {
            return;
        }
        else
        {
            currentTabLobby = (TypeTabLobby)id;

            for (int i = 0; i < uiBtnTabLobbies.Count; i++)
            {
                if (i == (int)currentTabLobby)
                {
                    uiBtnTabLobbies[i].SetChoosing(true);
                }
                else
                {
                    uiBtnTabLobbies[i].SetChoosing(false);
                }
            }

            for (int i = 0; i < uiTabLobbies.Count; i++)
            {
                if (i == (int)currentTabLobby)
                {
                    uiTabLobbies[i].SetChoosing(true);
                }
                else
                {
                    uiTabLobbies[i].SetChoosing(false);
                }
            }
        }
    }

    public UiTabLobby GetTab(int id)
    {
        return uiTabLobbies[id];
    }
}
