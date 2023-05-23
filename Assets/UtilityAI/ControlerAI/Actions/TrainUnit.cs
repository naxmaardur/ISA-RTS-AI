using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;
[CreateAssetMenu(fileName = "TrainUnit", menuName = "UtilityAI/Actions/TrainUnit")]
public class TrainUnit : ControlerAction
{
    [SerializeField] int type;

    public override void Execute(ControlerAI npc)
    {
        BaseAI ai = (BaseAI)npc;
        ai.ConstructUnit(type);
    }
}
