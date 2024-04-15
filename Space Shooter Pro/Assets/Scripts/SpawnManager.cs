using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject enemyContainer;

    private bool continueSpawning = true;


    // Start is called before the first frame update
    void Start() {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update() {
        
    }

    IEnumerator SpawnRoutine() {

        //infinite while loop
        //instantiate enemy prefab
        //yield wait for 5 seconds

        //yield return null; //waits 1 frame before executing rest of this routine
        //thing to do after yield return null, this is here just as an example

        //yield return new WaitForSeconds(5);
        //do this here after 5 seconds

        while (continueSpawning) {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(5);

        }


    }

    public void StopSpawning() {
        continueSpawning = false;
    }
}
