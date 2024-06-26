﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour {
    // Start is called before the first frame update

    private float speed = 8f;
    private float direction = 1f;

    void Start() {

    }

    // Update is called once per frame
    void Update() {

        transform.Translate(Vector3.down * speed * direction * Time.deltaTime);

        if (transform.position.y < -10f || transform.position.y > 10f) {
            Destroy(this.gameObject);
        }
    }

    public void SetOppositeDirection() {
        direction = -1;
    }


    private void OnTriggerEnter(Collider other) {

        if (other.transform.tag == "Player") {
            Destroy(this.gameObject);
        }
    }
}
