using Common.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharStateFSM
{
    public class SkillAction : FSMAction
    {
        private readonly CharacterBase characterBase;

        public SkillAction(CharacterBase _characterBase, FSMState owner) : base(owner)
        {
            characterBase = _characterBase;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            characterBase.OnEnterState(StateChar.Skill);
        }

        public override void OnExit()
        {
            base.OnExit();

            characterBase.OnExitState(StateChar.Skill);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            characterBase.OnUpdateState(StateChar.Skill);
        }
    }
}
