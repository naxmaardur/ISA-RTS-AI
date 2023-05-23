using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;
[CreateAssetMenu(fileName = "IsTheQueueShortEnough", menuName = "UtilityAI/Consideration/IsTheQueueShortEnough")]
public class IsTheQueueShortEnough : ControlerConsideration
{
    [SerializeField] private AnimationCurve _responseCurve;
    [SerializeField] private int unitType = 0;
    public override float ScoreConsideration(ControlerAI npc)
    {
        int unitCount = npc.GetShortestQueueByType(unitType);

        unitCount = Mathf.Clamp(unitCount,0, 25);

        return _responseCurve.Evaluate(UtiltyFunctions.Remap(unitCount, 0, 25, 0, 1));
    }
}