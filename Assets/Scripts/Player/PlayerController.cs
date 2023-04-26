using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    private Vector2 move, mouseLook, joystickLook;
    private Vector3 rotationTarget;
    public bool isPc;
    private Tank player;
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
        player = gameObject.GetComponent<Tank>();   
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

            movePlayerWithAim();
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

    public void movePlayer(){
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        player.move(movement);
    }

    public void movePlayerWithAim(){
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        player.move(movement, rotationTarget);
    }

    
}
