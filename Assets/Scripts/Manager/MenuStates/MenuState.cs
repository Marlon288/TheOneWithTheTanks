using UnityEngine;
using UnityEngine.UI;

public abstract class MenuState
{
    protected MenuManager menu;

    public MenuState(MenuManager _MenuManager)
    {
        menu = _MenuManager;
    }

    public abstract void Enter();
    public abstract void Exit();
}