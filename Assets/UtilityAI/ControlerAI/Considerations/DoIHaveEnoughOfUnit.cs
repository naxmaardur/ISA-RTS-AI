using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;
[CreateAssetMenu(fileName = "DoIHaveEnoughOfUnit", menuName = "UtilityAI/Consideration/DoIHaveEnoughOfUnit")]
public class DoIHaveEnoughOfUnit : ControlerConsideration
{
    [SerializeField] private AnimationCurve _responseCurve;
    [SerializeField] private int unitType = 0;
    [SerializeField] private int strongAgainst = 0;
    public override float ScoreConsideration(ControlerAI npc)
    {
        int unitCount = npc.GetUnitCountByType(unitType);
        unitCount += npc.GetUnitsInconstruction(unitType);
        int unitCountStrong = npc.GetEnemyUnitCountByType(strongAgainst);
        unitCountStrong += npc.GetEnemyUnitsInconstruction(strongAgainst);
        if(unitCountStrong == 0)
        {
            unitCountStrong = 1;
        }
        float ComparativePercentage = unitCount/unitCountStrong;
        ComparativePercentage = Mathf.Clamp(ComparativePercentage, 0, 3);
        return _responseCurve.Evaluate(UtiltyFunctions.Remap(ComparativePercentage, 0, 3, 0, 1));
    }
}
