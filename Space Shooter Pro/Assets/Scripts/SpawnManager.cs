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

    private bool continueSpawning = true;
    private bool spawnTrippleShotPowerup = true;
    private float powerUpSpawnDelay;
    private int noOfEnemyVariants;
    private int enemyInstance;


    // Start is called before the first frame update
    void Start() {
        noOfEnemyVariants = enemyPrefab.Length;
    }

    // Update is called once per frame
    void Update() {

    }

    public void StartSpawning() {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnTrippleShotPowerup());
    }

    IEnumerator SpawnEnemyRoutine() {

        //infinite while loop
        //instantiate enemy prefab
        //yield wait for 5 seconds

        //yield return null; //waits 1 frame before executing rest of this routine
        //thing to do after yield return null, this is here just as an example

        //yield return new WaitForSeconds(5);
        //do this here after 5 seconds
        yield return new WaitForSeconds(2f);
        while (continueSpawning) {

            //Randomize enemy variant to spawn
            //enemyInstance = Random.Range(0, noOfEnemyVariants); //Instantiates even distribution among all the variants
            enemyInstance = GetRandomValue(); //this one returns enemy variant 0 with 76% probability and enemy variant 1 with 25% probability
            //Debug.Log("Enemy Instance: " + enemyInstance);

            Vector3 spawnPosition = new Vector3(Random.Range(-6f, 6f), 10, 0);
            GameObject newEnemy = Instantiate(enemyPrefab[enemyInstance], spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(1);
        }
    }

    private int GetRandomValue() {
        //returns enemy variant 0 with 76% probability and enemy variant 1 with 25% probability
        float randomFloat = Random.Range(0f,1f);
        if(randomFloat <= .75f) {
            return 0;
        } else {
            return 1;
        }

    }

    public void SpawnAmmoPowerUp() {
        Vector3 powerUpSpawnPosition = new Vector3(Random.Range(-6f, 6f), 10, 0);
        GameObject ammoPowerUp = Instantiate(powerUps[3], powerUpSpawnPosition, Quaternion.identity);
        ammoPowerUp.transform.parent = powerUpContainer.transform;
    }

    IEnumerator SpawnTrippleShotPowerup() {
        yield return new WaitForSeconds(2f);
        while (spawnTrippleShotPowerup) {
            powerUpSpawnDelay = Random.Range(5, 7);
            Vector3 powerUpSpawnPosition = new Vector3(Random.Range(-6f, 6f), 10, 0);

            //Ammo Powerup is powerID = 4. Random.Range finds IDs 0 to 2
            //Intentionally excluding ID3 (ammo) because that will be handled separately when the player runs out of ammo
            int powerUpIndex = Random.Range(0, 4); 

            GameObject newPowerUp = Instantiate(powerUps[powerUpIndex], powerUpSpawnPosition, Quaternion.identity);
            newPowerUp.transform.parent = powerUpContainer.transform;
            yield return new WaitForSeconds(powerUpSpawnDelay);
        }

    }

    public void StopSpawning() {
        continueSpawning = false;
    }
}
