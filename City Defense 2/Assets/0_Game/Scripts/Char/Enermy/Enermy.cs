using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy : CharacterBase
{
    //[SerializeField] private bool isBoss;

    //[SerializeField] protected TypeEnermy typeEnermy;

    //[SerializeField] protected EnermyId enermyId;

    //[SerializeField] protected float coinEarn;

    protected float xToMoveRightRun = -2.8f;

    protected float xToMoveLeftRun = 2.8f;

    //public TypeEnermy TypeEnermy => typeEnermy;

    protected Vector3 postionFinish;

    //protected int energyErn;

    protected bool isGoStraight;

    protected bool isDifferentStraight;

    protected Camera cameraCheck;

    protected float width;

    protected float height;



    public override void Start()
    {
        base.Start();

        width = Screen.width * Help.NUMBER_CONFIG_SCREEN_W;

        height = Screen.height * Help.NUMBER_CONFIG_SCREEN_H;

        cameraCheck = Camera.main;
    }

    public override void OnEnable()
    {
        base.OnEnable();

        CharManager.Instance.AddEnermy(this);

        Init();
    }

    protected virtual void OnDestroy()
    {


        //LevelManagerMainGame.Instance.AddGoldEarn(coinEarn);
    }

    public override void InitIndexConfig(DataSqawn dataSqawn)
    {
        // dataConfig = LevelManagerMainGame.Instance.DataConfig;
        //
        base.InitIndexConfig(dataSqawn);

        postionFinish = ObjectManager.Instance.GetPositionFinish();
    }

    public override void Init(TypeSlotEquip _typeSlotEquip = TypeSlotEquip.Slot1)
    {
        base.Init(_typeSlotEquip);

        isGoStraight = false;

        //postionFinish = LevelManagerMainGame.Instance.ObjectManager.GetPositionFinish();

        ChangeState(StateChar.Run);

        currentTimeAnimation = speedAttack;

        canCheck = true;

        health.EventChangeHealth += OnChangeHealth;
    }

    protected virtual void OnChangeHealth(float currentHealth, float number, float healthMax)
    {
        if (currentHealth - number <= 0)
        {
            EnergyManager.Instance.AddEnergy(energy);
        }
    }

    public virtual void SetPositionFinishToStraight()
    {
        postionFinish = new Vector3(Random.Range(-2.8f, 2.8f) + 0.01f, postionFinish.y, postionFinish.z);
    }

    public virtual void OnBreak()
    {

    }

    public override void OnEnterState(StateChar stateChar)
    {
        base.OnEnterState(stateChar);

        switch (stateChar)
        {
            case StateChar.Idle:

                break;

            case StateChar.Run:

                render.Animation.Animate(TypeAnimation.Run, 1, true, null, null);

                //Vector3 u = new Vector3(postionFinish.x, transform.position.y, postionFinish.z);

                break;

            case StateChar.FindTarget:

                break;

            case StateChar.Attack:

                //currentTimeAnimation = 0;

                if (target != null)
                {
                    if (target.GetBody().position.x < transform.position.x)
                    {
                        render.SetFlip(true);
                    }
                    else
                    {
                        render.SetFlip(false);
                    }

                    render.Animation.Rotate(target.GetBody().position);
                }



                movement.StopMove();

                break;

            case StateChar.Die:

                //Destroy(gameObject);

                //Debug.Log(1);

                CharManager.Instance.RemoveEnermy(this);

                Pooling.Instance.PoolEffect.SqawnEnergyEffect(GetBody().position);

                //EnergyManager.Instance.AddEnergy(energyErn);

                StartWaitToDie(() => Destroy(gameObject));

                break;

            case StateChar.Revive:

                break;

            case StateChar.Destroy:

                //Pooling.Instance.PoolEffect.SqawnEnergyEffect(GetBody().position);

                //EnergyManager.Instance.AddEnergy(energyErn);

                CharManager.Instance.RemoveEnermy(this);

                //StartWaitToDie(() => Destroy(gameObject));

                Destroy(gameObject);

                break;
            case StateChar.Stun:
                break;
        }
    }

    public override void OnExitState(StateChar stateChar)
    {
        base.OnExitState(stateChar);

        switch (stateChar)
        {
            case StateChar.Stun:
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

                //Vector3 u = new Vector3(transform.position.x, postionFinish.y, transform.position.z);

                TypeMove _typeMove = TypeMove.Destination;

                if (isGoStraight)
                {
                    //SetPositionFinishToStraight();
                    _typeMove = TypeMove.Move;

                    Vector3 u = new Vector3(postionFinish.x, postionFinish.y, transform.position.z);

                    if (postionFinish.x == transform.position.x)
                    {
                        u.x += 0.01f;
                    }

                    if (u.x < transform.position.x)
                    {
                        render.SetFlip(true);
                    }
                    else
                    {
                        render.SetFlip(false);
                    }

                    movement.StartMove(TypeMove.Destination, u);
                }
                else
                {
                    if (transform.position.x < xToMoveRightRun)
                    {
                        MoveRight();

                        isDifferentStraight = false;
                    }
                    else
                    {
                        if (transform.position.x > xToMoveLeftRun)
                        {
                            MoveLeft();

                            isDifferentStraight = false;
                        }
                        else
                        {
                            if (!isDifferentStraight)
                            {
                                SetPositionFinishToStraight();

                                isDifferentStraight = true;
                            }

                            MoveStraight();
                        }
                    }
                }



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

    public override void OnStun(float timeStun)
    {
        base.OnStun(timeStun);
        ChangeState(StateChar.Stun);
        StartCoroutine(WaitStun(timeStun));
    }

    IEnumerator WaitStun(float timeStun)
    {
        yield return new WaitForSeconds(timeStun);

        if (state != StateChar.Die && state != StateChar.Destroy)
        {
            ChangeState(StateChar.Run);
        }
    }

    private void MoveLeft()
    {
        Vector3 u = new Vector3(postionFinish.x, postionFinish.y, transform.position.z);

        render.SetFlip(true);
        movement.StartMove(TypeMove.Destination, new Vector3(-100000, 0, 0));
    }

    private void MoveRight()
    {
        Vector3 u = new Vector3(postionFinish.x, postionFinish.y, transform.position.z);

        //if (postionFinish.x == transform.position.x)
        //{
        //    u.x += 0.01f;
        //}

        render.SetFlip(false);

        //if (u.x < transform.position.x)
        //{
        //    render.SetFlip(true);
        //}
        //else
        //{
        //    render.SetFlip(false);
        //}

        movement.StartMove(TypeMove.Destination, new Vector3(100000, 0, 0));

        //transform.position += new Vector3(1, 0, 0) * Time.deltaTime;

        //movement.MoveTo(new Vector3(1, 0, 0) * Time.deltaTime);
    }

    private void MoveStraight()
    {
        Vector3 u = new Vector3(postionFinish.x, postionFinish.y, transform.position.z);

        //if (postionFinish.x == transform.position.x)
        //{
        //    u.x += 0.01f;
        //}

        if (u.x < transform.position.x)
        {
            render.SetFlip(true);
        }
        else
        {
            render.SetFlip(false);
        }

        movement.StartMove(TypeMove.Destination, u);

        //transform.position += new Vector3(0, -1, 0) * Time.deltaTime;

        //movement.MoveTo(new Vector3(0, - 1, 0) * Time.deltaTime);
    }

    protected override bool CheckTarget()
    {
        bool check = false;

        if (canCheck)
        {
            var enermies = CharManager.Instance.Allies;

            float minDistance = 10000000;

            for (int i = 0; i < enermies.Count; i++)
            {
                float distance = Vector3.Distance(GetBody().position, enermies[i].GetBody().position);

                var screenPos = cameraCheck.WorldToScreenPoint(GetBody().position / 15 * 5);
                var onScreen = screenPos.x > (0f + width * 0.06) && screenPos.x < (width - width * 0.06) && screenPos.y > (0f + height * 0.065) && screenPos.y < (height - height * 0.065);

                if (distance <= radiusCheck && onScreen)
                {
                    if (distance < minDistance)
                    {
                        HealthBase health = enermies[i].GetHealth();

                        if (health.GetHealth() > 0)
                        {
                            minDistance = distance;

                            if (minDistance <= rangeAttack)
                            {
                                SetTarget(enermies[i]);
                                check = true;
                            }
                        }
                    }
                }
            }

            // if (!check && LevelManagerMainGame.Instance.TypeLevel == TypeLevel.ModeSurvival)
            // {
            //     HealthBase finishHealth = LevelManagerMainGame.Instance.ObjectManager.GetMap().HealthFinish;
            //
            //     float distance = Vector3.Distance(transform.position, finishHealth.transform.position);
            //
            //     if (distance <= radiusCheck)
            //     {
            //         if (finishHealth.GetHealth() > 0)
            //         {
            //             SetTarget(finishHealth);
            //
            //             check = true;
            //         }
            //     }
            // }
        }

        return check;
    }

    public override void Update()
    {
        base.Update();
    }

    public virtual void OnFinish()
    {
        //LevelManagerMainGame.Instance.OnEndGame(GameResult.Lose);

        HealthGamePlay.Instance.SubHealth();

        ChangeState(StateChar.Destroy);
    }

    public override void OnTriggerEnterChildDetect(Collider2D collider)
    {
        base.OnTriggerEnterChildDetect(collider);

        if (charFSM.CurrentCharState == StateChar.Die || charFSM.CurrentCharState == StateChar.Destroy)
        {
            return;
        }

        if (collider.CompareTag("MoveStraight"))
        {
            isGoStraight = true;
            SetPositionFinishToStraight();
        }

        if (collider.CompareTag("Finish"))
        {
            GameManager.Instance.SoundManager.PlaySoundDestroy();

            GameManager.Instance.VibrateManager.Vibate(80);

            Instantiate(ResourceManager.Instance.Load("Effect/ZombieHitBase"), GetBody().position, Quaternion.identity);

            Instantiate(ResourceManager.Instance.Load("Effect/HitBase"), GetBody().position, Quaternion.identity);

            OnFinish();
        }
    }
}