using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float speed = 8f;

    public float inputHorizontal;
    public float inputVertical;
    private Vector3 spawnPosition1;
    private Vector3 spawnPosition2;
    private Vector3 spawnPosition3;

    private Vector3 spawnPosition4;
    private Vector3 spawnPosition5;
    private Vector3 spawnPosition6;

    private Vector3 spawnPosition7;
    private Vector3 spawnPosition8;
    private Vector3 spawnPosition9;

    private Vector3 startPosition;

    [SerializeField]
    private GameObject laserPrefab;

    [SerializeField]
    public float fireDelay = .15f;
    public float nextFireMark;
    private float health = 3;

    // Start is called before the first frame update
    void Start() {

        //take current position and set equal to (0,0,0)
        //transform.position = new Vector3(0, -4, 0);
        nextFireMark = 0;
        startPosition = new Vector3(0, -3.5f, 0);
        transform.position = startPosition;

    }

    // Update is called once per frame
    void Update() {

        GetInput();
        MovePlayer();
        CheckPlayerPosition();
        FireLaser();

    }

    public void TakeDamage() {
        health -= 1;
        //Debug.Log("Health: " + health);

        if(health < 1) {
            Destroy(this.gameObject);
        }
    }

    void FireLaser() {
        if (Input.GetKeyDown(KeyCode.Return) && Time.time > nextFireMark) {
            //Debug.Log("Fire!");

            //Center Lazers
            spawnPosition1 = new Vector3(transform.position.x, transform.position.y + .8f, 0);
            spawnPosition2 = new Vector3(transform.position.x + 1.25f, transform.position.y + .8f, 0);
            spawnPosition3 = new Vector3(transform.position.x - 1.25f, transform.position.y + .8f, 0);

            Instantiate(laserPrefab, spawnPosition1, Quaternion.identity);
            Instantiate(laserPrefab, spawnPosition2, Quaternion.identity);
            Instantiate(laserPrefab, spawnPosition3, Quaternion.identity);

            nextFireMark = Time.time + fireDelay;
        }
    }

    void GetInput() {
        //transform.Translate(Vector3.right * Time.deltaTime * speed);
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");  
    }

    void MovePlayer() {
        //transform.Translate(Vector3.right * inputHorizontal * Time.deltaTime * speed);
        //transform.Translate(Vector3.up * inputVertical * Time.deltaTime * speed);
        transform.Translate(new Vector3(inputHorizontal, inputVertical, 0) * Time.deltaTime * speed);
    }
    void CheckPlayerPosition() {
        if (transform.position.x > 11f) {

            transform.position = new Vector3(-11f, transform.position.y, 0);

        } else if (transform.position.x < -11f) {

            transform.position = new Vector3(11f, transform.position.y, 0);
        }

        if (transform.position.y > 3.5f) {

            transform.position = new Vector3(transform.position.x, 3.5f, 0);
        } else if (transform.position.y < -3.65f) {

            transform.position = new Vector3(transform.position.x, -3.65f, 0);

            //NOTE: can alternatively use Mathf.Clamp to clamp the y position
            //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 5.5f), 0);
            //Can use this in lieu of the 2 if/else statements above
        }
    }

}
