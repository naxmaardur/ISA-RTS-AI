using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBase : ArmyActorBase
{
    private NavMeshAgent _navMeshAgent;
    private ArmyActorBase _target;
    public UnitScritableObject unitScritable;
    public Vector3 pathDestination;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_target != null)
        {
            if (Vector3.Distance(_target.transform.position, pathDestination) > 5) {
                MakePathToPosition(_target.transform.position);
            }
        }

        TryAttackTarget();
    }

    private void TryAttackTarget()
    {
        if(_target == null) { return; }
        if(Vector3.Distance(transform.position,_target.transform.position) < unitScritable.range)
        {
            _navMeshAgent.isStopped = true;
            _target.DealDamage(unitScritable.attack);// temp code
        }
        else
        {
            _navMeshAgent.isStopped = false;
        }
    }


    public void SetArmy(ArmyMaster armyMaster)
    {
        _army = armyMaster;
    }




    public void GiveOrder(GameObject obj, Vector3 position)
    {

        if(obj.layer == 3)
        {
            MakePathToPosition(position);
            _target = null;
        }
        else
        {
            ArmyActorBase armyActor;
            if (obj.TryGetComponent<ArmyActorBase>(out armyActor))
            {
                if(armyActor.ArmyMaster == ArmyMaster) { MakePathToPosition(transform.position); _target = null; return; }
                TargetEnemy(armyActor);
                MakePathToPosition(armyActor.transform.position);
                return;
            }
            MakePathToPosition(position);
        }
    }


    public void TargetEnemy(ArmyActorBase actor)
    {
        
        _target = actor;
    }

    public void MakePathToPosition(Vector3 position)
    {
        pathDestination = position;
        _navMeshAgent.SetDestination(position);
    }




}
