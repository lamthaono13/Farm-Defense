using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovementNavmesh : Movement
{
    [SerializeField] private NavMeshAgent navMeshAgent;

    [SerializeField] private bool isEnermy;



    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        CustomPropertyAvoid();

        if (canMove)
        {
            switch (typeMove)
            {
                case TypeMove.Standing:
                    break;
                case TypeMove.Destination:

                    //if (navMeshAgent.isOnNavMesh && navMeshAgent.isActiveAndEnabled)
                    //{
                    //    navMeshAgent.SetDestination(target);
                    //}

                    Vector3 j = target - transform.position;

                    j = j.normalized;

                    if (navMeshAgent.isOnNavMesh && navMeshAgent.isActiveAndEnabled)
                    {
                        navMeshAgent.velocity = j * speed;
                    }

                    //Vector3 u1 = target - transform.position;

                    //u1 = u1.normalized;

                    //navMeshAgent.velocity = u1 * speed;

                    break;
                case TypeMove.Move:

                    Vector3 u = target - transform.position;

                    u = u.normalized;

                    if (navMeshAgent.isOnNavMesh && navMeshAgent.isActiveAndEnabled)
                    {
                        navMeshAgent.velocity = u * speed;
                    }



                    //navMeshAgent.Move(u * speed);

                    //navMeshAgent.Move(target);

                    break;
            }
        }
    }

    public override void StartMove(TypeMove _typeMove, Vector3 _target)
    {
        base.StartMove(_typeMove, _target);

        navMeshAgent.speed = speed;

        if (navMeshAgent.enabled && navMeshAgent.isOnNavMesh)
        {
            if (navMeshAgent.isStopped)
            {
                navMeshAgent.isStopped = false;
            }
        }

        //if (canMove)
        //{
        //    switch (typeMove)
        //    {
        //        case TypeMove.Standing:
        //            break;
        //        case TypeMove.Destination:

        //            if (navMeshAgent.isOnNavMesh && navMeshAgent.isActiveAndEnabled)
        //            {
        //                navMeshAgent.SetDestination(target);
        //            }
        //            break;
        //        case TypeMove.Move:
        //            break;
        //    }
        //}
    }

    public override void MoveTo(Vector3 offset)
    {
        base.MoveTo(offset);

        navMeshAgent.speed = speed;

        if (navMeshAgent.enabled)
        {
            if (navMeshAgent.isStopped)
            {
                navMeshAgent.isStopped = false;
            }
        }

        navMeshAgent.Move(transform.position + offset);
    }

    public override void StopMove()
    {
        base.StopMove();

        if (navMeshAgent.isOnNavMesh && !navMeshAgent.isStopped)
        {
            navMeshAgent.isStopped = true;
        }
    }

    public override void OnDie()
    {
        base.OnDie();

        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.isStopped = true;
        }

        navMeshAgent.enabled = false;
    }

    public override void FixPositionNavmesh()
    {
        base.FixPositionNavmesh();
    }

    public void CustomPropertyAvoid()
    {
        if (isFreezeSpeed)
        {
            navMeshAgent.avoidancePriority = 1;

            return;
        }

        //if (isEnermy)
        //{
        //    //int a = (int)transform.position.y + 50;

        //    int a = (int)transform.position.y + 10;

        //    navMeshAgent.avoidancePriority = a;
        //}
        //else
        //{
        //    int a = (int)transform.position.y + 50;

        //    navMeshAgent.avoidancePriority =  99 - a;
        //}


    }

    public override void ActiveMovement(bool isTrue)
    {
        base.ActiveMovement(isTrue);

        navMeshAgent.enabled = isTrue;
    }

    public override void SetSpeed(float _speed)
    {
        base.SetSpeed(_speed);

        navMeshAgent.speed = speed;
    }

    public override void SetFreezeSpeed(bool isTrue)
    {
        base.SetFreezeSpeed(isTrue);

        isFreezeSpeed = isTrue;

        if (isTrue)
        {
            speed = 0;
            navMeshAgent.speed = 0;
        }
        else
        {
            speed = initialSpeed;
            navMeshAgent.speed = initialSpeed;
        }
    }
}
