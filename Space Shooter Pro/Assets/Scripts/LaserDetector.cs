using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetector : MonoBehaviour {

    private GameObject parentEnemyObject;

    // Start is called before the first frame update
    void Start() {
        parentEnemyObject = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Laser") {
            //Debug.Log("Laser detected");
            parentEnemyObject.GetComponent<Enemy>().executeEvasiveManeuver = true;
        } else if (other.tag == "Player") {
            //Debug.Log("Player Detected");
        }
    }
}
