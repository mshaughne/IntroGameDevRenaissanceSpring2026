using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;

    enum EnemyStates { Idle, Patrol, Chase }
    EnemyStates currentState;
    [SerializeField] float chaseRange = 10f,
        patrolDistance = 10f, idleTime = 3f,
        idleTimer, stopChaseRange = 15f;

    private Vector3 patrolTarget;
    private bool isPatrolTargetSet = false;

    public LayerMask groundLayers;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        currentState = EnemyStates.Idle;
        idleTimer = idleTime;
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(target.position);

        switch(currentState)
        {
            case EnemyStates.Idle:
                IdleState();
                break;
            case EnemyStates.Patrol:
                PatrolState();
                break;
            case EnemyStates.Chase:
                ChaseState();
                break;
            default:
                Debug.LogError("ENEMY STATE INVALID");
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
    
    void IdleState()
    {
        Debug.Log("Idle State");

        idleTimer -= Time.deltaTime;

        if(idleTimer <= 0)
        {
            currentState = EnemyStates.Patrol;
            idleTimer = idleTime;
        }

        if(Vector3.Distance(transform.position, target.position) < chaseRange)
        {
            currentState = EnemyStates.Chase;
            idleTimer = idleTime;
        }
    }

    void PatrolState()
    {
        Debug.Log("Patrol State");

        if(!isPatrolTargetSet) FindPatrolTarget();
        if(isPatrolTargetSet) agent.SetDestination(patrolTarget);

        Vector3 distanceToPatrol = transform.position - patrolTarget;
        if (distanceToPatrol.magnitude <= 1f)
        {
            isPatrolTargetSet = false;
            currentState = EnemyStates.Idle;
        }

        if (Vector3.Distance(transform.position, target.position) < chaseRange)
        {
            isPatrolTargetSet = false;
            currentState = EnemyStates.Chase;
        }
    }

    void ChaseState()
    {
        Debug.Log("Chase State");

        agent.SetDestination(target.position);

        float distance = Vector3.Distance(transform.position, target.position);

        if(distance > stopChaseRange)
        {
            currentState = EnemyStates.Idle;
        }
    }

    private void FindPatrolTarget()
    {
        float randomX = Random.Range(-patrolDistance, patrolDistance);
        float randomZ = Random.Range(-patrolDistance, patrolDistance);

        patrolTarget = new Vector3(transform.position.x + randomX,
            transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(patrolTarget, Vector3.down, 2f, groundLayers))
        {
            isPatrolTargetSet = true;
        }
    }
}
