using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusBlast : MonoBehaviour {
    // Start is called before the first frame update

    private float speed = 8f;
    private Vector3 blastPosition;
    private Vector3 navigationVector;
    private Vector2 deltaVector;
    private float deltaX;
    private float deltaY;
    private float finalX;
    private float finalY;
    private bool startBlastTimer;

    void Start() {
        blastPosition = new Vector3(0, 4.8f, 0);
        finalX = 0;
        finalY = 4.8f;
        navigationVector = blastPosition - this.transform.position;
    }

    // Update is called once per frame
    void Update() {

        //transform.Translate(navigationVector.normalized * Time.deltaTime * speed, Space.World);

        //Calculate absolute value of distance from current position to target position
        deltaX = Mathf.Abs(transform.position.x - finalX);
        deltaY = Mathf.Abs(transform.position.y - finalY);
        deltaVector = new Vector2(deltaX, deltaY);

        if (deltaVector.magnitude > .1f && !startBlastTimer) {
            transform.Translate(navigationVector.normalized * Time.deltaTime * speed, Space.World);
        } else {
            transform.position = new Vector3(0, 4.8f, 0);
            startBlastTimer = true;
            StartCoroutine(StartBlastTimer());
        }

    }

    IEnumerator StartBlastTimer() {
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter(Collider other) {

        if (other.transform.tag == "Enemy") {
            Destroy(other.gameObject);
        }
    }
}
