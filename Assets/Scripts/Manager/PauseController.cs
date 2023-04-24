using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    
    private bool isPaused = false;

    // Update is called once per frame
    void Update(){
         if (Input.GetKeyDown(KeyCode.Escape)){
            if (isPaused){
                ResumeGame();
            }
            else{
                PauseGame();
            }
        }
    }

    void PauseGame(){
        Time.timeScale = 0;
        isPaused = true;
        // You can also enable a pause menu UI here
    }

    void ResumeGame(){
        Time.timeScale = 1;
        isPaused = false;
        // You can also disable the pause menu UI here
    }
}
