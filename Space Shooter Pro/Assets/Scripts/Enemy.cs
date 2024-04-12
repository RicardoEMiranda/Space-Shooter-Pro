using System.Collections;
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
            //ResetPosition();
        }

    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Hit! " + other.transform.name);

        //if other is Player
        //damage Player
        //Destroy this object
        if(other.transform.tag == "Player") {

            //damage Player
            Player player = other.transform.GetComponent<Player>();

            //null check that the Player componet on the other.transform existis
            if(player != null) {
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
        xStartingPosition = Random.Range(-10f, 10f);
        transform.position = new Vector3(xStartingPosition, yStartingPosition, 0);
    }

    
}
