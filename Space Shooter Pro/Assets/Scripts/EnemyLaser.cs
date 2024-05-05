﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour {
    // Start is called before the first frame update

    private float speed = 8f;

    void Start() {

    }

    // Update is called once per frame
    void Update() {

        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -2f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {

        if (other.transform.tag == "Player") {
            //Destroy(this.gameObject);
            Debug.Log("Player Hit");
        }
    }
}