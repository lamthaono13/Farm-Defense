using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiChooseChar : UiCanvas
{
    [SerializeField] private List<ElementUiChooseChar> elementUiChooseChars;

    public override void Show(bool _isShow)
    {
        base.Show(_isShow);


    }

    public void Init()
    {

        for(int i = 0; i < elementUiChooseChars.Count; i++)
        {
            //int level = GameManager.Instance.DataManager.GetLevelAlly((TypeAlly)i);

            elementUiChooseChars[i].Init();
        }

        int currentLevelUnlock = GameManager.Instance.DataManager.GetLevelMaxUnlock();

        if(currentLevelUnlock < 3)
        {
            elementUiChooseChars[2].gameObject.SetActive(false);
        }

        Show(true);
    }

    public void OnChoosing(int id)
    {
        for(int i = 0; i < elementUiChooseChars.Count; i++)
        {
            if(i != id)
            {
                elementUiChooseChars[i].OnUnChoose();
            }
        }
    }

    public void OnEndgame(GameResult gameResult)
    {
        Show(false);
    }
}
