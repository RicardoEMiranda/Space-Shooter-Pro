using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour {

    private Vector3 spawnPosition;
    private Vector3 startFightPosition;
    private bool startSequenceComplete;
    private float startPositionDelta;


    // Start is called before the first frame update
    void Start()  {
        spawnPosition = new Vector3(0, 11, 0);
        transform.position = spawnPosition;
        startFightPosition = new Vector3(0, 5, 0);
        startPositionDelta = Mathf.Abs(startFightPosition.y - spawnPosition.y);

        //StartCoroutine(GetReadySequence());
    }

    // Update is called once per frame
    void Update() {
        //pause for 4 seconds before starting to move into position
        //StartCoroutine(GetReadySequence());

        //after the 4 seconds move to the startFightPosition
    }

    IEnumerator GetReadySequence() {
        yield return new WaitForSeconds(4);
        while(startPositionDelta > .1) {
            transform.Translate(Vector3.down * 3 * Time.deltaTime);
            startPositionDelta = Mathf.Abs(startFightPosition.y - spawnPosition.y);
        } 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Laser") {
            //Debug.Log("Hit with Laser");
        } else if (other.tag == "Player") {
            //Debug.Log("Hit by Player");
        }

    }
}
