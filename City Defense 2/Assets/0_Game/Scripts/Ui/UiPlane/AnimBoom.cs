using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimBoom : MonoBehaviour
{
    
    [SerializeField] private GameObject objAnim;

    [SerializeField] private float yInitial;

    [SerializeField] private float yMove;

    [SerializeField] private float timeBoomMove;

    [SerializeField] private float timeGroundBreakExist;

    [SerializeField] private GameObject objBoom;

    [SerializeField] private GameObject objGroundBreak;

    [SerializeField] private ParticleSystem particleSystemSmoke;

    [SerializeField] private ParticleSystem particleSystemBoom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnim()
    {
        //objAnim.SetActive(true);

        objBoom.transform.localPosition = new Vector3(objBoom.transform.localPosition.x, yInitial, objBoom.transform.localPosition.z);

        objBoom.SetActive(true);

        objAnim.gameObject.SetActive(true);

        particleSystemSmoke.Play();

        objBoom.transform.DOLocalMoveY(yMove, timeBoomMove).SetUpdate(true).SetEase(DG.Tweening.Ease.Linear).OnComplete(() => { objBoom.gameObject.SetActive(false); objGroundBreak.SetActive(true); StartCoroutine(WaitAnimDone()); });

        //animator.Play("FallBoom");
    }

    IEnumerator WaitAnimDone()
    {
        particleSystemBoom.Play();

        yield return new WaitForSeconds(timeGroundBreakExist);

        objGroundBreak.gameObject.SetActive(false);

        //objAnim.gameObject.SetActive(false);
    }
}
