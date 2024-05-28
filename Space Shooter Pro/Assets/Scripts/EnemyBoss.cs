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
    private float stopWatchTime;
    private float time;
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

        bossHealth = 5;
        shieldActive = true;
        shieldStrength = 5;
        
    }

    // Update is called once per frame
    void Update() {
        MoveIntoInitialPosition();
        AggressiveAttack();

        if(shieldStrength <= 0) {
            shield.SetActive(false);
            shieldActive = false;
        }

        if(bossHealth <= 0) {
            Debug.Log("Boss Dead");
        }

    }

    private void AggressiveAttack() {
        if(startSequenceComplete) {
            //Debug.Log("Start Sequence Complete, start aggressive attack");
            
        }
    }

    private void MoveIntoInitialPosition() {
        if(!startSequenceComplete) {
            //pause for 4 seconds before starting to move into position
            //after the 4 seconds move to the startFightPosition
            startPositionDelta = Mathf.Abs(startFightPosition.y - transform.position.y);
            //Debug.Log("Position Delta: " + startPositionDelta);
            time = DelayTimer();

            if (time <= 4) {
                transform.position = spawnPosition;
            } else if (time > 4 && startPositionDelta > .1) {
                transform.Translate(Vector3.down * 2 * Time.deltaTime);
            } else if (time > 4 & startPositionDelta < .1) {
                transform.position = startFightPosition;
                startSequenceComplete = true;
            } else {
                Debug.Log("Start Sequence exception");
            }
            Debug.Log("Time: " + time);
        }
        
    }

   

    private float DelayTimer() {
        stopWatchTime += Time.deltaTime;
        
        return stopWatchTime;
    }

    private void DamagePlayer(Collider2D obj) {
        //damage Player
        Player player = obj.transform.GetComponent<Player>();

        //null check that the Player componet on the other.transform existis
        if (player == null) {
            Debug.Log("No Player Object Detected");
        }

        if (player != null) {
            //Debug.Log("Player collision detected.");
            player.TakeDamage();
            //player.UpdateScore(enemyValue);
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Laser" && shieldActive) {
            //Debug.Log("Hit with Laser");
            Destroy(other.gameObject);
            shieldStrength -= 1;
            Vector3 explosionPosition = new Vector3(other.transform.position.x, transform.position.y -1f, transform.position.z);
            Instantiate(shieldExplosion, explosionPosition, Quaternion.identity);
        } else if (other.tag == "Player" && shieldActive) {
            //Debug.Log("Hit by Player");
            shieldStrength -= 1;
            Vector3 explosionPosition = new Vector3(transform.position.x, transform.position.y -1f, transform.position.z);
            Instantiate(shieldExplosion, explosionPosition, Quaternion.identity);
            DamagePlayer(other);
        }

        if (other.tag == "Laser" && !shieldActive) {
            //Debug.Log("Hit with Laser");
            Destroy(other.gameObject);
            bossHealth -= 1;
            Vector3 explosionPosition = new Vector3(other.transform.position.x, transform.position.y, transform.position.z);
            Instantiate(enemyExplosion, explosionPosition, Quaternion.identity);
        } else if (other.tag == "Player" && !shieldActive) {
           //Debug.Log("Hit by Player");
            bossHealth -= 1;
            Vector3 explosionPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(enemyExplosion, explosionPosition, Quaternion.identity);
            DamagePlayer(other);
        }

    }
}
