using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;

    public float inputHorizontal;
    public float inputVertical;
    private Vector3 spawnPosition;
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
        transform.position = new Vector3(0, 0, 0);
        nextFireMark = 0;
        startPosition = new Vector3(-8.8f, 0, 0);
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
            spawnPosition = new Vector3(transform.position.x +.8f, transform.position.y, 0);
            Instantiate(laserPrefab, spawnPosition, Quaternion.identity);
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
        if (transform.position.x > 7.5f) {

            transform.position = new Vector3(7.5f, transform.position.y, 0);

        } else if (transform.position.x < -8.8f) {

            transform.position = new Vector3(-8.8f, transform.position.y, 0);
        }

        if (transform.position.y > 7.55f) {

            transform.position = new Vector3(transform.position.x, -5.55f, 0);
        } else if (transform.position.y < -5.55f) {

            transform.position = new Vector3(transform.position.x, 7.55f, 0);

            //NOTE: can alternatively use Mathf.Clamp to clamp the y position
            //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 5.5f), 0);
            //Can use this in lieu of the 2 if/else statements above
        }
    }

}
