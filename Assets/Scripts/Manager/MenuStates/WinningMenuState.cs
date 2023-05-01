using UnityEngine;
using UnityEngine.UI;

public class WinningMenuState : MenuState
{
    public WinningMenuState(ChangeMenu changeMenu) : base(changeMenu) { }

    public override void Enter()
    {
        menu.winScreen.SetActive(true);
        menu.player.gamePaused = true;
        menu.state = "winScreen";
        menu._audioManager.PlayWinningSound();
    }

    public override void Exit()
    {
        menu.winScreen.SetActive(false);
    }
}