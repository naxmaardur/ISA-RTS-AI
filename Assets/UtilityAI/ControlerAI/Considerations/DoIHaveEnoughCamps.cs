using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;
[CreateAssetMenu(fileName = "DoIHaveEnoughCamps", menuName = "UtilityAI/Consideration/DoIHaveEnoughCamps")]
public class DoIHaveEnoughCamps : ControlerConsideration
{
    [SerializeField] private AnimationCurve _responseCurve;
    [SerializeField] private bool invertResponse = false;
    [SerializeField] private int type = 0;
    public override float ScoreConsideration(ControlerAI npc)
    {
        int camp = npc.GetCampCount(type);
        float responce = _responseCurve.Evaluate(UtiltyFunctions.Remap(camp, 0, 0, 3, 1));
        if (invertResponse)
        {
            responce = 1 - responce;
        }
        return responce;
    }
}
