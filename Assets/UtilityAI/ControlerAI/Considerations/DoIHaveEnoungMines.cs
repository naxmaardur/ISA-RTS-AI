using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;
[CreateAssetMenu(fileName = "DoIHaveEnoungMines", menuName = "UtilityAI/Consideration/DoIHaveEnoungMines")]
public class DoIHaveEnoungMines : ControlerConsideration
{
    [SerializeField] private AnimationCurve _responseCurve;
    public override float ScoreConsideration(ControlerAI npc)
    {
        int mines = npc.GetMineCount();
        return _responseCurve.Evaluate(UtiltyFunctions.Remap(mines, 0, 0, 8, 1));
    }
}
