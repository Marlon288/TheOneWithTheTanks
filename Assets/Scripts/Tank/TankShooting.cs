using System.Collections;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float firingSpeed = 0.5f;
    public int bounces;
    public bool targetSeeking = false;
    private float lastTimeShot = 0f;

    public void Shoot()
    {
        if(lastTimeShot + firingSpeed <= Time.time){
            lastTimeShot = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
           
            
            if(!targetSeeking) {
                Projectile pjct = bullet.GetComponent<Projectile>();
                pjct.NumOfBounces = bounces;
                pjct.projectileSpeed = bulletForce;
                Rigidbody rb = bullet.GetComponent<Rigidbody>(); 
                rb.AddForce(firePoint.up*bulletForce, ForceMode.Impulse);
                StartCoroutine(DelayedProjectileCheck(pjct));
            }
            
        }
    }
    //The projectile can be stuck in the wall, if it is not moving then the projectile gets removed.
    private IEnumerator DelayedProjectileCheck(Projectile pjct)
    {
        yield return new WaitForSeconds(0.1f);
        if(pjct != null) pjct.checkForMovingProjectile();
    }
}
