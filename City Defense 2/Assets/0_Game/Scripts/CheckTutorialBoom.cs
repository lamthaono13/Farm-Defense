using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTutorialBoom : MonoBehaviour
{
    private bool isCheckDone;

    private int currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        isCheckDone = false;

        currentLevel = GameManager.Instance.DataManager.GetLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isCheckDone || currentLevel != 7 || GameManager.Instance.NoTutorial || GameManager.Instance.DataManager.HasTutorialBoom())
        {
            return;
        }

        if (collision.CompareTag("Enermy"))
        {
            isCheckDone = true;

            LevelManagerMainGame.Instance.UiManagerMainGame.UiTutorial.StartTutorialBoom();
        }
    }
}
