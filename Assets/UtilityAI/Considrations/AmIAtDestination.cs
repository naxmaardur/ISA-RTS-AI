using System;
using System.Collections;
using System.Collections.Generic;
using UtilityAI;
using UnityEngine;

namespace UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "AmIAt_Destination", menuName = "UtilityAI/Considerations/Am I At Destination")]
    public class AmIAtDestination : Consideration
    {
        [SerializeField] private AnimationCurve _responseCurve;
        //[SerializeField] private DestinationType destinationType;
        [SerializeField] private bool invertResponse = false;

        public override float ScoreConsideration(ArmyActorBase npc)
        {
            // yes I am at destination, return default value of true
            if (GetDistanceFromDestination(npc) <= 5)
            {
                score = Response(invertResponse, true);
            }

            // no I'm not at destination, return default value of false
            else
            {
                score = Response(invertResponse, false);
            }
            return score;
        }

        private float Response(bool invertResponse, bool defaultValue)
        {
            if (invertResponse)
            {
                return Convert.ToInt32(!defaultValue);
            }
            return Convert.ToInt32(defaultValue);
        }

        private float GetDistanceFromDestination(ArmyActorBase npc)
        {
            UnitBase unit = (UnitBase)npc;
            return Vector3.Distance(npc.transform.position, unit.GetDestination());
        }
    }
}