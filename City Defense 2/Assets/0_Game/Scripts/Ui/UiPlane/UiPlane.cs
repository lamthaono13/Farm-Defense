using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UiPlane : MonoBehaviour
{
    [SerializeField] private float yInitial;

    [SerializeField] private float yMove;

    [SerializeField] private float timeMove;

    public void StartMove()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, yInitial, transform.localPosition.z);
        transform.DOLocalMoveY(yMove, timeMove).SetUpdate(true).SetEase(DG.Tweening.Ease.Linear);
    }

    public void ResetToInitial()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, yInitial, transform.localPosition.z);
    }
}
