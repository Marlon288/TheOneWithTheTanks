using UnityEngine;
using UnityEngine.UI;

public class PauseMenuState : MenuState
{
    public PauseMenuState(ChangeMenu changeMenu) : base(changeMenu) { }

    public override void Enter()
    {
        menu.pauseMenu.SetActive(true);
        menu.levelText.text = "LEVEL " + menu._levelManager.currentLevel.ToString();
        Time.timeScale = 0;
        menu.state = "pauseMenu";
        menu.player.gamePaused = true;
    }

    public override void Exit()
    {
        menu.pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}