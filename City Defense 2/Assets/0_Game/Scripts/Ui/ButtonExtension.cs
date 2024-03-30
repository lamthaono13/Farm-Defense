using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;


public class ButtonExtension : Button
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.SoundManager.PlaySoundButton();

        enabled = false;

        transform.DOScale(Vector3.one * 0.9f, 0.1f).SetUpdate(true).SetEase(DG.Tweening.Ease.Linear).OnComplete(() => 
        {
            transform.DOScale(Vector3.one, 0.1f).SetUpdate(true).SetEase(DG.Tweening.Ease.Linear).OnComplete(() => 
            {
                //interactable = true;

                enabled = true;

                base.OnPointerClick(eventData);

            });
        });
    }
}