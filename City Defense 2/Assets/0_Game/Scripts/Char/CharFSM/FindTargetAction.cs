using Common.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharStateFSM
{
    public class FindTargetAction : FSMAction
    {
        private readonly CharacterBase characterBase;

        public FindTargetAction(CharacterBase _characterBase, FSMState owner) : base(owner)
        {
            characterBase = _characterBase;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            characterBase.OnEnterState(StateChar.FindTarget);
        }

        public override void OnExit()
        {
            base.OnExit();

            characterBase.OnExitState(StateChar.FindTarget);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            characterBase.OnUpdateState(StateChar.FindTarget);
        }
    }
}


