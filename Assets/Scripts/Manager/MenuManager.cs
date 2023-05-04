using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menu, pauseMenu, gameOver, winScreen, optionsMenu;
    public string state = "menu";
    private MenuState currentState;

    public LevelManager _levelManager;
    public AudioManager _audioManager;

    public Text levelText;
    public GameObject prefabTank;


    void Awake(){
        _audioManager = gameObject.GetComponent<AudioManager>();
        _levelManager = gameObject.GetComponent<LevelManager>();
        SetMenuState(new MainMenuState(this));
    }

    void Update(){
         if (Input.GetKeyDown(KeyCode.Escape) && (state == "level" || state == "pauseMenu")){
            if (state == "pauseMenu"){
                currentState.Exit();
            }
            else{
                SetMenuState(new PauseMenuState(this));
            }
         }//else if(Input.GetKeyDown(KeyCode.Return)){
            // _levelManager.AdvanceLevel();
         //}
    }

    public void SetMenuState(MenuState newState){
        if (currentState != null){
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public void ToMenu(){
        SetMenuState(new MainMenuState(this));
    }

    public void StartGame(){
        currentState.Exit();
        _levelManager.LoadAndClearLevel(1);
        state = "level";
    }

    public void ToPauseMenu(){
        SetMenuState(new PauseMenuState(this));
    }

    public void ToGameOver(){
        if(state != "winScreen")SetMenuState(new GameOverState(this));

    }

    public void RestartFromGameOver(){
        GameObject newPlayer = Instantiate(prefabTank, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        newPlayer.name = "Player";
        state = "level";
        currentState.Exit();
        _levelManager.LoadAndClearLevel(1);
    }

    public void ExitGame(){
        Application.Quit();
    }


    public void ToWinningScreen(){
        SetMenuState(new WinningMenuState(this));
    }

    public void ExitState(){
        currentState.Exit();
        state = "level";
    }

    public void ToOptions(){
        SetMenuState(new OptionsMenuState(this));
    }

}
