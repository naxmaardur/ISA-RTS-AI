using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityAIControler
{
    [System.Serializable]
    public class AIBrainControler
    {
        public bool finishedDeciding { get; set; }
        public bool finishedExecutingBestAction { get; set; }

        public ControlerAction bestAction { get; set; }
        private ControlerAction LastAction;
        private ControlerAI npc;

        [SerializeField] private ControlerAction[] actionsAvailable;

        public AIBrainControler(ControlerAI armyActorBase)
        {
            npc = armyActorBase;
            finishedDeciding = false;
            finishedExecutingBestAction = false;
        }

        public void SetNPC(ControlerAI npc)
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
        public float ScoreAction(ControlerAction action)
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