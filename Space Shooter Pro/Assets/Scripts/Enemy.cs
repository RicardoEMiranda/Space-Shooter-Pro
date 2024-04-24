using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private float xStartingPosition;
    private float yStartingPosition;

    [SerializeField]
    private float speed = 4f;

    // Start is called before the first frame update
    void Start() {
     
    }

    // Update is called once per frame
    void Update() {

        //Translate enemy from top of screen to bottom of screen at 4 units per second
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        //if enemy position is below viewable screen, respawn at the top of the screen at a random x position
        if(transform.position.y < -10f) {
            //reset start position
            ResetPosition();
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Hit! " + other.transform.name);

        //if other is Player
        //damage Player
        //Destroy this object
        if(other.transform.tag == "Player") {

            //damage Player
            Player player = other.transform.GetComponent<Player>();

            //null check that the Player componet on the other.transform existis
            if(player == null) {
                Debug.Log("No Player Object Detected");

            }

            if(player != null) {
                Debug.Log("Player collision detected.");
                player.TakeDamage();
            }
            
            Destroy(this.gameObject);
        }

        //if other is Laser
        //Destroy this object
        if(other.transform.tag == "Laser") {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }

    void ResetPosition() {
        xStartingPosition = Random.Range(-6f, 6f);
        yStartingPosition = 10f;
        transform.position = new Vector3(xStartingPosition, yStartingPosition, 0);
    }

    
}
