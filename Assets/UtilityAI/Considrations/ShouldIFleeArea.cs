using System;
using System.Collections;
using System.Collections.Generic;
using UtilityAI;
using UnityEngine;

namespace UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "ShouldIFleeArea", menuName = "UtilityAI/Considerations/Should I Flee Area")]
    public class ShouldIFleeArea : Consideration
    {
        [SerializeField] private AnimationCurve _responseCurve;
        [SerializeField] private bool _invertResponse;

        public override float ScoreConsideration(ArmyActorBase npc)
        {
            //get node area actor is in
            NodeArea Currentarea = Grid.Instance.GridArray[npc.GridPosition.x, npc.GridPosition.y].NodeArea;
            float highestValue = 0;
            float LowestValue = 0;
            foreach (NodeArea nodeArea in Currentarea.Neigbors)
            {
                if (nodeArea.ArmyStrength > highestValue) { highestValue = nodeArea.ArmyStrength; }
                if (nodeArea.ArmyStrength < LowestValue) { LowestValue = nodeArea.ArmyStrength; }
            }
            if (Mathf.Abs(LowestValue) < highestValue)
            {
                if (_invertResponse) { 
                    score = 1;
                }
                else
                {
                    score = 0;
                }
                return score;
            }
            if (highestValue == 0)
            {
                if (_invertResponse)
                {
                    score = 0;
                }
                else
                {
                    score = 1;
                }
                return score;
            }

            if(MathF.Abs(LowestValue) < 0.1f)
            {
                LowestValue = 0.1f;
            }
            if(highestValue < 0.1f)
            {
                highestValue = 0.1f;
            }

            float value =  Mathf.Abs(LowestValue) / highestValue;

            value = value / 3;


            score = _responseCurve.Evaluate(value);
            if (_invertResponse)
            {
                value = 1 - value;
            }
            return score;
        }
    }
}