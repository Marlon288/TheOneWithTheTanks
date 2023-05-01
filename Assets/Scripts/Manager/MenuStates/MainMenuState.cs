using UnityEngine;
using UnityEngine.UI;

public class MainMenuState : MenuState
{
    public MainMenuState(ChangeMenu changeMenu) : base(changeMenu) { }

    public override void Enter()
    {
        menu.menu.SetActive(true);
        menu.state = "menu";
        menu._audioManager.ChangeToMenuAudio();
    }

    public override void Exit()
    {
        menu.menu.SetActive(false);
    }
}