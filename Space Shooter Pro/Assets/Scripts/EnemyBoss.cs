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
    private int sequence2RoundTrips;
    private bool deathSequenceStarted;
    private GameManager gameManager;



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

        bossHealth = 10;
        shieldActive = true;
        shieldStrength = 10;

        leftPositionLocation = new Vector3(-4.1f, 5, 0);
        centerPositionLocation = new Vector3(0, 5, 0);
        rightPositionLocation = new Vector3(4.1f, 5, 0);
        roundTrips = 0;
        attackSequence = 1;
        sequence2RoundTrips = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update() {


        MoveIntoInitialPosition();
        //Debug.Log("Boss health" + bossHealth);
        //Debug.Log("Shield Strength" + shieldStrength);
        if(attackSequence == 1 && !deathSequenceStarted) {
            AttackSequence1();
        } else if (attackSequence == 2 && sequence2RoundTrips <=5 && !deathSequenceStarted) {
            bossLaser.SetActive(true);
            AttackSequence2();
        } else if(sequence2RoundTrips > 5 && !deathSequenceStarted) {
            sequence2RoundTrips = 0;
            attackSequence = 1;
            fired = false;
            goCenter = true;
        }


       
        if(roundTrips >= 3) {
            attackSequence = 2;
            roundTrips = 0;
            //Debug.Log("Attack Sequence: " + attackSequence);
        }
        
        if(shieldStrength <= 0) {
            shield.SetActive(false);
            shieldActive = false;
            attackCycleStart = true;
        }

        if(bossHealth <= 0 && !deathSequenceStarted) {
            //Debug.Log("Boss Dead");
            bossLaser.SetActive(false);
            deathSequenceStarted = true;
            goCenter = false;
            goLeft = false;
            goRight = false;
            StartCoroutine(BossExplosionSequence());
        }

    }

    IEnumerator BossExplosionSequence() {
        bossLaser.SetActive(false);
        Vector3 explosionPosition = new Vector3(transform.position.x, transform.position.y - .65f, 0);
        Instantiate(enemyExplosion, explosionPosition, Quaternion.identity);
        yield return new WaitForSeconds(1);

        Vector3 explosionPosition2 = new Vector3(transform.position.x - .5f, transform.position.y + .65f, 0);
        Instantiate(enemyExplosion, explosionPosition2, Quaternion.identity);
        yield return new WaitForSeconds(1);

        Vector3 explosionPosition3 = new Vector3(transform.position.x + .5f, transform.position.y + .65f, 0);
        Instantiate(enemyExplosion, explosionPosition3, Quaternion.identity);
        yield return new WaitForSeconds(1);

        Instantiate(enemyExplosion, explosionPosition, Quaternion.identity);
        yield return new WaitForSeconds(.33f);

        Instantiate(enemyExplosion, explosionPosition2, Quaternion.identity);
        yield return new WaitForSeconds(.33f);

        Instantiate(enemyExplosion, explosionPosition3, Quaternion.identity);
        yield return new WaitForSeconds(.33f);
        Instantiate(enemyExplosion, explosionPosition, Quaternion.identity);
        Instantiate(enemyExplosion, explosionPosition2, Quaternion.identity);
        Instantiate(enemyExplosion, explosionPosition3, Quaternion.identity);
        gameManager.wonGameState = true;
        Destroy(this.gameObject, .25f);

    }


    IEnumerator PauseAttackCenter(bool leftValue, bool rightValue, int delay) {
        goCenter = false;
        fired = false;
        started = false;
        firingSequenceFinished = false;
        yield return new WaitForSeconds(delay);
        goLeft = leftValue;
        goRight = rightValue;
    }

    IEnumerator PauseAttackLeft(int delay) {
        goLeft = false;
        fired = false;
        started = false;
        firingSequenceFinished = false;
        yield return new WaitForSeconds(delay);
        goCenter = true;
        direction = 1;
    }

    IEnumerator PauseAttackRight(int delay) {
        goRight = false;
        fired = false;
        started = false;
        firingSequenceFinished = false;
        yield return new WaitForSeconds(delay);
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


    private void AttackSequence2() {
        if (goLeft) {
            positionDelta = transform.position.x - leftPositionLocation.x;
            if (positionDelta > .1) {
                transform.Translate(new Vector3(-1, 0, 0) * 4 * Time.deltaTime);
            } else if (positionDelta < .1) {
                transform.position = leftPositionLocation;
                StartCoroutine(PauseAttackLeft(0));
            }
        }

        if (goCenter) {

            positionDelta = transform.position.x - centerPositionLocation.x;
            if (Mathf.Abs(positionDelta) > .1) {
                transform.Translate(new Vector3(direction, 0, 0) * 4 * Time.deltaTime);
            } else if (Mathf.Abs(positionDelta) < .1) {
                transform.position = centerPositionLocation;
                sequence2RoundTrips += 1;
                //Debug.Log("Sequence 2 Round Trips: " + sequence2RoundTrips);
                if (direction == -1) {
                    StartCoroutine(PauseAttackCenter(true, false, 0));
                }
                if (direction == 1) {
                    StartCoroutine(PauseAttackCenter(false, true, 0));
                }
            }
        }

        if (goRight) {
            positionDelta = transform.position.x - rightPositionLocation.x;
            if (Mathf.Abs(positionDelta) > .1) {
                transform.Translate(new Vector3(1, 0, 0) * 4 * Time.deltaTime);
            } else if (Mathf.Abs(positionDelta) < .1) {
                transform.position = rightPositionLocation;
                StartCoroutine(PauseAttackRight(0));
            }
        }
    }

    private void AttackSequence1() {
        if (startSequenceComplete && !fired && goCenter) {
            StartCoroutine(PauseAttackCenter(true, false, 3));
            StartCoroutine(FireLaser());
            roundTrips += 1;
            //Debug.Log("Round Trip: " + roundTrips);
        }

        if (goLeft && firingSequenceFinished) {
            positionDelta = transform.position.x - leftPositionLocation.x;
            if (positionDelta > .1) {
                transform.Translate(new Vector3(-1, 0, 0) * 3 * Time.deltaTime);
            } else if (positionDelta < .1) {
                transform.position = leftPositionLocation;
                StartCoroutine(PauseAttackLeft(3));
                StartCoroutine(FireLaser());
            }
        }

        if (goCenter && firingSequenceFinished) {
            //Debug.Log("Go Center");

            positionDelta = transform.position.x - centerPositionLocation.x;
            if (Mathf.Abs(positionDelta) > .1) {
                transform.Translate(new Vector3(direction, 0, 0) * 3 * Time.deltaTime);
            } else if (Mathf.Abs(positionDelta) < .1) {
                transform.position = centerPositionLocation;
                roundTrips += 1;
                Debug.Log("Round Trip: " + roundTrips);
                if (direction == -1) {
                    StartCoroutine(PauseAttackCenter(true, false, 3));
                }
                if (direction == 1) {
                    StartCoroutine(PauseAttackCenter(false, true, 3));
                }
                StartCoroutine(FireLaser());
            }
        }

        if (goRight && firingSequenceFinished) {
            positionDelta = transform.position.x - rightPositionLocation.x;
            if (Mathf.Abs(positionDelta) > .1) {
                transform.Translate(new Vector3(1, 0, 0) * 3 * Time.deltaTime);
            } else if (Mathf.Abs(positionDelta) < .1) {
                transform.position = rightPositionLocation;
                StartCoroutine(PauseAttackRight(3));
                StartCoroutine(FireLaser());
            }
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
