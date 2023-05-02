using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using UnityEngine.AI; 
using UnityEngine;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour{
    
    [System.Serializable]
    public class Level{
        public List<SpawnInfo> obstacles;
        public List<SpawnInfo> enemySpawns;
        public SpawnInfo playerSpawn;
        
    }

    [System.Serializable]
    public class SpawnInfo{
        public GameObject prefab;
        public Vector3 spawnPosition;
        public Quaternion spawnRotation;
    }


    public List<Level> levels;
    public int currentLevel = 0;
    public NavMeshSurface navMeshSurface;
    public int enemiesLeft;

    private AudioManager _audioManager;

    void Awake(){
        _audioManager = gameObject.GetComponent<AudioManager>();
    }

    void Start(){
        LoadLevel(0);
    }

    public void AdvanceLevel(){
        ClearLevel();
        currentLevel++;

        if (currentLevel < levels.Count){
            LoadLevel(currentLevel);
        }
        else{
            gameObject.GetComponent<MenuManager>().ToWinningScreen();

        }
    }
    public void LoadAndClearLevel(int level){
        ClearLevel();
        LoadLevel(level);
    }
    private void LoadLevel(int levelIndex){
        currentLevel = levelIndex;
        if (levelIndex >= 0 && levelIndex < levels.Count){
            Level level = levels[levelIndex];
            enemiesLeft = 0;
            foreach (SpawnInfo obstacle in level.obstacles){
                Instantiate(obstacle.prefab, obstacle.spawnPosition, obstacle.spawnRotation);
            }
            int highestEnemy = 1;
            //Isolates Numbers from the Prefab Names
            string pattern = @"\d+";
            foreach (SpawnInfo enemySpawn in level.enemySpawns){
                Match match = Regex.Match(enemySpawn.prefab.name, pattern);
                if (match.Success){
                    int currentEnemy= Int32.Parse(match.Value);
                    if (currentEnemy > highestEnemy) highestEnemy = currentEnemy;  
                }
                Instantiate(enemySpawn.prefab, enemySpawn.spawnPosition, enemySpawn.spawnRotation);
                enemiesLeft++;
            }
            if(levelIndex!=0) _audioManager.ChangeToThemeSong(highestEnemy);
            GameObject player = GameObject.Find("Player");
            if(player != null){
                player.transform.position = level.playerSpawn.spawnPosition;
                player.transform.rotation = level.playerSpawn.spawnRotation;
            }

            StartCoroutine(DelayedBuildNavMesh());
        }else{
            Debug.Log("Invalid level index: " + levelIndex);
        }
    }

    private IEnumerator DelayedBuildNavMesh() {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(0.5f);

        // Call the method you want to delay
        navMeshSurface.BuildNavMesh();
    }

    private void ClearLevel(){
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles){
            Destroy(obstacle);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies){
            Destroy(enemy);
        }
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject projectile in projectiles){
            Destroy(projectile);
        }
    }

    public void checkForEnemies(){
        if(enemiesLeft == 0){
            if(currentLevel%5 == 0){
                GameObject.Find("Player").GetComponent<TankHealth>().AddLife();
            }
            _audioManager.PlayAdvanceLevelSound();
            StartCoroutine(DelayedAdvanceLevel());
        }
    }

    private IEnumerator DelayedAdvanceLevel() {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(2f);

        // Call the method you want to delay
        AdvanceLevel();
    }

}

