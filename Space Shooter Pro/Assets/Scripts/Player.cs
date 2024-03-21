using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float speed = 3.5f;

    // Start is called before the first frame update
    void Start() {

        //take current position and set equal to (0,0,0)
        transform.position = new Vector3(0, 0, 0);

    }

    // Update is called once per frame
    void Update() {

        transform.Translate(Vector3.right * Time.deltaTime * speed);

    }
}
