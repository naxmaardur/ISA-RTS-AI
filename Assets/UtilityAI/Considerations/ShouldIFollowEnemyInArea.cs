using System;
using System.Collections;
using System.Collections.Generic;
using UtilityAI;
using UnityEngine;

namespace UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "ShouldIFollowEnemyInArea", menuName = "UtilityAI/Considerations/Should I Follow Enemy In Area")]
    public class ShouldIFollowEnemyInArea : Consideration
    {
        [SerializeField] private AnimationCurve _responseCurve;
        [SerializeField] private bool _invertResponse;
        public override float ScoreConsideration(ArmyActorBase npc)
        {
            //get node area actor is in
            NodeArea Currentarea = Grid.Instance.GridArray[npc.GridPosition.x, npc.GridPosition.y].NodeArea;
            if(Currentarea.ArmyStrength < 0)
            {
                if (_invertResponse)
                {
                    score = 1;
                }
                else
                {
                    score = 0;
                }
                return score;
            }
            float highestValue = Currentarea.ArmyStrength;
            float LowestValue = 0;
            foreach(NodeArea nodeArea in Currentarea.Neigbors)
            {
                if(nodeArea.ArmyStrength > highestValue) { highestValue = nodeArea.ArmyStrength; }
                if(nodeArea.ArmyStrength < LowestValue) { LowestValue = nodeArea.ArmyStrength; }
            }
            if (LowestValue == 0)
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
            if(Mathf.Abs(LowestValue) > highestValue)
            {
                if (_invertResponse)
                {
                    score = 1;
                }
                else
                {
                    score = 0;
                }
                return score;
            }

            float value = highestValue/Mathf.Abs(LowestValue);

            value = value / 3;

            score = _responseCurve.Evaluate(value);
            if (_invertResponse)
            {
                score = 1 - score;
            }
            return score;
        }
       
    }
}