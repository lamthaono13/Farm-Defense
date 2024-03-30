using Common.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharStateFSM
{
    public class DestroyAction : FSMAction
    {
        private readonly CharacterBase characterBase;

        public DestroyAction(CharacterBase _characterBase, FSMState owner) : base(owner)
        {
            characterBase = _characterBase;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            characterBase.OnEnterState(StateChar.Destroy);
        }

        public override void OnExit()
        {
            base.OnExit();

            characterBase.OnExitState(StateChar.Destroy);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            characterBase.OnUpdateState(StateChar.Destroy);
        }
    }
}


