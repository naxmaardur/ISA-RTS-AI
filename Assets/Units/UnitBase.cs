using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBase : ArmyActorBase
{
    private NavMeshAgent _navMeshAgent;
    private ArmyActorBase _target;
    public ArmyActorBase Target { get { return _target; } }
    public UnitScritableObject unitScritable;
    public Vector3 pathDestination;
    [SerializeField]
    private UnitFormation _formationParent;
    [SerializeField]
    private Transform _formationTransform;

    private List<ArmyActorBase> _actorsInRange = new();
    public List<ArmyActorBase> ActosInRange { get { return _actorsInRange; } }

    [SerializeField]
    private LayerMask ArmyActorsMask;

    bool followTarget;



    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        Grid.Instance.AddEntityToALLMaps(this);
       
        StartCoroutine(TryFindTarget());
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            if (Vector3.Distance(_target.transform.position, pathDestination) > 5) {
                MakePathToPosition(_target.transform.position);
            }
        }

        TryAttackTarget();
        SetNewGridPosition();
        if (ArmyMaster == null || !ArmyMaster.IsPlayer)
        {
            aIBrain.DecideBestAction();
        }
        
        if(_target != null && followTarget)
        {
            if(Vector3.Distance(pathDestination,_target.transform.position) > 3)
            {
                MakePathToPosition(_target.transform.position);
                pathDestination = _target.transform.position;
            }
        }

    }

    //sets the units prefed transform position to the given transform
    public void SetTransformDestination(Transform transform)
    {
        _formationTransform = transform;
    }

    public void SetFormation(UnitFormation unitFormation)
    {
        _formationParent = unitFormation;
    }

    private void TryAttackTarget()
    {
        if(_target == null) { return; }
        if(Vector3.Distance(transform.position,_target.transform.position) < unitScritable.range)
        {
            if (followTarget)
            {
                _navMeshAgent.isStopped = true;
            }
            _target.DealDamage(unitScritable.attack);// temp code
        }
        else
        {
            if(!followTarget)
            {
                _target = null;
            }
            _navMeshAgent.isStopped = false;
        }
    }


    public void SetArmy(ArmyMaster armyMaster)
    {
        _army = armyMaster;
        if (armyMaster.IsPlayer)
        {
            influenceValue = -influenceValue;
            armyInfluenceValue = -armyInfluenceValue;
        }
        else
        {
            aIBrain.DecideBestAction();
        }
        //Grid.Instance.AddEntityToALLMaps(this);
    }




    public void GiveOrder(GameObject obj, Vector3 position)
    {

        if(obj.layer == 3)
        {
            pathDestination = position;
            MakePathToPosition(position);
            _target = null;
            followTarget = false;
        }
        else
        {
            ArmyActorBase armyActor;
            if (obj.TryGetComponent<ArmyActorBase>(out armyActor))
            {
                if(armyActor.ArmyMaster == ArmyMaster) { MakePathToPosition(transform.position); _target = null; return; }
                TargetEnemy(armyActor);
                pathDestination = position;
                MakePathToPosition(armyActor.transform.position);
                followTarget = true;
                return;
            }
            MakePathToPosition(position);
            pathDestination = position;
        }
    }


    public void TargetEnemy(ArmyActorBase actor)
    {
        
        _target = actor;
        actor.Ondeath += TargetDeath;
    }

    public void MakePathToPosition(Vector3 position)
    {
        //pathDestination = position;
        _navMeshAgent.SetDestination(position);
        _navMeshAgent.isStopped = false;
    }

    public Vector3 GetDestination()
    {
        if(pathDestination != Vector3.zero)
        {
            return pathDestination;
        }
        if(_formationParent != null)
        {
            return _formationTransform.position;
        }



        return transform.position;
    }

    //this is a vairly heavy function so we don't want to do this often 
    void GetAllEnemiesInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, unitScritable.range, ArmyActorsMask,QueryTriggerInteraction.Ignore);

        List<ArmyActorBase> armyActors = new();
        foreach(Collider collider in colliders)
        {
            if(Physics.Raycast(transform.position, UtiltyFunctions.Vector3Direction(transform.position, collider.transform.position), Vector3.Distance(collider.transform.position, transform.position), 1 << 31))
            {
                //don't add it if there is a obstical in the way
                continue;
            }
            ArmyActorBase armyActor;
            collider.TryGetComponent<ArmyActorBase>(out armyActor);
            if(armyActor == null) { continue; }
            if(armyActor.ArmyMaster == ArmyMaster) { continue; }
            armyActors.Add(armyActor);
        }
        if(armyActors.Count == 0) { return; }

        if(armyActors.Count < 2)
        {
            TargetEnemy(armyActors[0]);
            return;
        }

        //if it is a player unity target a random enemy 
        //they need to target the correct enemy types them selfs, this is just so that a unit continues to attack in a fight
        if (ArmyMaster.IsPlayer)
        {
            TargetEnemy(armyActors[UnityEngine.Random.Range(0, armyActors.Count)]);
            return;
        }

        //find the best actor to be targeting ,lowest health enemy that they are strong against,
        //if there are none then target the nutrual(same type) and then the unit they are weak against
        //and then buildings
        float importance = -300;
        ArmyActorBase actor = null;
        
        foreach(ArmyActorBase armyActor1 in armyActors)
        {
            //determin the importance of the actor
            float imp = 0;
            UnitBase unitBase = armyActor1 as UnitBase;
            if(unitBase != null)
            {
                //unity type evaluation
                int enemyType = unitBase.unitScritable.type;
                int ourType = unitScritable.type;

                int differance = enemyType - ourType;
                switch (differance)
                {
                    case 0:
                        //we are equal 
                        imp += 50;
                        break;
                    case -1:
                        //we are weak
                        imp += 10;
                        break;
                    case 1:
                        //we are strong
                        imp += 100;
                        break;
                    case -2:
                        //we are strong
                        imp += 100;
                        break;
                    case 2:
                        //we are weak
                        imp += 10;
                        break;
                }
            }

            //health evaluation
            float healthLevel = armyActor1.Health / armyActor1.MaxHealth;
            float mod = 1 - healthLevel;
            imp += 10 * mod;

            //set data
            if(imp > importance)
            {
                importance = imp;
                actor = armyActor1;
            }
        }

        //set target
        TargetEnemy(actor);
    }



    public IEnumerator ExecuteMoveTo()
    {
        MakePathToPosition(GetDestination());
        followTarget = false;
        yield return null;
    }

    public IEnumerator FollowTarget()
    {
        //MakePathToPosition(GetDestination());
        followTarget = true;
        yield return null;
    }

    public IEnumerator FleeArea()
    {
        Vector3 testFleePoint = new Vector3(173, 0, 168);
        MakePathToPosition(testFleePoint);
        followTarget = false;
        yield return null;
    }

    private IEnumerator TryFindTarget()
    {
        while (true)
        {

            if(_target == null)
            {
                GetAllEnemiesInRange();
            }
            yield return new WaitForSeconds(2);
        }
    }

    private void TargetDeath()
    {
        _target = null;
        GetAllEnemiesInRange();
    }

    private void OnDestroy()
    {
        if (_formationParent != null) { _formationParent.RemoveUnit(this); }
        Grid.Instance.RemoveEntityFromALLMaps(this);
        Ondeath?.Invoke();
    }


}
