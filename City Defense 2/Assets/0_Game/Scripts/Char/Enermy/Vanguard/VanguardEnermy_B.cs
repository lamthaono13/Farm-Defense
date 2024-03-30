using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanguardEnermy_B : VanguardEnermy
{
    [SerializeField] private ParticleSystem particleSystemSkill;

    public override void OnEnterState(StateChar stateChar)
    {
        base.OnEnterState(stateChar);

        switch (stateChar)
        {
            case StateChar.Skill:

                movement.ActiveMovement(false);

                render.Animation.Animate(TypeAnimation.Run, 1, true, null, null);

                particleSystemSkill.Play();

                break;
        }
    }

    public override void OnUpdateState(StateChar stateChar)
    {
        base.OnUpdateState(stateChar);

        switch (stateChar)
        {
            case StateChar.Skill:

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

                    movement.MoveStraight();
                }
                else
                {
                    if (transform.position.x < xToMoveRightRun)
                    {
                        movement.MoveRight();

                        isDifferentStraight = false;
                    }
                    else
                    {
                        if (transform.position.x > xToMoveLeftRun)
                        {
                            movement.MoveLeft();

                            isDifferentStraight = false;
                        }
                        else
                        {
                            if (!isDifferentStraight)
                            {
                                SetPositionFinishToStraight();

                                isDifferentStraight = true;
                            }

                            movement.MoveStraight();
                        }
                    }
                }

                break;
        }
    }

    public override void AcitionBeforeDie()
    {
        base.AcitionBeforeDie();

        particleSystemSkill.gameObject.SetActive(false);
    }

    public void OnChangeToSkill()
    {
        ChangeState(StateChar.Skill);
    }
}
