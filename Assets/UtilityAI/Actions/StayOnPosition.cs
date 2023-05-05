using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAI;


namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "StayOnPosition", menuName = "UtilityAI/Actions/StayOnPosition")]
    //a default fall back action for if there are no good actions
    public class StayOnPosition: Action
    {
        public override void Execute(ArmyActorBase npc)
        {
            UnitBase unit = (UnitBase)npc;
            unit.StartCoroutine(unit.StayOnPostion());
        }

        public override void SetRequiredDestination(ArmyActorBase npc)
        {
        }
    }
}