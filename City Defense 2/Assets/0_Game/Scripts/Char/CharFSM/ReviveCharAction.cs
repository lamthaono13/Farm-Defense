using Common.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharStateFSM
{
    public class ReviveCharAction : FSMAction
    {
        private readonly CharacterBase characterBase;

        public ReviveCharAction(CharacterBase _characterBase, FSMState owner) : base(owner)
        {
            characterBase = _characterBase;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            characterBase.OnEnterState(StateChar.Revive);
        }

        public override void OnExit()
        {
            base.OnExit();

            characterBase.OnExitState(StateChar.Revive);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            characterBase.OnUpdateState(StateChar.Revive);
        }
    }
}


