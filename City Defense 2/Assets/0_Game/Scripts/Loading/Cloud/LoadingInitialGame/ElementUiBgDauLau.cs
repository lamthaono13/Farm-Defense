using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementUiBgDauLau : UiCanvas
{
    private float positionReWind;

    private UiBgDauLau uiBgDauLau;

    private RectTransform positionRect;

    void Start()
    {
        positionRect = gameObject.GetComponent<RectTransform>();
    }

    public override void Show(bool isShow)
    {
        base.Show(isShow);
    }

    public void Update()
    {

    }

    public void Init(float _positionReWind, UiBgDauLau _uiBgDauLau)
    {
        uiBgDauLau = _uiBgDauLau;

        positionReWind = _positionReWind;
    }

    public void UpdatePosition(Vector3 positonAdd)
    {
        transform.localPosition += positonAdd;

        CheckReWind();
    }

    private void CheckReWind()
    {
        if(positionRect.anchoredPosition.y >= positionReWind)
        {
            uiBgDauLau.OnReWindElement(this);
        }
    }

    public void OnReWind(Vector3 positionReWind)
    {
        transform.localPosition = positionReWind;
    }
}
