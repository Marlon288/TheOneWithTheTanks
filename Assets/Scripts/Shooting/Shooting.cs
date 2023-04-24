using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    public float firingSpeed = 0.5f;

    private float lastTimeShot = 0f;
    private Tank player;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Tank>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1")){
            player.Shoot();
        }
    }

}
