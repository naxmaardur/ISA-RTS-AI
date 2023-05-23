using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;
[CreateAssetMenu(fileName = "DoIHaveEnoughCatapultCamps", menuName = "UtilityAI/Consideration/DoIHaveEnoughCatapultCamps")]
public class DoIHaveEnoughCatapultCamps : ControlerConsideration
{
    [SerializeField] private AnimationCurve _responseCurve;
    public override float ScoreConsideration(ControlerAI npc)
    {
        int camp = npc.GetCampCount(2);
        return _responseCurve.Evaluate(UtiltyFunctions.Remap(camp, 0, 0, 3, 1));
    }
}
