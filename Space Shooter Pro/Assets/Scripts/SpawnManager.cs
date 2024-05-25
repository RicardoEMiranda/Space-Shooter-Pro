using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] enemyPrefab;

    [SerializeField]
    private GameObject enemyContainer;

    [SerializeField]
    private GameObject powerUpContainer;
    
    [SerializeField]
    private GameObject[] powerUps;

    public bool continueSpawning = true;
    private bool spawnTrippleShotPowerup = true;
    private float powerUpSpawnDelay;
    private int noOfEnemyVariants;
    private int enemyInstance;
    private GameManager gameManager;
    private int enemyCount;
    private float spawnDelay;
    private float currentSpawnTime;
    private UIManager uiManager;
    private float previousSpawnPosition;


    // Start is called before the first frame update
    void Start() {
        noOfEnemyVariants = enemyPrefab.Length;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //uiManager = GameObject.Find("Canvas_UI").GetComponent<UIManager>();
        enemyCount = 0;
        uiManager = GameObject.Find("Canvas_UI").GetComponent<UIManager>();
        previousSpawnPosition = 0;
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log("Wave: " + gameManager.waveNumber + "   Wave Timer: " + gameManager.waveTimer);

    }

    public void StartSpawning() {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnTrippleShotPowerup());
        StartCoroutine(SpawnEnemyMine());
    }

    IEnumerator SpawnEnemyRoutine() {

        yield return new WaitForSeconds(2f);
        while (continueSpawning) {
            
            //Randomize enemy variant to spawn
            //enemyInstance = Random.Range(0, noOfEnemyVariants); //Instantiates even distribution among all the variants
            enemyInstance = GetRandomValue(); //this one returns enemy variant 0 with 37.5%, enemy variant 1 with 37.5% probability and enemy variant 2 with 25% probability
            //Debug.Log("Enemy Instance: " + enemyInstance);

            if(gameManager.waveNumber == 0) {
                //For the first 20 seconds
                //Wave 0, tell spawn manager to spawn an enemy once every 2-3 seconds
                spawnDelay = Random.Range(2f, 3f);
                uiManager.AlertIncomingWave(gameManager.waveNumber);
            } else if (gameManager.waveNumber == 1) {
                //Between 21-45 seconds, Wave 1, tell spawn manager to spawn an enemy once every 1-2 seconds
                spawnDelay = Random.Range(1f, 1.5f);
                uiManager.AlertIncomingWave(gameManager.waveNumber);
            } else if (gameManager.waveNumber == 2) {
                //Between 45 - 60 seconds, Wave 2, tell spawn manager to spawn an enemy once every .5 - 1 seconds
                spawnDelay = Random.Range(.55f, 1f);
                uiManager.AlertIncomingWave(gameManager.waveNumber);
            } else if (gameManager.waveNumber == 3) {
                //continueSpawning = false;
                uiManager.AlertIncomingWave(gameManager.waveNumber);
                continueSpawning = false;
            }

            Vector3 spawnPosition = new Vector3(Random.Range(-6f, 6f), 10, 0);

            //ensure proper distance between enemies so they are not spawned on top of each other
            float wingManDistance = Mathf.Abs(spawnPosition.x - previousSpawnPosition);
            if(wingManDistance < 1.5f) {
                float delta = 1.5f - wingManDistance;
                spawnPosition.x = spawnPosition.x + delta;
                if(Mathf.Abs(spawnPosition.x) > 6) {
                    spawnPosition.x = spawnPosition.x - 3f;
                }
            }
            

            //Debug.Log("Previous: " + previousSpawnPosition + "   Current: " + spawnPosition.x);
            GameObject newEnemy = Instantiate(enemyPrefab[enemyInstance], spawnPosition, Quaternion.identity);
            enemyCount += 1;
            currentSpawnTime = Time.time;

            newEnemy.transform.parent = enemyContainer.transform;
            
            yield return new WaitForSeconds(spawnDelay);
            previousSpawnPosition = spawnPosition.x;
            //Debug.Log("Wave: " + gameManager.waveNumber + "  Time: " + gameManager.waveTimer);
        }
    }

    private int GetRandomValue() {
        //returns enemy variant 0 with 76% probability and enemy variant 1 with 25% probability
        float randomFloat = Random.Range(0f,1f);
        if(randomFloat <= .375f) {
            return 0;
        } else if (randomFloat >.375f && randomFloat<.75f) {
            return 1; 
        } else {
            return 2;
        }

    }

    public void SpawnAmmoPowerUp() {
        Vector3 powerUpSpawnPosition = new Vector3(Random.Range(-6f, 6f), 10, 0);
        GameObject ammoPowerUp = Instantiate(powerUps[3], powerUpSpawnPosition, Quaternion.identity);
        ammoPowerUp.transform.parent = powerUpContainer.transform;
    }

    IEnumerator SpawnEnemyMine() {
        while (continueSpawning) {
            float delay = Random.Range(7, 10);
            yield return new WaitForSeconds(delay);
            Vector3 powerUpSpawnPosition = new Vector3(Random.Range(-6f, 6f), 10, 0);
            GameObject mine = Instantiate(powerUps[6], powerUpSpawnPosition, Quaternion.identity);
            mine.transform.parent = powerUpContainer.transform;
        }
        
    }

    IEnumerator SpawnTrippleShotPowerup() {
        yield return new WaitForSeconds(2f);
        while (spawnTrippleShotPowerup) {
            powerUpSpawnDelay = Random.Range(5, 7);
            Vector3 powerUpSpawnPosition = new Vector3(Random.Range(-6f, 6f), 10, 0);

            //Ammo Powerup is powerID = 4. Random.Range finds IDs 0 to 2
            //Intentionally excluding ID3 (ammo) because that will be handled separately when the player runs out of ammo
            int powerUpIndex = Random.Range(0, 6); 

            GameObject newPowerUp = Instantiate(powerUps[powerUpIndex], powerUpSpawnPosition, Quaternion.identity);
            newPowerUp.transform.parent = powerUpContainer.transform;
            yield return new WaitForSeconds(powerUpSpawnDelay);
        }

    }

    public void StopSpawning() {
        continueSpawning = false;
    }
}
