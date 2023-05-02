using UnityEngine;
using UnityEngine.UI;

public class WinningMenuState : MenuState
{
    public WinningMenuState(MenuManager _MenuManager) : base(_MenuManager) { }

    public override void Enter()
    {
        menu.winScreen.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerController>().gamePaused = true;
        menu.state = "winScreen";
        menu._audioManager.PlayWinningSound();
    }

    public override void Exit()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().gamePaused = false;
        menu.winScreen.SetActive(false);
    }
}