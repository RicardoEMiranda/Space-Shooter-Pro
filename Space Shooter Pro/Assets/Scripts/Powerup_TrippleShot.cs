using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_TrippleShot : MonoBehaviour {

    [SerializeField]
    private float speed = 3f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()  {


        //move power up at speed 3
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y<=-6.5) {
            Destroy(this.gameObject);
        }

        //detect collision with player and destroy self after collision

    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Player") {
            Destroy(this.gameObject);
        }

    }
}
