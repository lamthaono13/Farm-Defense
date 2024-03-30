using Common.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharStateFSM
{
    public class DieAction : FSMAction
    {
        private readonly CharacterBase characterBase;

        public DieAction(CharacterBase _characterBase, FSMState owner) : base(owner)
        {
            characterBase = _characterBase;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            characterBase.OnEnterState(StateChar.Die);
        }

        public override void OnExit()
        {
            base.OnExit();

            characterBase.OnExitState(StateChar.Die);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            characterBase.OnUpdateState(StateChar.Die);
        }
    }
}


