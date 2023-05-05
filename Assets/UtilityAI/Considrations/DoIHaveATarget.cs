using System;
using System.Collections;
using System.Collections.Generic;
using UtilityAI;
using UnityEngine;

namespace UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "DoIHaveATarget", menuName = "UtilityAI/Considerations/DoIHaveATarget")]
    public class DoIHaveATarget : Consideration
    {
        [SerializeField] private AnimationCurve _responseCurve;
        [SerializeField] private bool invertResponse = false;
        public override float ScoreConsideration(ArmyActorBase npc)
        {
            UnitBase unit = (UnitBase)npc;

            if (unit.Target != null)
            {
                score = Response(invertResponse, true);
            }
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

    }
}