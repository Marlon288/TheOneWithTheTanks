using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    
    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer, whatIsNotHoles;

    //Patroling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    private bool hasLineOfSight = false;
    public bool playerSpotted = false;

    private float timeSinceLastMove = 0f;
    private Vector3 lastPosition;
    private GameObject firePoint;

    //private Tank self; 
    private TankMovement _TankMovement;
    private TankShooting _TankShooting;

    private bool cooldownActive;
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
        firePoint = this.gameObject.transform.GetChild(1).gameObject;
        cooldownActive = true;
        //Wait two seconds until the enemy is able to shoot.
        StartCoroutine(CooldownCoroutine());

    }

    // Update is called once per frame
    void Update()
    {
        if(player != null){
            
            RaycastHit hit;
            Vector3 directionToPlayer = (player.position - firePoint.transform.position).normalized;
            if (Physics.Raycast(firePoint.transform.position, directionToPlayer, out hit, 100f, whatIsNotHoles)){
                if (hit.transform.CompareTag("Player")){
                    hasLineOfSight = true;
                    playerSpotted = true;
                }else{
                    hasLineOfSight = false;
                }
            }

            if ((!hasLineOfSight && !playerSpotted)){
                Patroling();
            }else if(hasLineOfSight){
                ShootPlayer();
            }else if(playerSpotted){
                ChasePlayer();
            }
        }else Patroling();
        //If the AI gets stuck for 2 seconds, it will calculate a new walkPoint after 
        if (Vector3.Distance(transform.position, lastPosition) < 0.2f){
            timeSinceLastMove += Time.deltaTime;

            if (timeSinceLastMove >= 2f){
                walkPointSet = false; // Reset the walk point so a new one will be searched
                timeSinceLastMove = 0f;
            }
        }else
        {
            timeSinceLastMove = 0f;
        }

        lastPosition = transform.position;
        
        
    }

    IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(2f);
        cooldownActive = false;
    }

    private void Patroling(){
        if(!walkPointSet) SearchWalkPoint();
        else agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    private void SearchWalkPoint(){
        float maxHeightDifference = 1f; 
        // If the walkpoint is set on top of an obstacle, then calculate a new one

        for (int i = 0; i < 100; i++) // Try up to 100 times to find a valid walk point
        {
            float randomAngle = Random.Range(0f, 360f);
            float randomDistance = Random.Range(0f, walkPointRange);

            Vector3 direction = new Vector3(Mathf.Sin(randomAngle), 0, Mathf.Cos(randomAngle));
            Vector3 potentialWalkPoint = transform.position + direction * randomDistance;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(potentialWalkPoint, out hit, walkPointRange, NavMesh.AllAreas))
            {
                // Check the height difference
                float heightDifference = Mathf.Abs(hit.position.y - transform.position.y);
                if (heightDifference <= maxHeightDifference)
                {
                    walkPoint = hit.position;
                    walkPointSet = true;
                    return;
                }
            }
        }
        
    }
    

    private void ChasePlayer(){
        agent.SetDestination(player.position);
    }

    private void ShootPlayer(){
        agent.SetDestination(transform.position);
        _TankMovement.RotateToTarget(player.position);
        if(!cooldownActive) _TankShooting.Shoot();
        
    }
}
