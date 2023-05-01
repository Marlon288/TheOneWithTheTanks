using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float speed = 8;
    public float turnSpeedWheels = .05f;
    public float turnSpeedHead = .025f;

    public LayerMask obstacleLayer;
    public float obstacleCheckDistance = 1f;

    private Vector3 lastSafePosition;

    void Start(){
        lastSafePosition = transform.position;
    }

    void OnTriggerStay(Collider other){
        if (other.CompareTag("Obstacle") || other.CompareTag("Hole")){
            transform.position = lastSafePosition;
        }
    }

    public void Move(Vector3 movement){
        if (!IsObstacleInFront(movement)){
            lastSafePosition = transform.position;
            Transform transChild = this.gameObject.transform.GetChild(0);
            if(movement != Vector3.zero){
                transChild.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnSpeedWheels);
            }
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
    }

    public void Move(Vector3 movement, Vector3 rotationTarget)
    {
        if (!IsObstacleInFront(movement)){
            lastSafePosition = transform.position;
            RotateToTarget(rotationTarget);
            Transform transWheels = this.gameObject.transform.GetChild(0);
            if(movement != Vector3.zero){
                transWheels.rotation = Quaternion.Slerp(transWheels.rotation, Quaternion.LookRotation(movement.normalized), turnSpeedWheels);
            }
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
         }
    }

    public void RotateToTarget(Vector3 rotationTarget)
    {
        Transform transHead = this.gameObject.transform.GetChild(1);

        var lookPos = (rotationTarget - transHead.position).normalized;
        lookPos.y = 0f;
        var rotation = Quaternion.LookRotation(lookPos);

        Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.z);
        
        if(aimDirection != Vector3.zero) transHead.rotation = Quaternion.Slerp(transHead.rotation, rotation, turnSpeedHead);   
        
    }

    private bool IsObstacleInFront(Vector3 movement)
    {
        RaycastHit hit;
        Vector3 pos = transform.position;
        if (Physics.Raycast(pos, movement.normalized, out hit, obstacleCheckDistance, obstacleLayer)){
            return true;
        }
        return false;
    }
}
