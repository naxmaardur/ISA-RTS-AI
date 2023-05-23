using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;

[CreateAssetMenu(fileName = "DoIHaveEnoughInfantryCamps", menuName = "UtilityAI/Consideration/DoIHaveEnoughInfantryCamps")]
public class DoIHaveEnoughInfantryCamps : ControlerConsideration
{
    [SerializeField] private AnimationCurve _responseCurve;
    public override float ScoreConsideration(ControlerAI npc)
    {
        int camp = npc.GetCampCount(0);
        return _responseCurve.Evaluate(UtiltyFunctions.Remap(camp, 0, 0, 3, 1));
    }
}
