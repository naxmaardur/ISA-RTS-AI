using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;
[CreateAssetMenu(fileName = "DoIHaveEnoungMines", menuName = "UtilityAI/Consideration/DoIHaveEnoungMines")]
public class DoIHaveEnoungMines : ControlerConsideration
{
    [SerializeField] private AnimationCurve _responseCurve;
    [SerializeField] private bool invertResponse = false;
    public override float ScoreConsideration(ControlerAI npc)
    {
        int mines = npc.GetMineCount();
        float reponse = _responseCurve.Evaluate(UtiltyFunctions.Remap(mines, 0, 8, 0, 1));
        if (invertResponse)
        {
            reponse = 1 - reponse;
        }
        return reponse;
    }
}
