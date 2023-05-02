using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuState : MenuState
{
    public OptionsMenuState(MenuManager _MenuManager) : base(_MenuManager) { }

    public override void Enter()
    {
       menu.optionsMenu.SetActive(true);
    }

    public override void Exit()
    {
        menu.optionsMenu.SetActive(false);
    }
}