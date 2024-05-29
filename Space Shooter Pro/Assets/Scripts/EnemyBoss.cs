using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour {

    [SerializeField]
    private GameObject shieldExplosion, enemyExplosion, bossLaser;
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
    private bool goLeft, goRight, goCenter, attackCycleStart;
    private bool attackCenter, attackLeft, attackRight;
    private float positionDelta;
    private Vector3 leftPositionLocation, rightPositionLocation, centerPositionLocation;
    private bool fired, started, firingSequenceFinished;
    private int direction;
    private int roundTrips;
    private int attackSequence;



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

        leftPositionLocation = new Vector3(-4.1f, 5, 0);
        centerPositionLocation = new Vector3(0, 5, 0);
        rightPositionLocation = new Vector3(4.1f, 5, 0);
        roundTrips = 0;
        attackSequence = 1;
    }

    // Update is called once per frame
    void Update() {

        

        MoveIntoInitialPosition();

        if(startSequenceComplete && !fired && goCenter) {
            StartCoroutine(PauseAttackCenter(true, false));
            StartCoroutine(FireLaser());
            roundTrips += 1;
            Debug.Log("Round Trip: " + roundTrips);
        }

        if(goLeft && firingSequenceFinished) {
            positionDelta = transform.position.x - leftPositionLocation.x;
            if(positionDelta > .1) {
                transform.Translate(new Vector3(-1, 0, 0) * 3 * Time.deltaTime);
            } else if(positionDelta < .1) {
                transform.position = leftPositionLocation;
                StartCoroutine(PauseAttackLeft());
                StartCoroutine(FireLaser());
            }
        }

        if(goCenter && firingSequenceFinished) {
            //Debug.Log("Go Center");
            
            positionDelta = transform.position.x - centerPositionLocation.x;
            if(Mathf.Abs(positionDelta) > .1) {
                transform.Translate(new Vector3(direction, 0, 0) * 3 * Time.deltaTime);
            } else if(Mathf.Abs(positionDelta) <.1) {
                transform.position = centerPositionLocation;
                roundTrips += 1;
                Debug.Log("Round Trip: " + roundTrips);
                if (direction == -1) {
                    StartCoroutine(PauseAttackCenter(true, false));
                } 
                if(direction == 1) {
                    StartCoroutine(PauseAttackCenter(false, true));
                }
                StartCoroutine(FireLaser());
            }
        }

        if(goRight && firingSequenceFinished) {
            positionDelta = transform.position.x - rightPositionLocation.x;
            if(Mathf.Abs(positionDelta) > .1) {
                transform.Translate(new Vector3(1, 0, 0) * 3 * Time.deltaTime);
            }  else if (Mathf.Abs(positionDelta) < .1) {
                transform.position = rightPositionLocation;
                StartCoroutine(PauseAttackRight());
                StartCoroutine(FireLaser());
            }
        }

       
        if(roundTrips >= 3) {
            attackSequence = 2;
            Debug.Log("Attack Sequence: " + attackSequence);
        }
        
        

        if(shieldStrength <= 0) {
            shield.SetActive(false);
            shieldActive = false;
            attackCycleStart = true;
            goCenter = true;
        }

        if(bossHealth <= 0) {
            Debug.Log("Boss Dead");
        }

    }


    IEnumerator PauseAttackCenter(bool leftValue, bool rightValue) {
        goCenter = false;
        fired = false;
        started = false;
        firingSequenceFinished = false;
        yield return new WaitForSeconds(3);
        goLeft = leftValue;
        goRight = rightValue;
    }

    IEnumerator PauseAttackLeft() {
        goLeft = false;
        fired = false;
        started = false;
        firingSequenceFinished = false;
        yield return new WaitForSeconds(3);
        goCenter = true;
        direction = 1;
    }

    IEnumerator PauseAttackRight() {
        goRight = false;
        fired = false;
        started = false;
        firingSequenceFinished = false;
        yield return new WaitForSeconds(3);
        goCenter = true;
        direction = -1;
    }

    IEnumerator FireLaser() {
        //Debug.Log("Fire Laser");
        if(!fired && !started) {
            started = true;
            yield return new WaitForSeconds(.5f);
            bossLaser.SetActive(true);
            fired = true;
            yield return new WaitForSeconds(3);
            bossLaser.SetActive(false);
            firingSequenceFinished = true;
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
            } else if (time > 4 & startPositionDelta <= .1) {
                transform.position = startFightPosition;
                startSequenceComplete = true;
                goCenter = true;
               // Debug.Log("StartSequence Complete");
            } else {
                Debug.Log("Start Sequence exception");
            }
            //Debug.Log("Time: " + time);
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
