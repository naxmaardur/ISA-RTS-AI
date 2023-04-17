using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAI;


namespace UtilityAI.Actions
{
    [CreateAssetMenu(fileName = "MoveToLocation", menuName = "UtilityAI/Actions/MoveToLocation")]
    public class MoveToLocation : Action
    {
        public override void Execute(ArmyActorBase npc)
        {
            //npc.DoWork(3);
        }

        public override void SetRequiredDestination(ArmyActorBase npc)
        {
            float distance = Mathf.Infinity;
            Transform nearestResource = null;

            /*List<Transform> resources = npc.context.Destinations[DestinationType.resource];
            foreach (Transform resource in resources)
            {
                float distanceFromResource = Vector3.Distance(resource.position, npc.transform.position);
                if (distanceFromResource < distance)
                {
                    nearestResource = resource;
                    distance = distanceFromResource;
                }
            }*/

            RequiredDestination = nearestResource;
            //npc.mover.destination = RequiredDestination;
        }
    }
}