using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : CharacterBase
{
    private TypeSlotEquip typeSlotEquip;

    protected Camera cameraCheck;

    protected float width;

    protected float height;

    public override void OnEnable()
    {
        base.OnEnable();

        width = Screen.width * Help.NUMBER_CONFIG_SCREEN_W;

        height = Screen.height * Help.NUMBER_CONFIG_SCREEN_H;

        cameraCheck = Camera.main;

        CharManager.Instance.AddAlly(this);
    }

    public override void OnDisable()
    {
        base.OnDisable();

        CharManager.Instance.RemoveAlly(this);
    }

    public override void OnStartLevel()
    {
        base.OnStartLevel();

        //ChangeState(StateChar.FindTarget);
    }

    public override void InitIndexConfig(DataSqawn dataSqawn)
    {
        base.InitIndexConfig(dataSqawn);
    }

    public override void Init(TypeSlotEquip _typeSlotEquip = TypeSlotEquip.Slot1)
    {
        base.Init(_typeSlotEquip);

        typeSlotEquip = _typeSlotEquip;

        ChangeState(StateChar.FindTarget);
    }

    protected override bool CheckTarget()
    {
        bool check = false;

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

                            check = true;
                        }
                    }
                }
            }
        }

        return check;
    }

    protected override bool CheckTargetPriority(TypeGroup _typeGroup)
    {
        //bool check = false;

        //if (canCheck)
        //{
        //    var enermies = CharManager.Instance.Enermies;

        //    float minDistance = 10000000;

        //    bool hasPriority = false;



        //    for (int i = 0; i < enermies.Count; i++)
        //    {
        //        var screenPos = cameraCheck.WorldToScreenPoint(enermies[i].GetBody().position / 15 * 5);
        //        var onScreen = screenPos.x > 0f && screenPos.x < width && screenPos.y > 0f && screenPos.y < height;

        //        float distance = Vector3.Distance(transform.position, enermies[i].GetBody().position);

        //        if (enermies[i].GetTypeGroup() == _typeGroup)
        //        {
        //            if (!hasPriority)
        //            {
        //                if (onScreen)
        //                {
        //                    SetTarget(enermies[i]);

        //                    minDistance = distance;

        //                    check = true;

        //                    hasPriority = true;
        //                }
        //                else
        //                {
        //                    continue;
        //                }
        //            }
        //            else
        //            {
        //                if (distance <= radiusCheck && onScreen)
        //                {
        //                    if (distance < minDistance)
        //                    {
        //                        HealthBase health = enermies[i].GetHealth();

        //                        if (health.GetHealth() > 0)
        //                        {
        //                            SetTarget(enermies[i]);

        //                            minDistance = distance;

        //                            check = true;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (!hasPriority)
        //            {
        //                if (distance <= radiusCheck && onScreen)
        //                {
        //                    if (distance < minDistance)
        //                    {
        //                        HealthBase health = enermies[i].GetHealth();

        //                        if (health.GetHealth() > 0)
        //                        {
        //                            SetTarget(enermies[i]);

        //                            minDistance = distance;

        //                            check = true;
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                continue;
        //            }
        //        }


        //    }
        //}

        //return check;

        bool check = false;

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

                            check = true;
                        }
                    }
                }
            }
        }

        return check;
    }
    
    protected override bool CheckTargetOnGround()
    {
        bool check = false;

        if (canCheck)
        {
            var enermies = CharManager.Instance.Enermies;

            float minDistance = 10000000;

            for (int i = 0; i < enermies.Count; i++)
            {
                if (enermies[i].GetTypePosition() == TypePosition.Sky)
                {
                    continue;
                }

                var screenPos = cameraCheck.WorldToScreenPoint(enermies[i].GetBody().position / 15 * 5);
                var onScreen = screenPos.x > (0f + width * 0.03) && screenPos.x < (width - width * 0.03) && screenPos.y > (0f + height * 0.04) && screenPos.y < (height - height * 0.04);

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

                            check = true;
                        }
                    }
                }
            }
        }

        return check;
    }

    public override void OnEnterState(StateChar stateChar)
    {
        base.OnEnterState(stateChar);

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

                StartWaitToDie(() => Pooling.Instance.PoolAlly.DeSqawn(typeSlotEquip, gameObject));

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

                break;
            case StateChar.FindTarget:

                break;
            case StateChar.Attack:
                if(target != null)
                {
                    //Debug.Log(target);

                    try
                    {
                        render.Animation.Rotate(target.GetBody().position);
                    }
                    catch
                    {

                    }


                }
                break;
            case StateChar.Die:
                break;
            case StateChar.Revive:
                break;
        }
    }

    public override void OnStun(float timeStun)
    {
        base.OnStun(timeStun);
        ChangeState(StateChar.Stun);
        StartCoroutine(WaitStun(timeStun));
    }

    IEnumerator WaitStun(float timeStun)
    {
        yield return new WaitForSeconds(timeStun);

        if(state != StateChar.Die)
        {
            ChangeState(StateChar.FindTarget);
        }
    }

#if UNITY_EDITOR
    public virtual void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.yellow;

        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radiusCheck);
    }

#endif
}
