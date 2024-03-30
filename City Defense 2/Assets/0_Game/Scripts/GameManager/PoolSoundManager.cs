using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSoundManager : MonoBehaviour
{
    [SerializeField] private int numberPool;

    [SerializeField] private GameObject objLoad;

    [SerializeField] private List<AudioClip> audioClips;

    private List<ElementPoolSound> audioSourcesSqawn;

    private List<ElementPoolSound> audioSourcesContain;

    private List<ElementPoolSound> audioSourcesHasUse;

    public void Init()
    {
        audioSourcesSqawn = new List<ElementPoolSound>();

        audioSourcesContain = new List<ElementPoolSound>();

        audioSourcesHasUse = new List<ElementPoolSound>();

        for(int i = 0; i < numberPool; i++)
        {
            GameObject objInstan = Instantiate(objLoad, transform);

            ElementPoolSound elementPoolSound = objInstan.GetComponent<ElementPoolSound>();

            audioSourcesSqawn.Add(elementPoolSound);

            audioSourcesContain.Add(elementPoolSound);
        }
    }

    public void PlaySound(TypeSound typeSound)
    {
        audioSourcesContain[0].Init(audioClips[(int)typeSound]);

        audioSourcesHasUse.Add(audioSourcesContain[0]);

        audioSourcesContain.RemoveAt(0);
    }

    public void ReturnSound(ElementPoolSound _elementPoolSound)
    {
        audioSourcesHasUse.Remove(_elementPoolSound);

        audioSourcesContain.Add(_elementPoolSound);
    }
}

public enum TypeSound
{
    Sqawn,
}