using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;
[CreateAssetMenu(fileName = "DoIHaveEnoughBalistaCamps", menuName = "UtilityAI/Consideration/DoIHaveEnoughBalistaCamps")]
public class DoIHaveEnoughBalistaCamps : ControlerConsideration
{
    [SerializeField] private AnimationCurve _responseCurve;
    public override float ScoreConsideration(ControlerAI npc)
    {
        int camp = npc.GetCampCount(1);
        return _responseCurve.Evaluate(UtiltyFunctions.Remap(camp, 0, 0, 3, 1));
    }
}
