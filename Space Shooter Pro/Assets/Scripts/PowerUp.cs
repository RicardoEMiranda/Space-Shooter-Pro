using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private int powerUpID;

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

            //Communicate with Player Object to activate tripple shot (call TrippleShotActive() method)
            Player player = other.transform.GetComponent<Player>();

            if(player != null) {

                //if powerUp ID == 0
                //if powerUp ID ==1, then call player.PowerUpSpeed()

                if(powerUpID == 0) {
                    player.TrippleShotActive();
                } else if (powerUpID ==1) {
                    player.SpeedPowerUpActive();
                }  else if (powerUpID ==2) {
                    player.ShieldActive();
                }
               

               
            }
            
            Destroy(this.gameObject);
        }

    }
}
