using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;
[CreateAssetMenu(fileName = "DoIHaveEnoughBalistas", menuName = "UtilityAI/Consideration/DoIHaveEnoughBalistas")]
public class DoIHaveEnoughBalistas : ControlerConsideration
{
    [SerializeField] private AnimationCurve _responseCurve;
    public override float ScoreConsideration(ControlerAI npc)
    {
        int unitCount = npc.GetEnemyUnitCountByType(1);
        int unitCountStrong = npc.GetEnemyUnitCountByType(2);

        float ComparativePercentage = unitCount/unitCountStrong;
        ComparativePercentage = Mathf.Clamp(ComparativePercentage, 0, 3);
        return _responseCurve.Evaluate(UtiltyFunctions.Remap(ComparativePercentage, 0, 0, 3, 1));
    }
}
