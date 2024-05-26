using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBeacon : MonoBehaviour {

    private GameObject parentMissleObject;
    private Vector2 enemyLocation;

    // Start is called before the first frame update
    void Start() {
        parentMissleObject = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            //Debug.Log("Laser detected");
            parentMissleObject.GetComponent<Lazer>().enemyDetected = true;
            enemyLocation = new Vector3(other.transform.position.x, other.transform.position.y, transform.position.z);
            parentMissleObject.GetComponent<Lazer>().GetTelemetryData(enemyLocation);

        } 
    }
}