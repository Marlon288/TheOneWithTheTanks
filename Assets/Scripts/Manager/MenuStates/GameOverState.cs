using UnityEngine;
using UnityEngine.UI;

public class GameOverState : MenuState
{
    private LevelManager _levelManager;
    private AudioManager _audioManager;

    public GameOverState(ChangeMenu changeMenu) : base(changeMenu) { }

    public override void Enter()
    {
        _levelManager = GameObject.Find("_Manager").GetComponent<LevelManager>();
        _audioManager = GameObject.Find("_Manager").GetComponent<AudioManager>();

        menu.gameOver.SetActive(true);
        menu.state = "gameOver";

        menu.gameOver.GetComponent<GameOverScreen>().Setup(_levelManager.currentLevel);
        _audioManager.PlayGameOverSound();
    }

    public override void Exit()
    {
        menu.gameOver.SetActive(false);
    }
}