using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBgDauLau : UiCanvas
{
    [SerializeField] private float speed;

    [SerializeField] private float positionReWindX;
    [SerializeField] private float positionReWindY;

    [SerializeField] private List<ElementUiBgDauLau> elementUiBgDauLaus;


    private void Start()
    {
        for (int i = 0; i < elementUiBgDauLaus.Count; i++)
        {
            elementUiBgDauLaus[i].Init(positionReWindY, this);
        }
    }

    private void Update()
    {
        float deltaX = Time.deltaTime * speed;

        float deltaY = deltaX * ( 16.0f/ 21.0f);

        Vector3 u = new Vector3(deltaX, deltaY, 0);

        for (int i = 0; i < elementUiBgDauLaus.Count; i++)
        {
            elementUiBgDauLaus[i].UpdatePosition(u);
        }
    }

    public override void Show(bool isShow)
    {
        base.Show(isShow);

        for(int i = 0; i < elementUiBgDauLaus.Count; i++)
        {
            elementUiBgDauLaus[i].Show(isShow);
        }
    }

    public void OnReWindElement(ElementUiBgDauLau elementUiBgDauLau)
    {
        elementUiBgDauLau.OnReWind(elementUiBgDauLaus[elementUiBgDauLaus.Count - 1].GetComponent<RectTransform>().localPosition - new Vector3(positionReWindX, positionReWindY, 0));

        elementUiBgDauLaus.RemoveAt(0);

        elementUiBgDauLaus.Add(elementUiBgDauLau);
    }
}
