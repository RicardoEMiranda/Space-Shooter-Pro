﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private float xStartingPosition;
    private float yStartingPosition;

    [SerializeField]
    private int enemyValue;

    [SerializeField]
    private float speed = 4f;

    [SerializeField]
    private GameObject explosion, enemyLaserPrefab;

    private Player player;
    private bool firedEnemyLaser;
    private GameObject leftScreenNavPoint;
    private GameObject rightScreenNavPoint;
    private SpawnManager spawnManager;
    private Vector3 enemyNavigationVector;
    private bool navVectorCalculated;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        leftScreenNavPoint = GameObject.Find("LeftScreenNavPoint");
        rightScreenNavPoint = GameObject.Find("RightScreenNavPoint");
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if (leftScreenNavPoint && rightScreenNavPoint != null) {
            //Debug.Log("Nav points found");
        } else {
            Debug.Log("Enemy navigation points not in the scene.");
        }
    }

    // Update is called once per frame
    void Update() {

        //Translate enemy from top of screen to bottom of screen at 4 units per second
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        //if enemy position is below viewable screen, respawn at the top of the screen at a random x position
        if(transform.position.y < -10f) {
            //reset start position
            ResetPosition();
        }

        if((enemyValue == 50 || enemyValue == 100) && transform.position.y > 9) {
            //ensures that if the enemy variant with value 50 is reinstantiated (player did not destroy it the first time)
            //at the top of the game scene, reset the firedEnemyLaser variable to ensure it fires again as if it's a new enemy.
            firedEnemyLaser = false;
        }

        //if enemy value is 50 then run the fire laser routine
        if((enemyValue == 50 || enemyValue ==100) && !firedEnemyLaser) {
            FireEnemyLaser();
        }

        if(enemyValue == 100) {
            ExecuteSpecialManeuver();
        }

        if(!spawnManager.continueSpawning && (transform.position.y<-9.5f || transform.position.y>9.7f)) {
            Destroy(this.gameObject);
        }
        
    }

    private void ExecuteSpecialManeuver() {
        if(transform.position.x < 0 && firedEnemyLaser && !navVectorCalculated && transform.position.y < 7.3f) {
            //handle diagonal movement for when the enemy is on the left side of the screen
            navVectorCalculated = true;
            enemyNavigationVector = rightScreenNavPoint.transform.position - transform.position;

        } else if (transform.position.x > 0 && firedEnemyLaser && !navVectorCalculated && transform.position.y < 7.3f) {
            //handle diagonal movement for when the enemy variant is on the right side of the screen
            navVectorCalculated = true;
            enemyNavigationVector = leftScreenNavPoint.transform.position - transform.position;
            
        }
        transform.Translate(enemyNavigationVector.normalized * speed * Time.deltaTime, Space.World);
    }

    private void FireEnemyLaser() {
        float firePosition = Random.Range(4f, 7.3f);
        float positionError = .5f;
        float maxPosition = firePosition + positionError;
        float minPosition = firePosition - positionError;
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - .8f, transform.position.z);
        if(transform.position.y <= (firePosition + positionError) && transform.position.y >= (firePosition - positionError) ) {
            Instantiate(enemyLaserPrefab, spawnPosition, Quaternion.identity);
            firedEnemyLaser = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Hit! " + other.transform.name);

        //if other is Player
        //damage Player
        //Destroy this object
        if(other.transform.tag == "Player") {

            //damage Player
            Player player = other.transform.GetComponent<Player>();

            //null check that the Player componet on the other.transform existis
            if(player == null) {
                Debug.Log("No Player Object Detected");
            }

            if(player != null) {
                //Debug.Log("Player collision detected.");
                player.TakeDamage();
                player.UpdateScore(enemyValue);
            }

            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        //if other is Laser
        //Destroy this object
        if(other.transform.tag == "Laser") {
            Destroy(other.gameObject);

            //If Enemy hit by laser, add +10 points to player score
            if(player != null) {
                player.UpdateScore(enemyValue);
            }
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

    }

    void ResetPosition() {
        xStartingPosition = Random.Range(-6f, 6f);
        yStartingPosition = 10f;
        transform.position = new Vector3(xStartingPosition, yStartingPosition, 0);
    }

    
}
