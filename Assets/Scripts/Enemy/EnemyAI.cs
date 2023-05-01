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

    private bool hasLineOfSight = false;
    public bool playerSpotted = false;

    //private Tank self; 
    private TankMovement _TankMovement;
    private TankShooting _TankShooting;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject playerGO = GameObject.Find("Player");
        if(playerGO != null) player = playerGO.transform;
        agent = GetComponent<NavMeshAgent>();
        //self = gameObject.GetComponent<Tank>();
        _TankMovement = gameObject.GetComponent<TankMovement>();
        _TankShooting = gameObject.GetComponent<TankShooting>();
        agent.speed = _TankMovement.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null){
            RaycastHit hit;
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, 100f)){
                if (hit.transform.CompareTag("Player")){
                    hasLineOfSight = true;
                    playerSpotted = true;
                }else{
                    hasLineOfSight = false;
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
        _TankMovement.RotateToTarget(player.position);
        _TankShooting.Shoot();
    }
}
