using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMenu : MonoBehaviour
{
    public GameObject menu, pauseMenu, gameOver;
    public string state = "menu";
    public PlayerController player;
    public LevelManager _levelManager;
    public Text levelText;
    public GameObject prefabTank;
    private AudioManager _audioManager;

    void Awake(){
        _audioManager = gameObject.GetComponent<AudioManager>();
    }

    void Update(){
         if (Input.GetKeyDown(KeyCode.Escape) && (state == "level" || state == "pauseMenu")){
            if (state == "pauseMenu"){
                ToLevel();
            }
            else{
                ToPauseMenu();
            }
        }
    }

    public void ToMenu(){
        changeScreens(menu);
        _levelManager.LoadAndClearLevel(0);
        state = "menu";
        _audioManager.ChangeToMenuAudio();
    }

    public void startGame(){
        setInActive();
        _levelManager.AdvanceLevel();
    }

    public void ToPauseMenu(){
        levelText.text = "LEVEL " + _levelManager.currentLevel.ToString();
        changeScreens(pauseMenu);
        
        Time.timeScale = 0;
        state = "pauseMenu";
        player.gamePaused = true;
    }

    public void ToGameOver(){
        gameOver.GetComponent<GameOverScreen>().Setup(_levelManager.currentLevel);
        changeScreens(gameOver);
        state = "gameOver";
    }

    public void ToMenuFromGameOver(){
        GameObject newPlayer = Instantiate(prefabTank, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        newPlayer.name = "Player";
        ToMenu();
    }

    public void RestartFromGameOver(){
        GameObject newPlayer = Instantiate(prefabTank, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        newPlayer.name = "Player";
        setInActive();
        _levelManager.LoadAndClearLevel(1);
    }

    public void ToMenuFromPause(){
        Tank player = GameObject.Find("Player").GetComponent<Tank>();
        player.lives = 3;
        ToLevel();
        ToMenu();
    } 

    private void changeScreens(GameObject _gameObject){
        setInActive();
        _gameObject.SetActive(true);
    }

    public void setInActive(){
        if(state == "menu") menu.SetActive(false); 
        else if(state == "pauseMenu") pauseMenu.SetActive(false);
        else if(state == "gameOver") gameOver.SetActive(false);
        state = "level";
    }


    public void ExitGame(){
        Application.Quit();
    }

    public void ToLevel(){
        Time.timeScale = 1;
        setInActive();
        player.gamePaused = false;
    }


}
