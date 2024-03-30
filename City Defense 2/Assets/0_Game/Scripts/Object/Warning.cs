using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField] private GameObject objWarning;

    [SerializeField] private float timeWarning;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWarning()
    {
        StartCoroutine(WaitToDeActiveWarning());
    }

    IEnumerator WaitToDeActiveWarning()
    {
        objWarning.gameObject.SetActive(true);

        yield return new WaitForSeconds(timeWarning);

        objWarning.gameObject.SetActive(false);
    }
}
