using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAI;


namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "FollowEnemy", menuName = "UtilityAI/Actions/FollowEnemy")]
    public class FollowEnemy : Action
    {
        public override void Execute(ArmyActorBase npc)
        {
            UnitBase unit = (UnitBase)npc;
            unit.StartCoroutine(unit.FollowTarget());
        }
    }
}