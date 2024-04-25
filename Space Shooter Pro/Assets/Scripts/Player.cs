using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float speed = 8f;
    private float mouseSensitivity = 48;

    public float inputHorizontal;
    public float inputVertical;
    public float mouseX;
    public float mouseY;
    private Vector3 spawnPosition1;

    private Vector3 startPosition;

    [SerializeField]
    private GameObject laserPrefab, trippleShotPrefab;

    [SerializeField]
    public float fireDelay = .15f;
    public float nextFireMark;
    private float health = 3;

    [SerializeField]
    private bool trippleShotActive;

    [SerializeField]
    private bool shieldActive = false;

    private SpawnManager spawnManager;

    [SerializeField]
    private GameObject shield;

    // Start is called before the first frame update
    void Start() {

        //take current position and set equal to (0,0,0)
        //transform.position = new Vector3(0, -4, 0);
        nextFireMark = 0;
        startPosition = new Vector3(0, -3.5f, 0);
        transform.position = startPosition;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if(spawnManager == null) {
            Debug.Log("There is no Spawn Manager in the game scene.");
        }

    }

    // Update is called once per frame
    void Update() {

        GetInput();
        MovePlayer();
        CheckPlayerPosition();
        FireLaser();

    }

    public void TakeDamage() {
        if (shieldActive == false) {
            health -= 1;
            Debug.Log("Health: " + health);
        }


        if (health < 1) {
            spawnManager.StopSpawning();
            Destroy(this.gameObject);
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
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }

    void MovePlayer() {
        //transform.Translate(Vector3.right * inputHorizontal * Time.deltaTime * speed);
        //transform.Translate(Vector3.up * inputVertical * Time.deltaTime * speed);

        if(Mathf.Abs(mouseX) > 0 || Mathf.Abs(mouseY) >0 )  {
            //if mouse is being used for player control, use mouse inputs to move the player
            //Cursor.lockState = CursorLockMode.Confined; //if want to confine the mouse inside the game screen
            transform.Translate(new Vector3(mouseX, mouseY, 0) * Time.deltaTime * mouseSensitivity);
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

    public void TrippleShotActive() {
        trippleShotActive = true;
        StartCoroutine(PowerDownTrippleShot());
        //Start Couroutine to countdown/powerdown TrippleShot powerup after 5 seconds

    }

    public void SpeedPowerUpActive() {
        speed = 16f;
        mouseSensitivity += 48;
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
        yield return new WaitForSeconds(5);
        speed = 8f;
        mouseSensitivity = 48;
    }

    IEnumerator PowerDownTrippleShot() {
        yield return new WaitForSeconds(5);
        trippleShotActive = false;
    }

}
