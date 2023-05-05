using System;
using System.Collections;
using System.Collections.Generic;
using UtilityAI;
using UnityEngine;

namespace UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "NoConsiderations", menuName = "UtilityAI/Considerations/NoConsiderations")]
    public class NoConsiderations : Consideration
    {
        public override float ScoreConsideration(ArmyActorBase npc)
        {
            return score;
        }
       
    }
}