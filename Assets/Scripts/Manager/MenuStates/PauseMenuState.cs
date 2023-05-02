using UnityEngine;
using UnityEngine.UI;

public class PauseMenuState : MenuState
{
    public PauseMenuState(MenuManager _MenuManager) : base(_MenuManager) { }

    public override void Enter()
    {
        menu.pauseMenu.SetActive(true);
        menu.levelText.text = "LEVEL " + menu._levelManager.currentLevel.ToString();
        Time.timeScale = 0;
        menu.state = "pauseMenu";
        GameObject.Find("Player").GetComponent<PlayerController>().gamePaused = true;
    }

    public override void Exit()
    {
        menu.pauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameObject.Find("Player").GetComponent<PlayerController>().gamePaused = false;
    }
}