using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Movement : MonoBehaviour
{
    [SerializeField] protected float speed;

    [SerializeField] protected float specialSpeed;

    protected bool isFreezeSpeed;

    protected bool canMove;

    protected Vector3 target;

    protected TypeMove typeMove;

    protected float initialSpeed;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public void InitIndexConfig(float _speed, float _specialSpeed)
    {
        speed = _speed;
        initialSpeed = speed;
        specialSpeed = _specialSpeed;
        ActiveMovement(true);
    }

    public virtual void StartMove(TypeMove _typeMove, Vector3 _target)
    {
        canMove = true;

        typeMove = _typeMove;

        target = _target;
    }

    public virtual void StopMove()
    {
        typeMove = TypeMove.Standing;

        canMove = false;
    }

    public virtual void OnDie()
    {
        ActiveMovement(false);
    }

    public virtual void MoveTo(Vector3 off)
    {

    }


    public virtual void FixPositionNavmesh()
    {

    }

    public virtual void ActiveMovement(bool isTrue)
    {
        
    }

    public virtual void SpecialMovement()
    {

    }

    public virtual void MoveLeft()
    {
        transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed;
    }

    public virtual void MoveRight()
    {
        transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
    }

    public virtual void MoveStraight()
    {
        transform.position += new Vector3(0, -1, 0) * Time.deltaTime * speed;
    }

    public virtual void SetSpeed(float _speed)
    {
        if (isFreezeSpeed)
        {
            return;
        }

        speed = _speed;
    }

    public virtual float GetSpeed()
    {
        return speed;
    }

    public virtual void SetFreezeSpeed(bool isTrue)
    {

    }
}

public enum TypeMove
{
    Standing,
    Destination,
    Move,
    Fly
}
