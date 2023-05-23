using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;
[CreateAssetMenu(fileName = "FallbackConsideration", menuName = "UtilityAI/Consideration/FallbackConsideration")]
public class FallbackConsideration : ControlerConsideration
{
    public override float ScoreConsideration(ControlerAI npc)
    {
        return 0;
    }
}