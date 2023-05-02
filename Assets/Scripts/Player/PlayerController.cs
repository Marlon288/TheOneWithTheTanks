using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    private Vector2 move, mouseLook, joystickLook;
    private Vector3 rotationTarget;
    public bool isPc;
    private TankShooting _TankShooting;
    private TankMovement _TankMovement;
    public bool gamePaused = false;

    public void OnMove(InputAction.CallbackContext context){
        move = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context){
        mouseLook = context.ReadValue<Vector2>();
    }
    public void OnJoystickLook(InputAction.CallbackContext context){
        joystickLook = context.ReadValue<Vector2>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //player = gameObject.GetComponent<Tank>();   
        _TankShooting = gameObject.GetComponent<TankShooting>();   
        _TankMovement = gameObject.GetComponent<TankMovement>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(isPc && !gamePaused){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mouseLook);

            if(Physics.Raycast(ray, out hit)){
                rotationTarget = hit.point;
            }

            if(Input.GetButton("Fire1") || Input.GetKeyDown("space") ){
                _TankShooting.Shoot();
            }

            MovePlayerWithAim();
        }
        //Joystick on Phone, maybe implementation done later
        // else{
        //     if(joystickLook.x == 0 && joystickLook.y == 0){
        //         movePlayer();
        //     }else{
        //         movePlayerWithAim();
        //     }
        // }
        
    }

    public void MovePlayer(){
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        _TankMovement.Move(movement);
    }

    public void MovePlayerWithAim(){
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        _TankMovement.Move(movement, rotationTarget);
    }

    
}
