using System;
using System.Collections;
using System.Collections.Generic;
using UtilityAI;
using UnityEngine;

namespace UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "ShouldIAttackEnemy", menuName = "UtilityAI/Considerations/Am I At Destination")]
    public class ShouldIAttackEnemy : Consideration
    {
        [SerializeField] private AnimationCurve _responseCurve;

        public override float ScoreConsideration(ArmyActorBase npc)
        {
            UnitBase unit = (UnitBase)npc;
            UnitScritableObject unitObject = unit.unitScritable;


            


            //get node area actor is in
            NodeArea Currentarea = Grid.Instance.GridArray[npc.GridPosition.x, npc.GridPosition.y].NodeArea;
            float highestValue = Currentarea.ArmyStrength;
            float LowestValue = 0;
            foreach (NodeArea nodeArea in Currentarea.Neigbors)
            {
                if (nodeArea.ArmyStrength > highestValue) { highestValue = nodeArea.ArmyStrength; }
                if (nodeArea.ArmyStrength < LowestValue) { LowestValue = nodeArea.ArmyStrength; }
            }
            if (Mathf.Abs(LowestValue) < highestValue)
            {
                score = 0;
                return score;
            }

            float value = Mathf.Abs(LowestValue) / highestValue;


            score = _responseCurve.Evaluate(value);
            return score;
        }
    }
}