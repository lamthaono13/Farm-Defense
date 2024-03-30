using Common.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharStateFSM
{
    public class IdleAction : FSMAction
    {
        private readonly CharacterBase characterBase;

        public IdleAction(CharacterBase _characterBase, FSMState owner) : base(owner)
        {
            characterBase = _characterBase;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            characterBase.OnEnterState(StateChar.Idle);
        }

        public override void OnExit()
        {
            base.OnExit();

            characterBase.OnExitState(StateChar.Idle);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            characterBase.OnUpdateState(StateChar.Idle);
        }
    }
}


