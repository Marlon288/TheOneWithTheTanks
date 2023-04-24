using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    // Start is called before the first frame update

    public int playerHealth;
    private Image[] hearts;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(){
        for(int i  = 0; i<hearts.Length; i++){
            if(i< playerHealth){
                hearts[i].color = new Color32(28,39,103,100);
            }else{
                hearts[i].color = Color.white;
            }
        }
    }
}
