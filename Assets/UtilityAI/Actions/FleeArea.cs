using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAI;


namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "FleeArea", menuName = "UtilityAI/Actions/FleeArea")]
    public class FleeArea : Action
    {
        public override void Execute(ArmyActorBase npc)
        {
            UnitBase unit = (UnitBase)npc;
            unit.StartCoroutine(unit.FleeArea());
        }
    }
}