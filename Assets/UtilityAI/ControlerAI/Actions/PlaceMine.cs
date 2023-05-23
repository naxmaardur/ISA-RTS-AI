using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;
[CreateAssetMenu(fileName = "PlaceMine", menuName = "UtilityAI/Actions/PlaceMine")]
public class PlaceMine : ControlerAction
{
    public override void Execute(ControlerAI npc)
    {
        BaseAI ai = (BaseAI)npc;
        ai.ConstructMine();
    }

    
}
