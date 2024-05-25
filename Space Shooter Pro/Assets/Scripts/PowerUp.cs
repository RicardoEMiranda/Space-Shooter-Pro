using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private int powerUpID;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip audioClip_PowerUp;

    [SerializeField]
    private GameObject explosion;
    private GameObject playerGO;
    private Vector3 navigationVector;
    private bool isGoodPowerup;
    private float attractionSpeed = 2f;


    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        playerGO = GameObject.Find("Player");

        if(powerUpID != 6) {
            isGoodPowerup = true;
        }
    }

    // Update is called once per frame
    void Update()  {


        //move power up at speed 3
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y<=-6.5) {
            Destroy(this.gameObject);
        }

        if(Input.GetKey(KeyCode.C) && isGoodPowerup) {
            //Debug.Log("C Key Pressed!");
            navigationVector = playerGO.transform.position - transform.position;
            transform.Translate(navigationVector * attractionSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Player") {

            //Communicate with Player Object to activate tripple shot (call TrippleShotActive() method)
            Player player = other.transform.GetComponent<Player>();

            if(player != null) {

                //if powerUp ID == 0
                //if powerUp ID ==1, then call player.PowerUpSpeed()
                switch (powerUpID) {
                    case 0:
                        player.TrippleShotActive();
                        audioSource.clip = audioClip_PowerUp;
                        audioSource.Play();
                        break;
                    case 1:
                        player.SpeedPowerUpActive();
                        audioSource.clip = audioClip_PowerUp;
                        audioSource.Play();
                        //audioSource.pitch = 2;
                        break;
                    case 2:
                        player.ShieldActive();
                        audioSource.clip = audioClip_PowerUp;
                        audioSource.Play();
                        //audioSource.pitch = 2;
                        break;
                    case 3:
                        player.AmmoPowerUpActive();
                        audioSource.clip = audioClip_PowerUp;
                        audioSource.Play();
                        break;
                    case 4:
                        player.HealthPowerUpActive();
                        audioSource.clip = audioClip_PowerUp;
                        audioSource.Play();
                        break;
                    case 5:
                        player.RadiusBlastActive();
                        audioSource.clip = audioClip_PowerUp;
                        audioSource.Play();
                        break;
                    case 6:
                        player.TakeDamage();
                        Instantiate(explosion, transform.position, Quaternion.identity);
                        break;
                    default:
                        Debug.Log("Warning, check Power Up IDs...");
                        break;

                }
            }
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject, 1);
        }

    }
}
