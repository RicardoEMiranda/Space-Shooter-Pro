﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private float xStartingPosition;
    private float yStartingPosition;

    [SerializeField]
    public float speed = 4f;

    // Start is called before the first frame update
    void Start() {
        yStartingPosition = 8.5f;
        xStartingPosition = Random.Range(-10f, 10f);
        transform.position = new Vector3(xStartingPosition, yStartingPosition, 0);
    }

    // Update is called once per frame
    void Update() {

        //Translate enemy from top of screen to bottom of screen at 4 units per second
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        //if enemy position is below viewable screen, respawn at the top of the screen at a random x position
        if(transform.position.y < -6.5) {
            //reset start position
            ResetPosition();
        }

    }

    void ResetPosition() {
        xStartingPosition = Random.Range(-10f, 10f);
        transform.position = new Vector3(xStartingPosition, yStartingPosition, 0);
    }
}
