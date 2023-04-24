using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange;
    private bool playerInSightRange;
    private bool playerSpotted = false, hasLineOfSight = false;

    private Tank self; 
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        self = gameObject.GetComponent<Tank>();
    }

    // Update is called once per frame
    void Update()
    {
        // playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        // playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // if(!playerInSightRange && !playerInAttackRange) Patroling();
        // if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        // if(playerInSightRange && playerInAttackRange) ShootPlayer();
        if(player != null){
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        
            if (playerInSightRange){
                RaycastHit hit;
                Vector3 directionToPlayer = (player.position - transform.position).normalized;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, sightRange)){
                    if (hit.transform.CompareTag("Player")){
                        hasLineOfSight = true;
                        playerSpotted = true;
                    }else{
                        hasLineOfSight = false;
                    }
                }
            }

            if ((!hasLineOfSight && !playerSpotted) ){
                Patroling();
            }else if(hasLineOfSight){
                ShootPlayer();
            }else if(playerSpotted){
                ChasePlayer();
            }
        }else Patroling();
        
        
    }

    private void Patroling(){
        if(!walkPointSet) SearchWalkPoint();
        else agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    private void SearchWalkPoint(){
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet =  true;
        
    }
    
    private void ChasePlayer(){
        agent.SetDestination(player.position);
    }

    private void ShootPlayer(){
        agent.SetDestination(transform.position);
        self.rotateToTarget(player.position);
        self.Shoot();
    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }
}
