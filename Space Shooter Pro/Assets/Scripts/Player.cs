using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField]
    private GameObject laserPrefab, trippleShotPrefab, shield, canvas, explosion, asteroid;

    [SerializeField]
    private GameObject[] fire;

    [SerializeField]
    private float speed = 8f, fireDelay = .15f, mouseSensitivity, speedBoost = 1;

    [SerializeField]
    public int score, fireObjectIndex, health = 3;

    [SerializeField]
    private AudioClip audioClip_Waiting, audioClip_BGL1, audioClip_PanicAlarm, audioClip_AIWarning;

    [SerializeField]
    private AudioSource audioSource_BGMusic, audioSource_SFX, audioSource_SFX2;

    [SerializeField]
    private Text text_TestData;

    private float inputHorizontal;
    private float inputVertical;
    private float mouseX;
    private float mouseY;
    private Vector3 spawnPosition1;
    private Vector3 startPosition;
    private float nextFireMark;
  

    [SerializeField]
    private bool trippleShotActive, shieldActive, speedActive, asteroidDestroyed;

    private SpawnManager spawnManager;
    private UIManager uiManager;


    // Start is called before the first frame update
    void Start() {

        score = 0;
        nextFireMark = 0;
        startPosition = new Vector3(0, -3.5f, 0);
        transform.position = startPosition;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        uiManager = canvas.GetComponent<UIManager>();

        Debug.Log("Screen Width: " + Screen.width);
        mouseSensitivity = 12f;

        if(Screen.width <= 500) {
            mouseSensitivity *= 1.5f;
            Debug.Log("Sensitivity for w<500: " + mouseSensitivity);
            text_TestData.text = "Mouse sensitivity: " + mouseSensitivity.ToString();
        } else if(Screen.width > 800 && Screen.width<1080) {
            mouseSensitivity *= 3f;
            Debug.Log("Sensitivity for 800<w<1080: " + mouseSensitivity);
            text_TestData.text = "Mouse sensitivity: " + mouseSensitivity.ToString();
        } else if (Screen.width >= 1080) {
            //this will catch the Windows build scenario where it's using 1280w x 720 h screen size
            //uses the default mouseSensitivity = 12f set at the Start( ) method
            Debug.Log("Sensitivity for w > 1080" + mouseSensitivity);
            text_TestData.text = "Mouse sensitivity: " + mouseSensitivity.ToString();
        } else {
            mouseSensitivity = 1.5f;
            Debug.Log("Screen width case not provisioned.");
        }
        
        if (spawnManager == null) {
            Debug.Log("There is no Spawn Manager in the game scene.");
        }

        if(audioSource_BGMusic == null) {
            Debug.Log("AudioSource on Player is null!");
        } else {
            audioSource_BGMusic.clip = audioClip_Waiting;
            audioSource_BGMusic.Play();
        }
        
    }

    // Update is called once per frame
    void Update() {

        GetInput();
        MovePlayer();
        CheckPlayerPosition();
        FireLaser();
        StartAudio();

    }

    private void StartAudio() {
        if(!asteroidDestroyed) {
            if(asteroid == null) {
                //Debug.Log("Asteroid Destroyed");
                asteroidDestroyed = true;
                audioSource_BGMusic.clip = audioClip_BGL1;
                audioSource_BGMusic.Play();
                audioSource_BGMusic.volume = .5f;
            }
        }
    }

    public void TakeDamage() {
        if (shieldActive == false) {
            health -= 1;
            uiManager.UpdateHealthSprites(health);
            
            if(health == 2) {
                fireObjectIndex = Random.Range(0, 2);
                fire[fireObjectIndex].SetActive(true);
            } else if (health == 1) {
                if(fireObjectIndex == 0) {
                    fire[1].SetActive(true);
                } else if(fireObjectIndex == 1) {
                    fire[0].SetActive(true);
                }
                audioSource_SFX.clip = audioClip_PanicAlarm;
                audioSource_SFX2.clip = audioClip_AIWarning;
                audioSource_SFX.Play();
                audioSource_SFX2.Play();

            }

        }

        if (health < 1) {
            spawnManager.StopSpawning();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            uiManager.TurnOnGameOverMessage();
        }
    }

    void FireLaser() {
        if (Input.GetKeyDown(KeyCode.Return) && Time.time > nextFireMark) {
            //Debug.Log("Fire!");

            //Center Lazers
            spawnPosition1 = new Vector3(transform.position.x, transform.position.y, 0);
            
            if(trippleShotActive) {
                Instantiate(trippleShotPrefab, spawnPosition1, Quaternion.identity);
            } else {
                Instantiate(laserPrefab, spawnPosition1, Quaternion.identity);
            }
           
            nextFireMark = Time.time + fireDelay;
        }
    }

    void GetInput() {
        //transform.Translate(Vector3.right * Time.deltaTime * speed);
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");

        //Input for Mouse Movement as input
        mouseX = Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1);
        mouseY = Mathf.Clamp(Input.GetAxis("Mouse Y"), -1, 1);
        
    }

    void MovePlayer() {
        //transform.Translate(Vector3.right * inputHorizontal * Time.deltaTime * speed);
        //transform.Translate(Vector3.up * inputVertical * Time.deltaTime * speed);

        if (Mathf.Abs(mouseX) > 0 || Mathf.Abs(mouseY) >0 )  {
            //if mouse is being used for player control, use mouse inputs to move the player
            Cursor.lockState = CursorLockMode.Confined; //if want to confine the mouse inside the game screen
            transform.Translate(new Vector3(mouseX, mouseY, 0) * Time.deltaTime * mouseSensitivity * speedBoost);
        } else {
            //if mouse is not being used for player control, use the keyboard arrow inputs and move player using those inputs
            transform.Translate(new Vector3(inputHorizontal, inputVertical, 0) * Time.deltaTime * speed);
        }


    }
    void CheckPlayerPosition() {
        if (transform.position.x > 7.4f) {

            transform.position = new Vector3(-7.4f, transform.position.y, 0);

        } else if (transform.position.x < -7.4f) {

            transform.position = new Vector3(7.4f, transform.position.y, 0);
        }

        if (transform.position.y > 5.5f) {

            transform.position = new Vector3(transform.position.x, 5.5f, 0);
        } else if (transform.position.y < -5.0f) {

            transform.position = new Vector3(transform.position.x, -5.0f, 0);

            //NOTE: can alternatively use Mathf.Clamp to clamp the y position
            //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 5.5f), 0);
            //Can use this in lieu of the 2 if/else statements above
        }
    }

    public void UpdateScore(int scoreValue) {
        score += scoreValue;

        //Update UI Manager Score Text
        uiManager.UpdateUIScore(score);
    }

    public void TrippleShotActive() {
        trippleShotActive = true;
        StartCoroutine(PowerDownTrippleShot());
        //Start Couroutine to countdown/powerdown TrippleShot powerup after 5 seconds

    }

    public void SpeedPowerUpActive() {
        speedBoost = 2;
        StartCoroutine(PowerDownSpeed());
    }

    public void ShieldActive() {
        shieldActive = true;
        shield.SetActive(true);
        StartCoroutine(PowerDownShield());

    }

    IEnumerator PowerDownShield() {

        yield return new WaitForSeconds(5);
        shieldActive = false;
        shield.SetActive(false);
    }

    IEnumerator PowerDownSpeed() {
        yield return new WaitForSeconds(10);
        speedBoost = 1;
    }

    IEnumerator PowerDownTrippleShot() {
        yield return new WaitForSeconds(5);
        trippleShotActive = false;
    }

}
