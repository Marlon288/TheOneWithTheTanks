using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    public int lives = 3;
    public float speed = 8;
    
    public float turnSpeedHead = .025f;
    public float turnSpeedWheels = .05f;


    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    public float firingSpeed = 0.5f;
    public int bounces;

    private float lastTimeShot = 0f;
    public bool isPlayer = false;

    private Vector3 lastSafePosition;
    public float obstacleCheckDistance = 1f;
    public LayerMask obstacleLayer;

    private Image[] hearts;
    private MenuManager menu;
    private LevelManager _levelManager;

    public float flashDuration = 10f;

    public GameObject explosionPrefab; 
    public float explosionDuration = 0.5f;

    public AudioClip DeathSound;
    AudioSource _audioSource;

    private Dictionary<MeshRenderer, Color[]> OriginalColors;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("_Manager").GetComponent<MenuManager>();
        _levelManager = GameObject.Find("_Manager").GetComponent<LevelManager>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        lastSafePosition = transform.position;
        if(isPlayer) {
            hearts = GameObject.Find("Hearts").GetComponent<HeartManager>().GetAllChildImages();
            StartCoroutine(InitializeHealth());     
        }
        OriginalColors = getMaterials();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void OnTriggerStay(Collider other){
        if (other.CompareTag("Obstacle")){
            transform.position = lastSafePosition;
        }
    }

    public void move(Vector3 movement){
        if (!IsObstacleInFront(movement)){
            lastSafePosition = transform.position;
            Transform transChild = this.gameObject.transform.GetChild(0);
            if(movement != Vector3.zero){
                transChild.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnSpeedWheels);
            }
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
    }

    public void move(Vector3 movement, Vector3 rotationTarget){
         if (!IsObstacleInFront(movement)){
            lastSafePosition = transform.position;
            rotateToTarget(rotationTarget);
            Transform transWheels = this.gameObject.transform.GetChild(0);
            if(movement != Vector3.zero){
                transWheels.rotation = Quaternion.Slerp(transWheels.rotation, Quaternion.LookRotation(movement.normalized), turnSpeedWheels);
            }
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
         }
    }

    public void rotateToTarget(Vector3 rotationTarget){
        Transform transHead = this.gameObject.transform.GetChild(1);

        var lookPos = (rotationTarget - transHead.position).normalized;
        lookPos.y = 0f;
        var rotation = Quaternion.LookRotation(lookPos);

        Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.z);
        
        if(aimDirection != Vector3.zero){
            transHead.rotation = Quaternion.Slerp(transHead.rotation, rotation, turnSpeedHead);   
        }
    }

    public void Shoot(){
        if(lastTimeShot + firingSpeed <= Time.time){
            lastTimeShot = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Projectile pjct = bullet.GetComponent<Projectile>();
            pjct.NumOfBounces = bounces;
            pjct.projectileSpeed = bulletForce;
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.up*bulletForce, ForceMode.Impulse);
            StartCoroutine(DelayedProjectileCheck(pjct));
        }

    }

    private IEnumerator DelayedProjectileCheck(Projectile pjct){
        yield return new WaitForSeconds(0.1f);
        if(pjct != null) pjct.checkForMovingProjectile();
    }

    private void Explode(){
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, explosionDuration);
        _audioSource.clip = DeathSound;
        _audioSource.Play();
        Destroy(gameObject);
    }

    public void takeHit(){
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
        StartCoroutine(FlashRed());
    }
    public void addLife(){
        lives += 1;
        UpdateHealth();
    }
    private bool IsObstacleInFront(Vector3 movement){
        RaycastHit hit;
        
        Vector3 pos = transform.position;
        if (Physics.Raycast(pos, movement.normalized, out hit, obstacleCheckDistance, obstacleLayer)){
            return true;
        }
        return false;
    }

    private IEnumerator InitializeHealth(){
        yield return new WaitUntil(() => hearts != null && hearts.Length > 0);
        UpdateHealth();
    }

    public void UpdateHealth(){
        for(int i  = 0; i<hearts.Length; i++){
            if(i< lives){
                hearts[i].color = new Color32(28,39,103,255);
            }else{
                hearts[i].color = Color.white;
            }
        }
    }

    private Dictionary<MeshRenderer, Color[]> getMaterials(){
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>(true);
        Dictionary<MeshRenderer, Color[]> originalColors = new Dictionary<MeshRenderer, Color[]>();

        foreach (MeshRenderer renderer in renderers){
            Color[] colors = new Color[renderer.materials.Length];
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                colors[i] = renderer.materials[i].color;
            }
            originalColors.Add(renderer, colors);
        }
        return originalColors;
    } 

    private IEnumerator FlashRed(){
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>(true);
        float elapsedTime = 0f;

        while (elapsedTime < flashDuration){
            elapsedTime += Time.deltaTime;

            float lerpFactor = elapsedTime / flashDuration;

            foreach (MeshRenderer renderer in renderers){
                for (int i = 0; i < renderer.materials.Length; i++){
                    renderer.materials[i].color = Color.Lerp(Color.red, OriginalColors[renderer][i], lerpFactor);
                }
            }

            yield return null;
        }

        foreach (MeshRenderer renderer in renderers){
            for (int i = 0; i < renderer.materials.Length; i++){
                renderer.materials[i].color = OriginalColors[renderer][i];
            }
        }
    }


}
