using UnityEngine;
using UnityEngine.UI;

public class GameOverState : MenuState
{
    private LevelManager _levelManager;
    private AudioManager _audioManager;

    public GameOverState(MenuManager _MenuManager) : base(_MenuManager) { }

    public override void Enter()
    {
        menu.gameOver.SetActive(true);
        menu.state = "gameOver";

        menu.gameOver.GetComponent<GameOverScreen>().Setup( menu._levelManager.currentLevel);
        menu._audioManager.PlayGameOverSound();
    }

    public override void Exit()
    {
        menu.gameOver.SetActive(false);
    }
}