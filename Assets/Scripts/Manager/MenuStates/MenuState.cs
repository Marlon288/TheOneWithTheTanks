using UnityEngine;
using UnityEngine.UI;

public abstract class MenuState
{
    protected ChangeMenu menu;

    public MenuState(ChangeMenu changeMenu)
    {
        menu = changeMenu;
    }

    public abstract void Enter();
    public abstract void Exit();
}