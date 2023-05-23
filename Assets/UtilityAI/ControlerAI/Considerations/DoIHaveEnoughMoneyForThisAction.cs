using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAIControler;
[CreateAssetMenu(fileName = "DoIHaveEnoughMoneyForThisAction", menuName = "UtilityAI/Consideration/DoIHaveEnoughMoneyForThisAction")]
public class DoIHaveEnoughMoneyForThisAction : ControlerConsideration
{
    [SerializeField] int moneyNeeded;
    [SerializeField] UnitScritableObject unitScritableObject;
    public override float ScoreConsideration(ControlerAI npc)
    {
        if(npc.GetMoney() >= moneyNeeded)
        {
            return 1;
        }
        if(unitScritableObject != null)
        {
            if (npc.GetMoney() >= unitScritableObject.cost)
            {
                return 1;
            }
        }
        return 0;
    }

    
}
