using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPoolSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void Init(AudioClip audioClip)
    {
        //audioSource.clip = audioClip;

        gameObject.SetActive(true);

        if (GameManager.Instance.DataManager.GetSetting(TypeSetting.Sound))
        {
            audioSource.PlayOneShot(audioClip);
        }

        StartCoroutine(WaitReturn());
    }

    IEnumerator WaitReturn()
    {
        yield return new WaitForSeconds(2);

        gameObject.SetActive(false);

        GameManager.Instance.SoundManager.PoolSoundManager.ReturnSound(this);
    }
}
