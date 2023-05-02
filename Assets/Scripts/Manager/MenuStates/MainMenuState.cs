using UnityEngine;
using UnityEngine.UI;

public class MainMenuState : MenuState
{
    public MainMenuState(MenuManager _MenuManager) : base(_MenuManager) { }

    public override void Enter()
    {
        menu.state = "menu";
        menu.menu.SetActive(true);
        Object.Destroy(GameObject.Find("Player"));
        GameObject newPlayer = Object.Instantiate(menu.prefabTank, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        newPlayer.name = "Player";

        
        menu._levelManager.LoadAndClearLevel(0);
        menu._audioManager.ChangeToMenuAudio();
    }

    public override void Exit()
    {
        menu.menu.SetActive(false);
    }
}