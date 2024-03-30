using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiReload : MonoBehaviour
{
    [SerializeField] private Button btnReload;

    // Start is called before the first frame update
    void Start()
    {
        btnReload.onClick.AddListener(OnClickBtnReload);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBtnReload()
    {
        GameManager.Instance.OnReload();
    }
}
