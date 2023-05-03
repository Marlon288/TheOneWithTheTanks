using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 200f;
    public float acceleration = 5f;
    public float maxSpeed = 8f;

    private Rigidbody rb;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject playerGO = GameObject.Find("Player");
        if (playerGO != null) target = playerGO.transform;
        currentSpeed = 0f;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float step = rotationSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, step, 0.0f);
            rb.MoveRotation(Quaternion.LookRotation(newDirection));

             // Acceleration logic
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
                if (currentSpeed > maxSpeed)
                {
                    currentSpeed = maxSpeed;
                }
            }

            rb.velocity = transform.forward * currentSpeed;
        }
    }
}
