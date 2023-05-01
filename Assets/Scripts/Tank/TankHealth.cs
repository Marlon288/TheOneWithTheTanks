using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public int lives = 3;
    public bool isPlayer = false;

    private Image[] hearts;
    private ChangeMenu menu;
    private LevelManager _levelManager;
    private TankVisuals _TankVisuals;

    public AudioClip DeathSound;
    AudioSource _audioSource;

    public GameObject explosionPrefab;
    public float explosionDuration = 0.5f;

    private void Start()
    {
        menu = GameObject.Find("_Manager").GetComponent<ChangeMenu>();
        _levelManager = GameObject.Find("_Manager").GetComponent<LevelManager>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        _TankVisuals = gameObject.GetComponent<TankVisuals>();
        if (isPlayer)
        {
            hearts = GameObject.Find("Hearts").GetComponent<HeartManager>().GetAllChildImages();
            StartCoroutine(InitializeHealth());
        }
    }

    public void TakeHit()
    {
        if(menu.state != "menu"){
            lives -= 1;
            if(isPlayer) UpdateHealth();
            if(lives <= 0 ){
                Explode();
                if(isPlayer){
                    menu.ToGameOver();
                }else{
                    _levelManager.enemiesLeft -= 1;
                    _levelManager.checkForEnemies();
                } 
            }
        }   
        StartCoroutine(_TankVisuals.FlashRed());
    }

    public void AddLife()
    {
        lives += 1;
        UpdateHealth();
    }

    private IEnumerator InitializeHealth()
    {
        yield return new WaitUntil(() => hearts != null && hearts.Length > 0);
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        for(int i  = 0; i<hearts.Length; i++){
            if(i< lives){
                hearts[i].color = new Color32(28,39,103,255);
            }else{
                hearts[i].color = Color.white;
            }
        }
    }

    private void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, explosionDuration);
        _audioSource.clip = DeathSound;
        _audioSource.Play();
        Destroy(gameObject);
    }
}
