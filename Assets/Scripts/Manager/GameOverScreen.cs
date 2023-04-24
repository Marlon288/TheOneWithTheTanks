using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text levelText;
    public void Setup(int level){
        levelText.text = "LEVEL " + level.ToString();
    }

}
