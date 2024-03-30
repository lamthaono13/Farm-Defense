using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppressorAlly : Ally
{
    //[SerializeField] private float speedAttack;

    //private bool isShooting;

    public override void OnEnterState(StateChar stateChar)
    {
        base.OnEnterState(stateChar);

        switch (stateChar)
        {
            case StateChar.Idle:
                break;
            case StateChar.Run:

                render.Animation.Animate(TypeAnimation.Run, 1, true, null, null);
                
                break;
            case StateChar.FindTarget:

                currentTimeCheckUpdate = Random.Range(0, configTimeCheckUpdate);

                movement.StopMove();

                render.Animation.Animate(TypeAnimation.Idle, 1, true, null, null);

                break;
            case StateChar.Attack:

                render.Animation.Animate(TypeAnimation.Idle, 1, true, null, null);

                movement.StopMove();

                break;
            case StateChar.Die:

                break;
            case StateChar.Revive:
                break;
        }
    }

    public override void OnExitState(StateChar stateChar)
    {
        base.OnExitState(stateChar);

        switch (stateChar)
        {
            case StateChar.Idle:
                break;
            case StateChar.Run:
                break;
            case StateChar.FindTarget:
                break;
            case StateChar.Attack:
                break;
            case StateChar.Die:
                break;
            case StateChar.Revive:
                break;
        }
    }

    public override void OnUpdateState(StateChar stateChar)
    {
        base.OnUpdateState(stateChar);

        switch (stateChar)
        {
            case StateChar.Idle:
                break;
            case StateChar.Run:
                
                bool c = CheckAliveTarget();

                if (!c)
                {
                    ChangeState(StateChar.FindTarget);

                    //currentTimeAnimation = 0;

                    return;
                }

                if (target.GetBody().position.x < transform.position.x)
                {
                    render.SetFlip(true);
                }
                else
                {
                    render.SetFlip(false);
                }

                movement.StartMove(TypeMove.Destination, target.GetBody().position);

                currentTimeAnimation += Time.deltaTime;
                
                if(Vector3.Distance(transform.position, target.GetBody().position) <= rangeAttack)
                {
                    ChangeState(StateChar.Attack);

                    //currentTimeAnimation = 0;
                            
                }
                
                break;
            case StateChar.FindTarget:

                if(currentTimeCheckUpdate >= configTimeCheckUpdate)
                {
                    bool a = CheckTarget();

                    currentTimeAnimation += Time.deltaTime;

                    if (a)
                    {
                        if(Vector3.Distance(transform.position, target.GetBody().position) > rangeAttack)
                        {
                            ChangeState(StateChar.Run);

                            //currentTimeAnimation = 0;
                            
                        }
                        else
                        {
                            ChangeState(StateChar.Attack);
                        }
                    }
                }
                else
                {
                    currentTimeCheckUpdate += Time.deltaTime;
                }



                break;
            case StateChar.Attack:

                bool b = CheckAliveTarget();

                if (!b)
                {
                    ChangeState(StateChar.FindTarget);
                    
                    return;
                }

                if(Vector3.Distance(transform.position, target.GetBody().position) > rangeAttack)
                {
                    ChangeState(StateChar.FindTarget);

                    //currentTimeAnimation = 0;

                    return;
                }
                

                if(currentTimeAnimation >= speedAttack)
                {
                    currentTimeAnimation = 0;

                    render.Animation.Animate(TypeAnimation.Attack, speedAttack / render.Animation.GetTimeAnimation(TypeAnimation.Attack, false), false, () => { weapon.Attack(target); }, () => { ChangeState(StateChar.Attack); });

                    CheckSetNearestTarget();


                }
                else
                {
                    currentTimeAnimation += Time.deltaTime;
                }

                if (target.GetBody().position.x < transform.position.x)
                {
                    render.SetFlip(true);
                }
                else
                {
                    render.SetFlip(false);
                }

                break;
            case StateChar.Die:
                break;
            case StateChar.Revive:
                break;
        }
    }

    public void CheckSetNearestTarget()
    {
        if (canCheck)
        {
            var enermies = CharManager.Instance.Enermies;

            float minDistance = 10000000;

            for (int i = 0; i < enermies.Count; i++)
            {
                var screenPos = cameraCheck.WorldToScreenPoint(enermies[i].GetBody().position / 15 * 5);
                var onScreen = screenPos.x > 0f && screenPos.x < width && screenPos.y > 0f && screenPos.y < height;

                float distance = Vector3.Distance(transform.position, enermies[i].GetBody().position);

                if (distance <= radiusCheck && onScreen)
                {
                    if (distance < minDistance)
                    {
                        HealthBase health = enermies[i].GetHealth();

                        if (health.GetHealth() > 0)
                        {
                            SetTarget(enermies[i]);

                            minDistance = distance;
                        }
                    }
                }
            }
        }
    }
}
