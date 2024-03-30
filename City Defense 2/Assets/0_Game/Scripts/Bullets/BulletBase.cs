using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class BulletBase : MonoBehaviour
{
    [SerializeField] private bool isRotate;

    [ShowIf("isRotate")] [SerializeField] private float speedRotate;

    protected float damage;

    protected Vector3 postionTarget;

    protected Vector3 direction;

    [SerializeField] protected Rigidbody2D ri;

    [SerializeField] private float timeToDestroy;

    [SerializeField] protected float speedMove;

    protected bool canMove;

    private Coroutine coroutine;
    
    protected Tween tweenScale;

    protected TypeWeapon typeWeapon;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    public virtual void Init(DataBullet dataBullet)
    {
        damage = dataBullet.Damage;

        typeWeapon = dataBullet.TypeWeapon;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (isRotate)
        {
            transform.eulerAngles = transform.eulerAngles - new Vector3(0, 0, 1) * Time.deltaTime * speedRotate;
        }
    }

    public virtual void OnShoot(float _damage, Vector3 _postionTarget, Vector3 _direction)
    {
        tweenScale = transform.DOScale(Vector3.one, 0.1f).OnComplete(() => { tweenScale = null; });

        damage = _damage;

        postionTarget = _postionTarget;

        direction = _direction;
    }

    public virtual void OnStopShoot()
    {

    }
    
    protected IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        
        if(tweenScale != null)
        {
            tweenScale.Kill();
        }

        Destroy(gameObject);
    }
}

public class DataBullet
{
    public TypeWeapon TypeWeapon;
    
    public float Damage;
}
