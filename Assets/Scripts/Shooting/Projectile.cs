using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 firingPoint;

    public float projectileSpeed = 20f;
    public float maxProjectileDistance = 50f;
    
    private Rigidbody rb;
    Vector3 lastVelocity;
    private int currBounces = 0;

    private float curSpeed;
    private Vector3 direction;

    public int NumOfBounces = 2;
    public GameObject explosionPrefab; 
    public float explosionDuration = 0.5f;

    void Start()
    {
        firingPoint = transform.position;
        rb = GetComponent<Rigidbody>();
        
    }


    void LateUpdate()
    {
        lastVelocity = rb.velocity;
    }

    public void checkForMovingProjectile(){
        if(lastVelocity.magnitude < 0.5f) Destroy(gameObject);
    }

    private Tank GetTankComponent(Transform transform){
        if (transform == null) return null;

        Tank tank = transform.GetComponent<Tank>();
        if (tank != null)
            return tank;

        return GetTankComponent(transform.parent);
    }
    private void handleCollision(GameObject hitObject, Vector3 collisionNormal){
        if (hitObject.layer == 6 || hitObject.layer == 8){
            if(NumOfBounces<=currBounces){
                Explode();
            }else{
                direction = Vector3.Reflect(lastVelocity.normalized, collisionNormal);
                rb.velocity = direction * Mathf.Max(projectileSpeed, 0);
                currBounces++;
            }
        }else if(hitObject.tag == "Enemy" || hitObject.tag == "Player"){
            Tank tank = GetTankComponent(hitObject.transform);
            Debug.Log(rb.velocity.magnitude);
            if (tank != null) {
                Explode();
                tank.takeHit();
            } else {
                Debug.Log("Tank component not found on the GameObject with the 'Enemy' or 'Player' tag.");
            }
        }else if(hitObject.tag == "Projectile"){
            Explode();
        }
    }
    private void OnCollisionEnter(Collision collision){
        handleCollision(collision.gameObject, collision.contacts[0].normal);
    }

    private void OnTriggerEnter(Collider collider){
        handleCollision(collider.gameObject, Vector3.zero);
    }
    
    private void Explode(){
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, explosionDuration);
        Destroy(gameObject);
    }
}

