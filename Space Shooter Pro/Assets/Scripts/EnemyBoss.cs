using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour {

    [SerializeField]
    private GameObject shieldExplosion, enemyExplosion, enemyLaserPrefab;
    private GameObject shield;

    private Vector3 spawnPosition;
    private Vector3 startFightPosition;
    private bool startSequenceComplete;
    private float startPositionDelta;
    private float startPositionDuration;
    private float stopWatchTime;
    private float time;
    private bool beganStartRoutine;
    private int bossHealth;
    private bool shieldActive;
    private int shieldStrength;



    // Start is called before the first frame update
    void Start()  {
        shield = transform.GetChild(0).gameObject;
        shield.SetActive(true);
        spawnPosition = new Vector3(0, 11, 0);
        transform.position = spawnPosition;
        startFightPosition = new Vector3(0, 5, 0);
        startPositionDelta = Mathf.Abs(startFightPosition.y - transform.position.y);
        stopWatchTime = 0;
        time = 0;
        startPositionDuration = 4f;

        bossHealth = 5;
        shieldActive = true;
        shieldStrength = 5;
        
    }

    // Update is called once per frame
    void Update() {
        //pause for 4 seconds before starting to move into position
        //after the 4 seconds move to the startFightPosition
        startPositionDelta = Mathf.Abs(startFightPosition.y - transform.position.y);
        //Debug.Log("Position Delta: " + startPositionDelta);
        time = DelayTimer(5);
        
        if(time <= 4) {
            transform.position = spawnPosition;
        } else if (time > 4 && startPositionDelta > .1) {
            transform.Translate(Vector3.down * 2 * Time.deltaTime);
        } else if(time > 4 & startPositionDelta < .1) {
            transform.position = startFightPosition;
        } else {
            Debug.Log("Start Sequence exception");
        }

        if(shieldStrength <= 0) {
            shield.SetActive(false);
            shieldActive = false;
        }

        if(bossHealth <= 0) {
            Debug.Log("Boss Dead");
        }

    }

   

    private float DelayTimer(float delay) {
        stopWatchTime += Time.deltaTime;
        
        return stopWatchTime;
    }
    

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Laser" && shieldActive) {
            //Debug.Log("Hit with Laser");
            shieldStrength -= 1;
            Vector3 explosionPosition = new Vector3(other.transform.position.x, transform.position.y -1f, transform.position.z);
            Instantiate(shieldExplosion, explosionPosition, Quaternion.identity);
        } else if (other.tag == "Player" && shieldActive) {
            //Debug.Log("Hit by Player");
            shieldStrength -= 1;
            Vector3 explosionPosition = new Vector3(transform.position.x, transform.position.y -1f, transform.position.z);
            Instantiate(shieldExplosion, explosionPosition, Quaternion.identity);
        }

        if (other.tag == "Laser" && !shieldActive) {
            //Debug.Log("Hit with Laser");
            bossHealth -= 1;
            Vector3 explosionPosition = new Vector3(other.transform.position.x, transform.position.y, transform.position.z);
            Instantiate(enemyExplosion, explosionPosition, Quaternion.identity);
        } else if (other.tag == "Player" && !shieldActive) {
           //Debug.Log("Hit by Player");
            bossHealth -= 1;
            Vector3 explosionPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(enemyExplosion, explosionPosition, Quaternion.identity);
        }

    }
}
