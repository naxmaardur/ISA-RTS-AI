using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;
[CreateAssetMenu(fileName = "PlaceBalistaCamp", menuName = "UtilityAI/Actions/PlaceBalistaCamp")]
public class PlaceCamp : ControlerAction
{
    [SerializeField] int type = 0;
    public override void Execute(ControlerAI npc)
    {
        BaseAI ai = (BaseAI)npc;
        ai.ConstructCamp(type);
    }
}
