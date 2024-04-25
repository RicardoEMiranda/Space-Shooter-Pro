using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject enemyContainer;

    [SerializeField]
    private GameObject powerUpContainer;
    
    [SerializeField]
    private GameObject[] powerUps;

    private bool continueSpawning = true;
    private bool spawnTrippleShotPowerup = true;
    private float powerUpSpawnDelay;


    // Start is called before the first frame update
    void Start() {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnTrippleShotPowerup());
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator SpawnEnemyRoutine() {

        //infinite while loop
        //instantiate enemy prefab
        //yield wait for 5 seconds

        //yield return null; //waits 1 frame before executing rest of this routine
        //thing to do after yield return null, this is here just as an example

        //yield return new WaitForSeconds(5);
        //do this here after 5 seconds

        while (continueSpawning) {
            Vector3 spawnPosition = new Vector3(Random.Range(-6f, 6f), 10, 0);
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(1);

        }
    }

    IEnumerator SpawnTrippleShotPowerup() {
        while(spawnTrippleShotPowerup) {
            powerUpSpawnDelay = Random.Range(10, 20);
            Vector3 powerUpSpawnPosition = new Vector3(Random.Range(-6f, 6f), 10, 0);
            int powerUpIndex = Random.Range(0, 3);
            GameObject newPowerUp = Instantiate(powerUps[powerUpIndex], powerUpSpawnPosition, Quaternion.identity);
            newPowerUp.transform.parent = powerUpContainer.transform;
            yield return new WaitForSeconds(powerUpSpawnDelay);
        }

    }

    public void StopSpawning() {
        continueSpawning = false;
    }
}
