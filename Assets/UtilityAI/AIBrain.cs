using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityAI
{
    [System.Serializable]
    public class AIBrain
    {
        public bool finishedDeciding { get; set; }
        public bool finishedExecutingBestAction { get; set; }

        public Action bestAction { get; set; }
        private Action LastAction;
        private ArmyActorBase npc;

        [SerializeField] private Action[] actionsAvailable;

        public AIBrain(ArmyActorBase armyActorBase)
        {
            npc = armyActorBase;
            finishedDeciding = false;
            finishedExecutingBestAction = false;
        }

        public void SetNPC(ArmyActorBase npc)
        {
            this.npc = npc;
        }

        // Loop through all the available actions 
        // Give me the highest scoring action
        public void DecideBestAction()
        {
            finishedExecutingBestAction = false;

            float score = 0f;
            int nextBestActionIndex = 0;
            for (int i = 0; i < actionsAvailable.Length; i++)
            {
                if (ScoreAction(actionsAvailable[i]) > score)
                {
                    nextBestActionIndex = i;
                    score = actionsAvailable[i].score;
                }
            }

            bestAction = actionsAvailable[nextBestActionIndex];
            if(bestAction == LastAction) { finishedDeciding = true; return; }
            bestAction.SetRequiredDestination(npc);
            bestAction.Execute(npc);

            finishedDeciding = true;
        }

        // Loop through all the considerations of the action
        // Score all the considerations
        // Average the consideration scores ==> overall action score
        public float ScoreAction(Action action)
        {
            float score = 1f;
            for (int i = 0; i < action.considerations.Length; i++)
            {
                float considerationScore = action.considerations[i].ScoreConsideration(npc);
                score *= considerationScore;
                if (score == 0)
                {
                    action.score = 0;
                    return action.score; // No point computing further
                }
            }

            // Averaging scheme of overall score
            float originalScore = score;
            float modFactor = 1 - (1 / action.considerations.Length);
            float makeupValue = (1 - originalScore) * modFactor;
            action.score = originalScore + (makeupValue * originalScore);

            return action.score;
        }


    }
}