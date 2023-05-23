using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;
[CreateAssetMenu(fileName = "DoIHaveEnoughCatapults", menuName = "UtilityAI/Consideration/DoIHaveEnoughCatapults")]
public class DoIHaveEnoughCatapults : ControlerConsideration
{
    [SerializeField] private AnimationCurve _responseCurve;
    public override float ScoreConsideration(ControlerAI npc)
    {
        int unitCount = npc.GetEnemyUnitCountByType(2);
        int unitCountStrong = npc.GetEnemyUnitCountByType(0);

        float ComparativePercentage = unitCount / unitCountStrong;
        ComparativePercentage = Mathf.Clamp(ComparativePercentage, 0, 3);
        return _responseCurve.Evaluate(UtiltyFunctions.Remap(ComparativePercentage, 0, 0, 3, 1));
    }
}
