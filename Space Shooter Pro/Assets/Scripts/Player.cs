using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float speed = 3.5f;

    public float inputHorizontal;
    public float inputVertical;

    [SerializeField]
    private GameObject laserPrefab;

    // Start is called before the first frame update
    void Start() {

        //take current position and set equal to (0,0,0)
        transform.position = new Vector3(0, 0, 0);

    }

    // Update is called once per frame
    void Update() {

        GetInput();
        MovePlayer();
        CheckPlayerPosition();

        if(Input.GetKeyDown(KeyCode.Space)) {
            //Debug.Log("Fire!");
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
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
        if (transform.position.x > 11.29f) {

            transform.position = new Vector3(-11.29f, transform.position.y, 0);

        } else if (transform.position.x < -11.29f) {

            transform.position = new Vector3(11.29f, transform.position.y, 0);
        }

        if (transform.position.y > 5.5f) {

            transform.position = new Vector3(transform.position.x, 5.5f, 0);
        } else if (transform.position.y < -3.5f) {

            transform.position = new Vector3(transform.position.x, -3.5f, 0);

            //NOTE: can alternatively use Mathf.Clamp to clamp the y position
            //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 5.5f), 0);
            //Can use this in lieu of the 2 if/else statements above
        }
    }

}
